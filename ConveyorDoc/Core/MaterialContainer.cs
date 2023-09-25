using ConveyorDoc.Business.Queries;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Core
{
    public class MaterialContainer: BindableBase , IMaterialContainer
    {
        private ObservableCollection<string>  _materials;
        public ObservableCollection<string> Materials
        {
            get { return _materials; }
            set { SetProperty(ref _materials, value); }
        }


        public MaterialContainer(IGetAllMaterialsQuery query, IAppTask appTask)
        {

            appTask.Run(() =>
            {
                _materials = new ObservableCollection<string>(query.GetMaterials());
            }, ConveyorDoc.Resources.Properties.Resources.LoadingMaterials);           
        }
    }
}
