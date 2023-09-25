using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Core.Database
{
    public interface IConncetionBase
    {
        IDbConnection GetOpenConnection();

        void Dispose();
    }
}
