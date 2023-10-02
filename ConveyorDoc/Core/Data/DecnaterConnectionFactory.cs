using ConveyorDoc.Core.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Core.Data
{
    public class DecanterConnectionFactory : IDecanterConncetionFactory
    {

        private OleDbConnection _connection;
        private string _connectionString;

        public DecanterConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public OleDbConnection GetOpenConnection()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                _connection = new OleDbConnection(_connectionString);
                _connection.Open();
            }

            return _connection;
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Dispose();
            }
        }
    }
}
