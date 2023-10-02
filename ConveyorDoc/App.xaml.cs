
using ConveyorDoc.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Windows;
using System.Windows.Controls;
using ConveyorDoc.Core.Regions;
using ConveyorDoc.Views.Dialogs;
using ConveyorDoc.ViewModels.Dialogs;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.IO;
using ConveyorDoc.ViewModels.ShallView.Dialogs;
using ConveyorDoc.Interfaces;
using ConveyorDoc.Views.Settings;
using ConveyorDoc.Views.Programs;
using ConveyorDoc.Settings.ViewModels;
using ConveyorDoc.ViewModels.InstructionViewModels;
using ConveyorDoc.Views.InstructionViews.TreeViewContent;
using ConveyorDoc.ViewModels.InstructionViewModels.InstructionSubItemViewModels;
using ConveyorDoc.Views.InstructionViews.InstructionSubViews.Dialogs;
using ConveyorDoc.ViewModels.InstructionViewModels.InstructionSubItemViewModels.Dialogs;
using ConveyorDoc.Views.InstructionViews.InstructionSubViews;
using ConveyorDoc.ViewModels.Commands;
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using ConveyorDoc.Views.InstructionViews;
using ConveyorDoc.ViewModels.InstructionViewModels.Dialogs;
using System.Configuration;
using ConveyorDoc.Model.Settings;
using Prism.Mvvm;
using ConveyorDoc.ViewModels;
using ConveyorDoc.Interfaces.Commands;
using ConveyorDoc.Core.Database;
using ConveyorDoc.Core.Data;
using ConveyorDoc.Fixtures;
using ConveyorDoc.Core;
using ConveyorDoc.Descriptions;
using ConveyorDoc.Business.Queries;
using ConveyorDoc.Services.QueryHandlers;
using ConveyorDoc.Interfaces.Services;
using ConveyorDoc.Services;
using ConveyorDoc.Descriptions.Interfaces;

namespace ConveyorDoc
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();           
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

            //InitNotifer
            containerRegistry.RegisterInstance(new Notifier(cfg =>
            {
                var mainWindow = Current.MainWindow;

                cfg.DisplayOptions.Width = 350;

                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: mainWindow,
                    corner: Corner.BottomRight,
                    offsetX: 20,
                    offsetY: 20);


                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                        notificationLifetime: TimeSpan.FromSeconds(5),
                        maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            }));


            containerRegistry.RegisterSingleton<IToastMessage, ToastMessage>();
            containerRegistry.RegisterSingleton<IAppActivationService, AppActivationService>();
            containerRegistry.RegisterSingleton<AppTaskManager>();
            containerRegistry.RegisterSingleton<IAppTaskManager, AppTaskManager>();
            containerRegistry.RegisterSingleton<IAppTask, AppTaskManager>();
            containerRegistry.RegisterSingleton<IActiveTasks, AppTaskManager>();
            containerRegistry.Register<IWindowsDialogService, WindowsDialogService>();
            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();
            containerRegistry.RegisterSingleton<ILoadInstructionCommand, LoadInstructionCommands>();
            containerRegistry.RegisterSingleton<IInstructionCommands , InstructionCommands>();
            containerRegistry.RegisterSingleton<IMainWindowCommands, MainWindowCommands>();
            containerRegistry.RegisterSingleton<IDataContainer, DataContainer>();
            


            containerRegistry.RegisterSingleton<InstructionViewModelBase>();



            //Register app settings as singleton and loads user settings
            containerRegistry.RegisterInstance(AppSettings.Load());
            containerRegistry.RegisterInstance<IAppDirectories>(Container.Resolve<AppSettings>().DirectoriesSettings);



            //Repositories
            containerRegistry.RegisterInstance<IDecanterConncetionFactory>(
                new DecanterConnectionFactory(ConfigurationManager.ConnectionStrings["DecanterDb"].ConnectionString));
            containerRegistry.RegisterInstance<ICimcoConnectionFactory>(
                new CimcoConnectionFactory(ConfigurationManager.ConnectionStrings["CimcoDb"].ConnectionString));
            containerRegistry.RegisterInstance<IToolsConnectionFactory>(
                 new ToolsConnectionFactory(ConfigurationManager.ConnectionStrings["ToolListDb"].ConnectionString));



            //Queries
            containerRegistry.RegisterSingleton<DecanterDatabaseQueryHandlers>();
            containerRegistry.RegisterSingleton<CimcoDatabaseQueryHandlers>();
            containerRegistry.RegisterSingleton<ToolsDatabaseQueryHandlers>();

            containerRegistry.RegisterSingleton<IGetAllMaterialsQuery, DecanterDatabaseQueryHandlers>();
            containerRegistry.RegisterSingleton<IGetAllToolsQuery, ToolsDatabaseQueryHandlers>();
            containerRegistry.RegisterSingleton<IGetToolQuery, ToolsDatabaseQueryHandlers>();
            containerRegistry.RegisterSingleton<IGetAllFixturesQuery, DecanterDatabaseQueryHandlers>();
            containerRegistry.RegisterSingleton<IGetModuleProgramsQuery, CimcoDatabaseQueryHandlers>();
            containerRegistry.RegisterSingleton<IGetAllDescriptionsQuery, DecanterDatabaseQueryHandlers>();
            containerRegistry.RegisterSingleton<IGetAllModuleTypes, DecanterDatabaseQueryHandlers>();


            //Dialogs
            ViewModelLocationProvider.Register<NewInstructionDialog, NewInstructionDialogViewModel>();
            ViewModelLocationProvider.Register<AskForSave, AskForSaveViewModel>();
            ViewModelLocationProvider.Register<InstructionDataDialog, InstructionDataDialogViewModel>();

            ViewModelLocationProvider.Register<SelectToolDialog, SelectToolDialogViewModel>();
            ViewModelLocationProvider.Register<SelectFixtureDialog, SelectFixtureDialogViewModel>();
            ViewModelLocationProvider.Register<SelectDescriptionDialog, SelectDescriptionDialogViewModel>();
            ViewModelLocationProvider.Register<SelectCimcoProgramDialog, SelectModuleProgramDialogViewModel>();



            //Navigation Cards
            containerRegistry.RegisterForNavigation<InstructionView, InstructionViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<ToolsItemView, ToolsViewModel>();
            containerRegistry.RegisterForNavigation<VariablesItemView, VariableViewModel>();
            containerRegistry.RegisterForNavigation<DrawingsItemView, DrawingsViewModel>();
            containerRegistry.RegisterForNavigation<DescriptionItemView, DescriptionViewModel>();
            containerRegistry.RegisterForNavigation<FixturesItemView, FixturesViewModel>();

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<FixturesModule>();
            moduleCatalog.AddModule<DescriptionsModule>();
        }
        

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(StackPanel), Container.Resolve<NavigationToolBarAdapter>());
        }
    }
}
