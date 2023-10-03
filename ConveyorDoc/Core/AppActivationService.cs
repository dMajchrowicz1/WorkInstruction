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
        private readonly IAppTask _appTask;
        private readonly IDataContainer _container;


        public AppActivationService(
            IDecanterConncetionFactory decanterConncetionFactory,
            ICimcoConnectionFactory cimcoConnectionFactory,
            IToolsConnectionFactory toolsConnectionFacotry,
            IAppTask appTask,
            IDataContainer container,
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
            _appTask = appTask;
            _container = container;


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
            //Opening connection to decanter db 
            _appTask.RunAsync(() =>
            {
                _decanterConncetionFactory.GetOpenConnection();
                               
            }, ConveyorDoc.Resources.Properties.Resources.ConnectingDecanter, callbackStatus=>
            {
                if(callbackStatus == System.Threading.Tasks.TaskStatus.RanToCompletion)
                {
                    //When status is ok, loading data from database
                    _container.Init();
                }


                //Tools connection has to wait until first oleDbConnection finish
                //Can only open one connection at time
                _appTask.RunAsync(() =>
                {
                    _toolConnectionFactory.GetOpenConnection();

                }, ConveyorDoc.Resources.Properties.Resources.ConnectionTools);
            });

            //Opening connection to cimco db 
            _appTask.RunAsync(() =>
            {
                _cimcoConnectionFactory.GetOpenConnection();

            }, ConveyorDoc.Resources.Properties.Resources.ConnectingCimco);
        }

    }
}
