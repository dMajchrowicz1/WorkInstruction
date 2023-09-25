using ConveyorDoc.Core.Database;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Core.Data
{
    public class DecanterConnectionFactory : ConnectionFactoryBase<OleDbConnection>, IDecanterConncetionFactory
    {
        public DecanterConnectionFactory(string connectionString) : base(connectionString)
        {

        }
    }
}
