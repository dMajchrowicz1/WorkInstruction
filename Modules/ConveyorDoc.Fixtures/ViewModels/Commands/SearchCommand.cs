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

        private DelegateCommand<FixtureRecord> _folderBrowseCommand;
        public DelegateCommand<FixtureRecord> FolderBrowseCommand =>
            _folderBrowseCommand ?? (_folderBrowseCommand = new DelegateCommand<FixtureRecord>(ExecuteFolderBrowseCommand));


        public SearchCommand(IWindowsDialogService windowsDialogService,IAppDirectories appDirectories)
        {
            _windowsDialogService = windowsDialogService;

            _appDirectories = appDirectories;
        }


        void ExecuteFolderBrowseCommand(FixtureRecord parameter)
        {
            _windowsDialogService.ShowFolderBrowseDialog(callback =>
            {
                if (callback.Result == Prism.Services.Dialogs.ButtonResult.OK)
                {

                    parameter.PDF =  callback.Parameters.GetValue<string>("parameter");

                }

            },_appDirectories.FixturesWorkspaceDir);
        }
    }
}
