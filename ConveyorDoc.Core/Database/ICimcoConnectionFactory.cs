using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Core.Database
{
    public interface ICimcoConnectionFactory 
    {
        SqlConnection GetOpenConnection();

        void Dispose();
    }
}
