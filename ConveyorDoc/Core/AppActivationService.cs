using ConveyorDoc.Business.Constants;
using ConveyorDoc.Business.Model;
using ConveyorDoc.Core.Database;
using ConveyorDoc.Interfaces;
using ConveyorDoc.Model.Settings;
using ConveyorDoc.ViewModels.InstructionViewModels;
using System;
using System.IO;
using System.Linq;
using ToastNotifications;

namespace ConveyorDoc.Core
{
    public class AppActivationService : IAppActivationService
    {
        private AppSettings _settings;
        private Notifier _notifier;
        private InstructionViewModelBase _viewModelBase;
        private readonly ICimcoConnectionFactory _cimcoConnectionFactory;
        private readonly IDecanterConncetionFactory _decanterConncetionFactory;
        private readonly IToolsConnectionFactory _toolConnectionFactory;


        public AppActivationService(
            IDecanterConncetionFactory decanterConncetionFactory,
            ICimcoConnectionFactory cimcoConnectionFactory,
            IToolsConnectionFactory toolsConnectionFacotry,
            IMaterialContainer materialContainer,
            IModuleTypesContainer moduleTypesContainer,
            InstructionViewModelBase viewModelBase, 
            Notifier notifier,
            AppSettings settings)
        {
            _notifier = notifier;
            _viewModelBase = viewModelBase;
            _decanterConncetionFactory = decanterConncetionFactory;
            _cimcoConnectionFactory = cimcoConnectionFactory;
            _toolConnectionFactory = toolsConnectionFacotry;
            _settings = settings;

        }

        public void Activate()
        {
            
            ReadExecutableFile();
            OpenConnectionSources();
            _settings.GeneralSettings.InitSettings();
        }

        public void Deactivate()
        {
            RemoveAllTempFiles();
            _notifier.Dispose();
            _decanterConncetionFactory.Dispose();
            _cimcoConnectionFactory.Dispose();
            _toolConnectionFactory.Dispose();
        }


        private void ReadExecutableFile()
        {
            if (Environment.GetCommandLineArgs().Count() == 2)
            {
                _viewModelBase.CurrentInstruction  = Instruction.Load(Environment.GetCommandLineArgs()[1]);
            }
        }

        private void RemoveAllTempFiles()
        {
            foreach (var item in Directory.GetFiles(GeneralConstants.TEMP_FOLDER_DIR))
            {
                File.Delete(item);               
            }
        }

        private void OpenConnectionSources()
        {
            _decanterConncetionFactory.GetOpenConnection();
            _cimcoConnectionFactory.GetOpenConnection();
            _toolConnectionFactory.GetOpenConnection(); 
        }


    }
}
