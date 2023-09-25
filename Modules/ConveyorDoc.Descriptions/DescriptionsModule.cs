using ConveyorDoc.Descriptions.Interfaces;
using ConveyorDoc.Descriptions.Repository;
using ConveyorDoc.Descriptions.ViewModels;
using ConveyorDoc.Descriptions.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ConveyorDoc.Descriptions
{
    public class DescriptionsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<DescriptionViewModelBase>();
            containerRegistry.RegisterSingleton<IDescriptionRepository, DescriptionRepository>();


            containerRegistry.RegisterForNavigation<DescriptionView>();
        }
    }
}