using ConveyorDoc.Core;
using ConveyorDoc.Fixtures.Interfaces;
using ConveyorDoc.Fixtures.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ConveyorDoc.Fixtures.ViewModels.Commands
{
    public class SearchCommand : ISearchCommand
    {
        private IWindowsDialogService _windowsDialogService;

        private IAppDirectories _appDirectories;

        private DelegateCommand<FixtureRecord> _fileSelectDialogCommand;
        public DelegateCommand<FixtureRecord> FileSelectDialogCommand =>
            _fileSelectDialogCommand ?? (_fileSelectDialogCommand = new DelegateCommand<FixtureRecord>(ExecuteFileSelectDialogCommand));


        public SearchCommand(IWindowsDialogService windowsDialogService,IAppDirectories appDirectories)
        {
            _windowsDialogService = windowsDialogService;

            _appDirectories = appDirectories;
        }


        void ExecuteFileSelectDialogCommand(FixtureRecord parameter)
        {
            _windowsDialogService.ShowOpenFileDialog(ConveyorDoc.Resources.Properties.Resources.SelectFixtureDrawing,Constants.PDF_DIALOG_FILTER,callback =>
            {
                if (callback.Result == Prism.Services.Dialogs.ButtonResult.OK)
                {

                    parameter.PDF =  callback.Parameters.GetValue<string>("parameter");

                }

            },_appDirectories.FixturesWorkspaceDir);
        }
    }
}
