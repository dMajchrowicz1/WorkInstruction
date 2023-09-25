using ConveyorDoc.Business.Queries;
using ConveyorDoc.Core.Database;
using Dapper;
using Slapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Services.QueryHandlers
{
    public class DecanterDatabaseQueryHandlers : IGetAllFixturesQuery, IGetAllMaterialsQuery, IGetAllDescriptionsQuery, IGetAllModuleTypes
    {
        private readonly IDbConnection _connection;

        public DecanterDatabaseQueryHandlers(IDecanterConncetionFactory conncetionFactory)
        {
            _connection = conncetionFactory.GetOpenConnection();
        }

        public IEnumerable<DescriptionDto> GetAllDescriptions()
        {
            var result = Enumerable.Empty<DescriptionDto>();

            result = _connection.Query<DescriptionDto>("SELECT * FROM Descriptions");

            return result;
        }

        public IEnumerable<FixtureDto> GetAllFixtures()
        {
            var result = Enumerable.Empty<FixtureDto>();

            result = _connection.Query<FixtureDto>("SELECT * FROM Fixtures");

            return result;
        }
       
        public IEnumerable<string> GetMaterials()
        {
            var result = Enumerable.Empty<string>();

            result = _connection.Query<string>("SELECT Material FROM Materials");

            return result;
        }
            
        public IEnumerable<string> GetAllModuleTypes()
        {
            var result = Enumerable.Empty<string>();

            result = _connection.Query<string>("SELECT ModuleType FROM ModuleTypes");

            return result;
        }

    }
}
