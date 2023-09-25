using ConveyorDoc.Fixtures.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;
using ConveyorDoc.Core.Database;
using System.Drawing;
using ConveyorDoc.Fixtures.Model;
using System.Data;

namespace ConveyorDoc.Fixtures.Repository
{
    public class FixtureRepository : IFixtureRepository
    {
        private readonly IDbConnection _connection;

        public FixtureRepository(IDecanterConncetionFactory oleDbConncetion)
        {
            _connection = oleDbConncetion.GetOpenConnection();
        }

        public void Insert(FixtureRecord entity)
        {

            string queryString = @"INSERT INTO Fixtures 
                                                ([ItemNumber],
                                                [Size],
                                                [CreateDate],
                                                [ItemType],
                                                [Machine],
                                                [CreatedBy],
                                                [Details],
                                                [PDF])
                                      VALUES 
                                               (@ItemNumber,
                                               @Size,
                                               @CreateDate,
                                               @ItemType,
                                               @Machine,
                                               @CreatedBy,
                                               @Details,
                                               @PDF)";
        

                _connection.Execute(queryString, new
                {
                    ItemNumber = entity.ItemNumber,
                    Size = entity.Size,
                    ItemType = entity.ItemType,
                    Machine = entity.Machine,
                    CreatedBy = entity.CreatedBy,
                    CreateDate = entity.CreateDate,
                    Details = entity.Details,
                    PDF = entity.PDF,
                    Id = entity.Id,
                });
            
        }

        public void Update(FixtureRecord entity)
        {

            string queryString = @"UPDATE Fixtures SET 
                                              [ItemNumber]=@ItemNumber,
                                              [Size]=@Size,
                                              [ItemType]=@ItemType,
                                              [Machine]=@Machine,
                                              [Details]=@Details,
                                              [PDF]=@PDF,
                                              [ModifiedBy]=@ModifiedBy,
                                              [ModificationDate]=@ModificationDate
                                   WHERE [Id] = @Id";
            

                _connection.Execute(queryString, new
                {
                    entity.ItemNumber,
                    entity.Size,
                    entity.ItemType,
                    entity.Machine,
                    entity.Details,
                    entity.PDF,
                    entity.ModifiedBy,
                    ModificationDate = entity.ModificationDate,
                    entity.Id,

                });
              
        }

        public void Delete(FixtureRecord entity)
        {

            string query = "DELETE FROM Fixtures WHERE [ID] = @id";

            _connection.Execute(query, new { ID = entity.Id });

        }

        public IEnumerable<FixtureRecord> GetAll()
        {
            var result = Enumerable.Empty<FixtureRecord>();

            result = _connection.Query<FixtureRecord>("SELECT * FROM Fixtures");

            return result;
        }

        public int GetEditMode(int id)
        {
            string query = $@"SELECT [EditMode] FROM Fixtures WHERE [ID] = @id";

            return _connection.QuerySingle<int>(query, new { ID = id });
        }

        public IEnumerable<string> GetAllMachines()
        {
            var result = Enumerable.Empty<string>();

            string query = "SELECT DISTINCT [Machine] FROM Fixtures";

            result = _connection.Query<string>(query);

            return result;
        }

        public IEnumerable<string> GetAllSizes()
        {
            var result = Enumerable.Empty<string>();

            string query = "SELECT DISTINCT [Size] FROM Fixtures";

            result = _connection.Query<string>(query);

            return result;
        }

        public IEnumerable<string> GetAllTypes()
        {
            var result = Enumerable.Empty<string>();

            string query = "SELECT DISTINCT [ItemType] FROM Fixtures";

            result = _connection.Query<string>(query);

            return result;
        }

        public void LockEditMode(int id)
        {
            string query = $@"INSERT INTO Fixtures (EditMode) VALUES (@EditMode) WHERE [ID] = @id";

            _connection.QuerySingle<int>(query, new { ID = id, EditMode = 1 });
        }
    }
}
