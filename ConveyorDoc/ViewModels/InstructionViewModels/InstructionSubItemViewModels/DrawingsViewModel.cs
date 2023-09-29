using ConveyorDoc.Business.Model;
using ConveyorDoc.Core;
using ConveyorDoc.Model.Settings;
using Resx = ConveyorDoc.Resources.Properties.Resources;
using Prism.Commands;
using System;
using System.Collections.Generic;
using ConveyorDoc.Business.Constants;
using System.IO;

namespace ConveyorDoc.ViewModels.InstructionViewModels.InstructionSubItemViewModels
{
    public class DrawingsViewModel : SubItemBase<Drawing>
    {
        private IWindowsDialogService _windowsDialogService;
        private IAppTask _appTask;
        private AppSettings _appSettings;


        private DelegateCommand _filePickerCommand;
        public DelegateCommand FilePickerCommand =>
            _filePickerCommand ?? (_filePickerCommand = new DelegateCommand(ExecuteFilePickerCommand));


        private DelegateCommand<Drawing> _pdfPreviewCommand;
        public DelegateCommand<Drawing> PdfPreviewCommand =>
            _pdfPreviewCommand ?? (_pdfPreviewCommand = new DelegateCommand<Drawing>(ExecutePdfPreviewCommand));




        public DrawingsViewModel(IWindowsDialogService windowsDialogService,
            IAppTask appTask ,AppSettings settings) 
            : base(windowsDialogService, null)
        {
            _windowsDialogService = windowsDialogService;
            _appTask = appTask;
            _appSettings = settings;
 
        }



        void ExecutePdfPreviewCommand(Drawing parameter)
        {
            _appTask.RunAsync(() => { parameter.PlotPreview(); }, Resx.LoadingPdfPreview);
        }

        private void ExecuteFilePickerCommand()
        {

            _windowsDialogService.ShowOpenFileDialog(Resx.SelectDXF, DrawingConstants.DXF_DIALOG_FILTER, s =>
            {
                if (s.Result == Prism.Services.Dialogs.ButtonResult.OK)
                {
                    foreach (var path in s.Parameters.GetValue<List<string>>("parameter"))
                    {
                        SelectedWord.Drawings.Add(new Drawing(path, Path.GetFileNameWithoutExtension(SelectedWord.NcProgram.ProgramPath)));
                    }
                }

            },_appSettings.DirectoriesSettings.DrawingTemplatesDir, true);
        }

    }
}
