using ConveyorDoc.Fixtures.Interfaces;
using ConveyorDoc.Fixtures.Model;
using ConveyorDoc.Fixtures.Repository;
using ConveyorDoc.Fixtures.ViewModels;
using ConveyorDoc.Fixtures.ViewModels.Commands;
using ConveyorDoc.Fixtures.ViewModels.Dialogs;
using ConveyorDoc.Fixtures.Views;
using ConveyorDoc.Fixtures.Views.Dialogs;
using ConveyorDoc.Fixtures.Views.Reusable;
using ConveyorDoc.Resources.Reusable;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace ConveyorDoc.Fixtures
{
    public class FixturesModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry.RegisterSingleton<IFixtureRepository, FixtureRepository>();
            containerRegistry.RegisterSingleton<ISearchCommand,SearchCommand>();
            containerRegistry.RegisterSingleton<FixtureViewModelBase>();

            

            containerRegistry.RegisterForNavigation<FixturesView>();


        }
    }
}