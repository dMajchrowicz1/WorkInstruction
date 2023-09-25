using Microsoft.Win32;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernWpf;
using ConveyorDoc.Interfaces;
using ModernWpf.Controls;
using Prism.Ioc;
using System.Windows;
using winForms = System.Windows.Forms;
using Prism.Commands;
using ConveyorDoc.Business.Constants;

namespace ConveyorDoc.Core
{
    public class WindowsDialogService : IWindowsDialogService
    {
        private readonly IContainerExtension _containerExtension;

        public WindowsDialogService(IContainerExtension containerExtension)
        {
            _containerExtension = containerExtension;
        }

        public void ShowSaveFileDialog(Action<IDialogResult> callback, string initialDirecotry ,string filename = "")
        {
            IDialogParameters dialogParameters = new DialogParameters();


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Reset();
            saveFileDialog.Title = Resources.Properties.Resources.SaveInstruction;
            saveFileDialog.FileName = filename;
            saveFileDialog.Filter = GeneralConstants.ALL_DIALOG_FILTER;
            saveFileDialog.ValidateNames = false;
            saveFileDialog.CheckFileExists = false;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.RestoreDirectory = false;
            saveFileDialog.InitialDirectory = initialDirecotry;
            saveFileDialog.FileOk += (sender, e) =>
            {
                dialogParameters.Add("parameter", saveFileDialog.FileName);
                callback?.Invoke(new DialogResult(ButtonResult.OK, dialogParameters));
            };
            
            saveFileDialog.ShowDialog();
            
        }

        public void ShowFolderBrowseDialog(Action<IDialogResult> callback, string initialDirecotry)
        {
            IDialogParameters dialogParameters = new DialogParameters();

            if (initialDirecotry == null)
                initialDirecotry = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            winForms.FolderBrowserDialog folderBrowseDialog = new winForms.FolderBrowserDialog();
            folderBrowseDialog.Reset();
            folderBrowseDialog.UseDescriptionForTitle = true;
            folderBrowseDialog.Description = "Select Folder";           
            folderBrowseDialog.InitialDirectory = initialDirecotry;

            var result = folderBrowseDialog.ShowDialog();
            if (result == winForms.DialogResult.OK)
            {
                dialogParameters.Add("parameter", folderBrowseDialog.SelectedPath);
                callback?.Invoke(new DialogResult(ButtonResult.OK, dialogParameters));
            }
            else
            {
                callback?.Invoke(new DialogResult(ButtonResult.Cancel));
            }
        }

        public void ShowOpenFileDialog(string title, string filter, Action<IDialogResult> callback, string initialDirectory, bool multiSelection = false)
        {

            IDialogParameters dialogParameters = new DialogParameters();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Reset();
            openFileDialog.Title = title;
            openFileDialog.RestoreDirectory = false;
            openFileDialog.InitialDirectory = initialDirectory;
            openFileDialog.Multiselect = multiSelection;
            openFileDialog.Filter = filter;
            openFileDialog.FileOk += (sender, e) =>
            {
                if (multiSelection)
                {
                    dialogParameters.Add("parameter", openFileDialog.FileNames.ToList());
                    callback?.Invoke(new DialogResult(ButtonResult.OK, dialogParameters));
                }
                else
                {
                    dialogParameters.Add("parameter", openFileDialog.FileName);
                    callback?.Invoke(new DialogResult(ButtonResult.OK, dialogParameters));
                }

            };
            openFileDialog.ShowDialog();

        }

        public async Task ShowContentDialog(Type view, Action<IDialogResult> callback, IDialogParameters parameters = null)
        {
            var dialogHost = new ContentDialog();

            var content = _containerExtension.Resolve(view);
            if (!(content is FrameworkElement dialogContent))
                throw new InvalidOperationException(Resources.Properties.Resources.DialogContentFrameworkElementError);


            if (!(dialogContent.DataContext is IDialogAware2 viewModel))
            {
                throw new NullReferenceException($"A dialog's ViewModel must implement the IDialogAware2 interface ({dialogContent.DataContext})");
            }


            ConfigureContentDialogEvents(dialogHost, callback, parameters);
            ConfigureContentDialogProperties(dialogHost, dialogContent, viewModel);

            await dialogHost.ShowAsync();
        }


        //privates
        private void ConfigureContentDialogEvents(ContentDialog dialogHost, Action<IDialogResult> callback, IDialogParameters parameters)
        {

            IDialogResult result = null;

            Action<IDialogResult> requestCloseHandler = null;
            requestCloseHandler = (o) =>
            {
                result = o;
                dialogHost.Hide();
            };

            RoutedEventHandler loadedHandler = null;
            loadedHandler = (o, e) =>
            {
                dialogHost.Loaded -= loadedHandler;
                if (dialogHost.DataContext is IDialogAware2 dialogAware)
                {
                    dialogAware.RequestClose += requestCloseHandler;
                    dialogAware.OnDialogOpened(parameters);
                }
            };

            dialogHost.Loaded += loadedHandler;


            TypedEventHandler<ContentDialog, ContentDialogClosedEventArgs> closedHandler = null;
            closedHandler = (o, e) =>
            {
                dialogHost.Closed -= closedHandler;
                if (dialogHost.DataContext is IDialogAware2 dialogAware)
                {
                    dialogAware.RequestClose -= requestCloseHandler;
                }
                if (result == null)
                    result = new DialogResult();
                callback?.Invoke(result);
                dialogHost.DataContext = null;
                dialogHost.Content = null;
            };
            dialogHost.Closed += closedHandler;

            //PrimaryButtonPressed
            dialogHost.PrimaryButtonClick += (o, e) =>
            {
                if (dialogHost.DataContext is IDialogAware2 dialogAware)
                {
                    dialogAware.OnPrimaryButtonPressed();
                    e.Cancel = true;
                }

            };

        }

        private void ConfigureContentDialogProperties(ContentDialog dialogHost, FrameworkElement view, IDialogAware2 viewModel)
        {
            dialogHost.DataContext = viewModel;

            dialogHost.Title = viewModel.Title;
            dialogHost.PrimaryButtonText = viewModel.PrimaryButtonText;
            dialogHost.CloseButtonText = Resources.Properties.Resources.Close;
            dialogHost.Content = view;
            dialogHost.DefaultButton = ContentDialogButton.Primary;
        }

    }
}
