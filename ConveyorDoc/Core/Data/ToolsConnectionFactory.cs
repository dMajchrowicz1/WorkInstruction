using ConveyorDoc.Core.Database;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Core.Data
{
    public class ToolsConnectionFactory : ConnectionFactoryBase<OleDbConnection>, IToolsConnectionFactory
    {
        public ToolsConnectionFactory(string connectionString) : base(connectionString)
        {
        }
    }
}
