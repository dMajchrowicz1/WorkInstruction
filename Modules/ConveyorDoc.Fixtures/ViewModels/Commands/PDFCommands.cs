using ConveyorDoc.Core;
using ConveyorDoc.Fixtures.Interfaces;
using ConveyorDoc.Fixtures.Model;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Fixtures.ViewModels.Commands
{
    public class PDFCommands : IPDFCommands
    {

        private readonly IWindowsDialogService _windowsDialogService;
        private readonly IAppDirectories _appDirectories;

        private DelegateCommand<FixtureRecord> _pdfDrawingSelectCommand;
        public DelegateCommand<FixtureRecord> PdfDrawingSelectCommand =>
            _pdfDrawingSelectCommand ?? (_pdfDrawingSelectCommand = new DelegateCommand<FixtureRecord>(ExecutePdfDrawingSelectCommand));


        public PDFCommands(IWindowsDialogService windowsDialogService, IAppDirectories appDirectories)
        {
            _windowsDialogService = windowsDialogService;

            _appDirectories = appDirectories;
        }

        void ExecutePdfDrawingSelectCommand(FixtureRecord parameter)
        {
            _windowsDialogService.ShowOpenFileDialog(ConveyorDoc.Resources.Properties.Resources.SelectFixtureDrawing, Constants.PDF_DIALOG_FILTER, callback =>
            {
                if (callback.Result == ButtonResult.OK)
                {
                    parameter.PDF = callback.Parameters.GetValue<string>("parameter");
                }

            }, _appDirectories.FixturesWorkspaceDir);
        }
    }
}
