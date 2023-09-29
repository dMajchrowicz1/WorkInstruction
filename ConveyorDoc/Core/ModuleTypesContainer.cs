using ConveyorDoc.Business.Extension;
using ConveyorDoc.Business.Queries;
using ConveyorDoc.Descriptions.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConveyorDoc.Core
{
    public class ModuleTypesContainer : BindableBase , IModuleTypesContainer
    {
        private ObservableCollection<string> _moduleTypes = new ObservableCollection<string>();
        public ObservableCollection<string> ModuleTypes
        {
            get { return _moduleTypes; }
            set { SetProperty(ref _moduleTypes, value); }
        }

        public ModuleTypesContainer(IGetAllModuleTypes query, IAppTask appTask)
        {
            appTask.RunAsync(() => 
            {
                return query.GetAllModuleTypes();

            }, Resources.Properties.Resources.LoadingModuleTypes , (status, data) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ModuleTypes.Replace(data as IEnumerable<string>);
                });

            });
        }
    }
}
