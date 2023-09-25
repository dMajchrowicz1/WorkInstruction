using ConveyorDoc.Core.Database;
using ConveyorDoc.Descriptions.Interfaces;
using ConveyorDoc.Descriptions.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Descriptions.Repository
{
    public class DescriptionRepository : IDescriptionRepository
    {

        private readonly IDbConnection _connection;

        public DescriptionRepository(IDecanterConncetionFactory oleDbConncetion)
        {
            _connection = oleDbConncetion.GetOpenConnection();
        }

        public void Update(DescriptionRecord entity)
        {

            string query = @"Update Descriptions set
                                                                [Size] = @Size,
                                                                [ModuleType] = @ModuleType,
                                                                [Machine] = @Machine,
                                                                [OperationNumber] = @OperationNumber,
                                                                [Text] = @Text
                                                            WHERE [ID] = @ID";

            _connection.Execute(query, new
            {

                Size = entity.Size,
                ModuleType = entity.ModuleType,
                Machine = entity.Machine,
                OperationNumber = entity.OperationNumber,
                Text = entity.Text,
                ID = entity.ID
            });

        }

        public void Insert(DescriptionRecord entity)
        {
            string query = @"Insert into Descriptions ([Size],[ModuleType],[Machine],[OperationNumber],[Text]) 
                                        values (@Size,@ModuleType,@Machine,@OperationNumber,@Text)";


            _connection.Execute(query, new
            {
                Size = entity.Size,
                ModuleType = entity.ModuleType,
                Machine = entity.Machine,
                OperationNumber = entity.OperationNumber,
                Text = entity.Text
            });
        }

        public void Delete(DescriptionRecord entity)
        {

            _connection.Execute("DELETE FROM Descriptions WHERE [ID]=@ID", new { ID = entity.ID });
        }

        public IEnumerable<DescriptionRecord> GetAll()
        {
            var result = Enumerable.Empty<DescriptionRecord>();

            result = _connection.Query<DescriptionRecord>("SELECT * FROM Descriptions");

            return result;


        }
    }
}
