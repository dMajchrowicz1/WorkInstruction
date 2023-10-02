using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Core
{
    public interface IDataContainer
    {
        IEnumerable<string> Materials { get; }

        IEnumerable<string> ModuleTypes { get; }

        void Init();
    }
}
