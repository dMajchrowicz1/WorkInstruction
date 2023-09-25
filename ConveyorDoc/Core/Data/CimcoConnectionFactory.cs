using ConveyorDoc.Core.Database;
using System.Data.SqlClient;

namespace ConveyorDoc.Core.Data
{
    public class CimcoConnectionFactory : ConnectionFactoryBase<SqlConnection>, ICimcoConnectionFactory
    {
        public CimcoConnectionFactory(string connectionString) : base(connectionString)
        {

        }
    }
}
