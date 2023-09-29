using ConveyorDoc.Core.Regions;
using ConveyorDoc.Model;
using ConveyorDoc.Views.InstructionViews.InstructionSubViews;
using ConveyorDoc.Views.InstructionViews;
using ModernWpf.Controls;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Prism.Mvvm;
using ConveyorDoc.Interfaces.Commands;
using ConveyorDoc.Core;
using ConveyorDoc.ViewModels.InstructionViewModels;
using ConveyorDoc.Business.Model;
using ConveyorDoc.Business.Queries;
using Resx = ConveyorDoc.Resources.Properties.Resources;
using ConveyorDoc.Business.Constants;
using ConveyorDoc.Model.Settings;
using ConveyorDoc.Business.Extension;

namespace ConveyorDoc.ViewModels
{
    public class InstructionCommands : BindableBase, IInstructionCommands
    {
        private IWindowsDialogService _windowsDialogService;
        private IAppTask _appTask;
        private IRegionManager _regionManager;
        private IGetToolQuery _getToolQuery;
        private InstructionViewModelBase _viewModelBase;
        private AppSettings _settings;



        private DelegateCommand _addInstructionCommand;
        public DelegateCommand AddInstructionCommand =>
            _addInstructionCommand ?? (_addInstructionCommand = new DelegateCommand(ExecuteAddInstructionCommand));

        private DelegateCommand<object> _navigateInstructionSubItemsCommand;
        public DelegateCommand<object> NavigateInstructionSubItemsCommand =>
            _navigateInstructionSubItemsCommand ?? (_navigateInstructionSubItemsCommand = new DelegateCommand<object>(ExecuteNavigateInstructionSubItemsCommand));

        private DelegateCommand<object> _removeInstructionCommand;
        public DelegateCommand<object> RemoveInstructionCommand =>
            _removeInstructionCommand ?? (_removeInstructionCommand = new DelegateCommand<object>(ExecuteRemoveInstructionCommand));

        private DelegateCommand _generateInstructionsCommand;
        public DelegateCommand GenerateInstructionCommand =>
            _generateInstructionsCommand ?? (_generateInstructionsCommand = new DelegateCommand(ExecuteGenerateInstructionCommand));

        private DelegateCommand _addCimcoInstructionCommand;
        public DelegateCommand AddCimcoInstructionCommand =>
            _addCimcoInstructionCommand ?? (_addCimcoInstructionCommand = new DelegateCommand(ExecuteAddCimcoInstructionCommand));

        private DelegateCommand<DragEventArgs> _dropInstructionCommand;
        public DelegateCommand<DragEventArgs> DropInstructionCommand =>
            _dropInstructionCommand ?? (_dropInstructionCommand = new DelegateCommand<DragEventArgs>(ExecuteDropInstructionCommand));

        private DelegateCommand<object> _loadAll;
        public DelegateCommand<object> LoadAllCommand =>
            _loadAll ?? (_loadAll = new DelegateCommand<object>(ExecuteLoadAllCommand));

        private DelegateCommand _ChangeInstructionDataCommand;
        public DelegateCommand ChangeInstructionDataCommand =>
            _ChangeInstructionDataCommand ?? (_ChangeInstructionDataCommand = new DelegateCommand(ExecuteChangeInstructionDataCommand));


        public InstructionCommands(
              IWindowsDialogService windowsDialogService
            , IAppTask appTask
            , IGetToolQuery getToolQuery
            , IRegionManager regionManager
            , InstructionViewModelBase viewModelBase, AppSettings settings)
        {
            _windowsDialogService = windowsDialogService;
            _appTask = appTask;
            _regionManager = regionManager;
            _viewModelBase = viewModelBase;
            _getToolQuery = getToolQuery;
            _settings = settings;
        }



        void ExecuteChangeInstructionDataCommand()
        {
            var parameter = new DialogParameters();
            parameter.Add("instruction",_viewModelBase.CurrentInstruction);

            _windowsDialogService.ShowContentDialog(typeof(InstructionDataDialog), null, parameter);
        }

