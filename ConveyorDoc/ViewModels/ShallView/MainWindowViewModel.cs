
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;
using ConveyorDoc.Interfaces;
using ConveyorDoc.Interfaces.Commands;
using ConveyorDoc.ViewModels.InstructionViewModels;

namespace ConveyorDoc.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IRegionManager _regionManager;

        private IAppActivationService _appActivationService;

        public IMainWindowCommands Commands { get; }


        public MainWindowViewModel(
            IRegionManager regionManager, 
            IAppActivationService appActivationService,
            IMainWindowCommands commands) 
        {

            _regionManager = regionManager;
            _appActivationService = appActivationService;
            Commands = commands;

            

            Application.Current.MainWindow.Loaded += MainWindow_Loaded;

            Application.Current.Exit += Current_Exit;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Initialize();
        }


        private void Current_Exit(object sender, ExitEventArgs e)
        {
            _appActivationService.Deactivate();
        }

        private void Initialize()
        {

            //Navigate to instruction view on open 
            _regionManager.RegisterViewWithRegion("ContentRegion", "InstructionView");

            //Activate app services
            _appActivationService.Activate();
        }


    }
}
