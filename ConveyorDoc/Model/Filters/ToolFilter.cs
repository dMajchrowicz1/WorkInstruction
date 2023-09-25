using ConveyorDoc.Fixtures.Model;
using Prism.Mvvm;
using ConveyorDoc.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConveyorDoc.Business.Queries;

namespace ConveyorDoc.Model.Filters
{
    public class ToolFilter : BindableBase
    {
        private string _offset;
        public string Offset
        {
            get { return _offset ?? (_offset = string.Empty); }
            set { SetProperty(ref _offset, value); }
        }

        private string _machine;
        public string Machine
        {
            get { return _machine ?? (_machine = string.Empty); }
            set { SetProperty(ref _machine, value); }
        }

        private string _type;
        public string Type
        {
            get { return _type ?? (_type = string.Empty); }
            set { SetProperty(ref _type, value); }
        }

        public bool Contains(object obj)
        {
            if (obj is ToolDto Tool)
            {
                return
                    Tool.Offset.Filter(Offset) &&
                    Tool.Type.Filter(Type) &&
                    Tool.Machine.Filter(Machine);
            }
            else
                return false;
        }
    }
}