        void ExecuteLoadAllCommand(object parameter)
        {
            _appTask.RunOnUIAsync(() =>
            {
                if (parameter != null && parameter is Word word)
                {
                    word.GetDefaultDescription();
                    word.FindProgramTools(_getToolQuery);
                    word.ReadVariables();
                }
                else
                    throw new Exception(Resx.SelectWordFile);

            }, Resx.LoadAll);

        }

        void ExecuteAddInstructionCommand()
        {

            _windowsDialogService.ShowOpenFileDialog(Resx.OpenFileNcProgram, NcProgramConstants.NC_PROGRAM_DIALOG_FILTER, result =>
            {
                if (result.Result == Prism.Services.Dialogs.ButtonResult.OK)
                {
                    var programPath = result.Parameters.GetValue<string>("parameter");

                    _viewModelBase.CurrentInstruction.AddWord(programPath);
                }

            },Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

        }

        void ExecuteNavigateInstructionSubItemsCommand(object parameter)
        {
            if (parameter is NavigationViewSelectionChangedEventArgs args
                && args.SelectedItem is InstructinSubItem model)
            {
                Navigate(model.ViewName, (Word)model.Parent);
            }
        }

        void ExecuteDropInstructionCommand(DragEventArgs parameter)
        {

            _appTask.RunOnUIAsync(() =>
            {
                if (parameter.Data is DataObject data)
                {
                    var dataList = data.GetFileDropList();

                    foreach (var item in dataList)
                    {
                        if (item.EndsWith(NcProgramConstants.FANUC_EXTENSIONS, StringComparison.CurrentCultureIgnoreCase)
                            || item.EndsWith(NcProgramConstants.SINUMERIK_EXTENSION, StringComparison.CurrentCultureIgnoreCase))
                        {
                            _viewModelBase.CurrentInstruction.AddWord(item);
                        }
                        else
                        {
                            throw new Exception(Resx.WarningFileLoad);
                        }
                    }

                }

            },Resx.DropingNcProgram);

        }

        void ExecuteAddCimcoInstructionCommand()
        {

            _windowsDialogService.ShowContentDialog(typeof(SelectCimcoProgramDialog), callback =>
            {
                if (callback.Result == ButtonResult.OK)
                {
                    _viewModelBase.CurrentInstruction.AddWord(callback.Parameters.GetValue<IEnumerable<NcProgram>>("programs").ToArray());
                }
            });
        }

        void ExecuteGenerateInstructionCommand()
        {
            _windowsDialogService.ShowFolderBrowseDialog(callback =>
            {
                if (callback.Result == ButtonResult.OK)
                {
                    var dir = callback.Parameters.GetValue<string>("parameter");

                    _appTask.RunAsync(() =>
                    {
                        _viewModelBase.CurrentInstruction.GenerateWords(dir,_settings.InstructionSettings.GenerateDetailedTools);

                    }, Resx.TaskGenerating);
                }
            },_viewModelBase.CurrentInstruction.SavePath ?? Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
        }

        void ExecuteRemoveInstructionCommand(object parameter)
        {

            _appTask.RunOnUIAsync(() =>
            {
                if (parameter is Word word && word is not null)
                {
                    ClearRegion();
                    _viewModelBase.CurrentInstruction.RemoveWord(word);
                    
                }
                else
                    throw new Exception(Resx.SelectWordFile);

            }, $@"{Resx.RemovingWordFile}");


        }

        void Navigate(string view, Word wordInstruction)
        {
            var navigationParameter = new NavigationParameters();
            navigationParameter.Add("word", wordInstruction);

            _regionManager.RequestNavigate(RegionNames.InstructionItemRegion, view, navigationParameter);
        }

        void ClearRegion()
        {
            var region = _regionManager.Regions[RegionNames.InstructionItemRegion];

            region.RemoveAll();
        }

    }
}
