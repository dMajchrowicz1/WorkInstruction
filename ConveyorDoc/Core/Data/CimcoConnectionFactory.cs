using ConveyorDoc.Core.Database;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;

namespace ConveyorDoc.Core.Data
{
    public class CimcoConnectionFactory :  ICimcoConnectionFactory
    {
        private string _connectionString;
        private SqlConnection _connection;


        public CimcoConnectionFactory(string connectionString) 
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetOpenConnection()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                _connection = new SqlConnection(_connectionString);
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
