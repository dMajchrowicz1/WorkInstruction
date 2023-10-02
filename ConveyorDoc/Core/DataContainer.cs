using ConveyorDoc.Business.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Core
{
    public class DataContainer : IDataContainer
    {
        private readonly IAppTask _appTask;
        private readonly IGetAllModuleTypes _moduleTypesQuery;
        private readonly IGetAllMaterialsQuery _materialsQuery;



        public IEnumerable<string> Materials { get; private set; }

        public IEnumerable<string> ModuleTypes { get; private set; }


        public DataContainer(IGetAllMaterialsQuery materialsQuery, IGetAllModuleTypes moduleTypesQuery, IAppTask appTask)
        {
            _appTask = appTask;
            _materialsQuery = materialsQuery;
            _moduleTypesQuery = moduleTypesQuery;
        }

        public void Init()
        {
            _appTask.RunAsync(() =>
            {
                Materials = _materialsQuery.GetMaterials();

            }, Resources.Properties.Resources.LoadingMaterials);

            _appTask.RunAsync(() =>
            {
                ModuleTypes = _moduleTypesQuery.GetAllModuleTypes();

            }, Resources.Properties.Resources.LoadingModuleTypes);
        }
    }
}
