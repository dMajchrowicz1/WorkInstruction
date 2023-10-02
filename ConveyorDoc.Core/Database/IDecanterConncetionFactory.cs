using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Core.Database
{
    public interface IDecanterConncetionFactory 
    {
        OleDbConnection GetOpenConnection();

        void Dispose();
    }
}
