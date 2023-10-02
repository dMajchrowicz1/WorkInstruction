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
        private readonly IDecanterConncetionFactory _connectionFactory;

        private  IDbConnection _connection;

        public DecanterDatabaseQueryHandlers(IDecanterConncetionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<DescriptionDto> GetAllDescriptions()
        {
            CheckConnection();

            var result = Enumerable.Empty<DescriptionDto>();

            result = _connection.Query<DescriptionDto>("SELECT * FROM Descriptions");

            return result;
        }

        public IEnumerable<FixtureDto> GetAllFixtures()
        {
            CheckConnection();

            var result = Enumerable.Empty<FixtureDto>();

            result = _connection.Query<FixtureDto>("SELECT * FROM Fixtures");

            return result;
        }
       
        public IEnumerable<string> GetMaterials()
        {
            CheckConnection();

            var result = Enumerable.Empty<string>();

            result = _connection.Query<string>("SELECT Material FROM Materials");

            return result;
        }
            
        public IEnumerable<string> GetAllModuleTypes()
        {
            CheckConnection();

            var result = Enumerable.Empty<string>();

            result = _connection.Query<string>("SELECT ModuleType FROM ModuleTypes");

            return result;
        }

        private void CheckConnection()
        {
            if (_connection == null) 
            {
                _connection = _connectionFactory.GetOpenConnection();
            }
        }

    }
}
