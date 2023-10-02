using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Core.Data
{
    public class ConnectionFactoryBase<T> where T : DbConnection, IDbConnection, IDisposable
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

        

        public ConnectionFactoryBase(string connectionString)
        {
            _connectionString = connectionString;

        }

        public IDbConnection GetOpenConnection()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                _connection = Activator.CreateInstance<T>();
                _connection.ConnectionString = _connectionString;
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
