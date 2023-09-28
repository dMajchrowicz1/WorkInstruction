using ConveyorDoc.Business.Constants;
using ConveyorDoc.Business.Model;
using ConveyorDoc.Core;
using ConveyorDoc.Interfaces;
using ConveyorDoc.Interfaces.Commands;
using ConveyorDoc.Notification;
using ConveyorDoc.ViewModels.InstructionViewModels;
using ConveyorDoc.Views.Dialogs;
using ModernWpf.Controls;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToastNotifications;


namespace ConveyorDoc.ViewModels.Commands
{
    public class MainWindowCommands : IMainWindowCommands
    {

        private IRegionManager _regionManager;
        private IWindowsDialogService _dialogService;
        private InstructionViewModelBase _viewModelBase;
        private IToastMessage _toast;


        private DelegateCommand<object> _navigateCommand;
        public DelegateCommand<object> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<object>(ExecuteNaviagateCommand));

        private DelegateCommand _NewWorkInstructionCommand;
        public DelegateCommand NewWorkInstructionCommand =>
            _NewWorkInstructionCommand ?? (_NewWorkInstructionCommand = new DelegateCommand(ExecuteNewInstructionCommand));

        private DelegateCommand _openWorkInstructionCommand;
        public DelegateCommand OpenWorkInstructionCommand =>
            _openWorkInstructionCommand ?? (_openWorkInstructionCommand = new DelegateCommand(ExecuteOpenWorkInstructionCommand));


        private DelegateCommand _saveWorkInstructionCommand;
        public DelegateCommand SaveWorkInstructionCommand =>
            _saveWorkInstructionCommand ?? (_saveWorkInstructionCommand = new DelegateCommand(ExecuteSaveWorkInstructionCommand));


        public MainWindowCommands(IRegionManager regionManager,
            IWindowsDialogService dialogService,
            InstructionViewModelBase viewModelbase,
            IToastMessage toast)
        {
            _dialogService = dialogService;
            _regionManager = regionManager;
            _toast = toast;
            _viewModelBase = viewModelbase;
        }

        private void OpenFile()
        {
            _dialogService.ShowOpenFileDialog($"{Resources.Properties.Resources.Open}   {Resources.Properties.Resources.Instruction}"
                ,InstructionConstants.INSTRUCTION_DIALOG_FILTER, result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    _viewModelBase.CurrentInstruction = Instruction.Load(result.Parameters.GetValue<string>("parameter"));

                    NavigateCommand.Execute("InstructionView");
                }
            }, Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
        }

        private void SaveFile()
        {

            _dialogService.ShowSaveFileDialog(result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    _viewModelBase.CurrentInstruction.Save(result.Parameters.GetValue<string>("parameter"));
                }
              
            },_viewModelBase.CurrentInstruction.SavePath ?? Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            , _viewModelBase.CurrentInstruction.ModuleNumber + InstructionConstants.INSTRUCTION_EXTENSION);

        }

        private void NewFile()
        {
            _dialogService.ShowContentDialog(typeof(NewInstructionDialog), result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    _viewModelBase.CurrentInstruction = result.Parameters.GetValue<Instruction>("dialogInstruction");
                }
            });
        }

        private void ExecuteNaviagateCommand(object commandParameter)
        {


            if (commandParameter is NavigationViewSelectionChangedEventArgs args)
            {

                if (args.IsSettingsSelected)
                {
                    _regionManager.RequestNavigate("ContentRegion", "SettingsView");
                }
                else if (args.SelectedItemContainer.Tag is not null && args.SelectedItemContainer.Tag is string tag)
                {

                    _regionManager.RequestNavigate("ContentRegion", tag);
                }
            }
            else
            {
                _regionManager.RequestNavigate("ContentRegion", commandParameter.ToString());
            }

        }

        private async void ExecuteNewInstructionCommand()
        {
            if (_viewModelBase.CurrentInstruction != null)
            {
                await _dialogService.ShowContentDialog(typeof(AskForSave), result =>
                {
                    if (result.Result == ButtonResult.Yes)
                        SaveFile();

                });
            }
            NewFile();
        }

        private async void ExecuteOpenWorkInstructionCommand()
        {
            if (_viewModelBase.CurrentInstruction != null)
            {
                await _dialogService.ShowContentDialog(typeof(AskForSave), result =>
                {
                    if (result.Result == ButtonResult.OK)
                        SaveFile();
                });
            }
            OpenFile();
        }

        private void ExecuteSaveWorkInstructionCommand()
        {
            if (_viewModelBase.CurrentInstruction != null)
                SaveFile();
            else
                _toast.ShowInfo(Resources.Properties.Resources.SaveInfo);
            
        }
    }
}
