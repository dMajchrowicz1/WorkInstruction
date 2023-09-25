using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Descriptions.Model
{
    public abstract class RecordBase: BindableBase
    {
        public abstract bool Contains(object obj);
    }
}
