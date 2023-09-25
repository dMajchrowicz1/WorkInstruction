using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConveyorDoc.Business.Model;


namespace ConveyorDoc.Business.Queries
{
    public interface IGetModuleProgramsQuery
    {
        IEnumerable<NcProgram> GetModulePrograms(string moduleNumber);
    }
}
