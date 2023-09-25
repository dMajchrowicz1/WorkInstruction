using ConveyorDoc.Business.Constants;
using ConveyorDoc.Core;
using ConveyorDoc.Interfaces;
using ConveyorDoc.Service;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using ToastNotifications;

namespace ConveyorDoc.ViewModels.Commands
{
    public class ApplicationCommands : IApplicationCommands
    {
        private IWindowsDialogService _windowsDialogService;
        private IAppTask _appTask;


        private DelegateCommand<string> _openPDFCommand;
        public DelegateCommand<string> OpenPDFCommand =>
            _openPDFCommand ?? (_openPDFCommand = new DelegateCommand<string>(ExecuteOpenPDFCommand));


        private DelegateCommand<object> _searchCommand;
        public DelegateCommand<object> SearchCommand =>
            _searchCommand ?? (_searchCommand = new DelegateCommand<object>(ExecuteSearchCommand));



        public ApplicationCommands(IWindowsDialogService windowsDialogService ,IAppTask appTask)
        {
            _windowsDialogService = windowsDialogService;
            _appTask = appTask;
        }

        void ExecuteSearchCommand(object parameter)
        {
            _windowsDialogService.ShowFolderBrowseDialog(callback =>
            {
                if (callback.Result == Prism.Services.Dialogs.ButtonResult.OK)
                {
                    if (parameter is TextBox tb)
                    {
                        tb.Text = callback.Parameters.GetValue<string>("parameter");
                    }

                }
            }, Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

        }

        void ExecuteOpenPDFCommand(string parameter)
        {
            if (parameter != null)
            {
                Process.Start("explorer", parameter);
            }
        }
    }
}
