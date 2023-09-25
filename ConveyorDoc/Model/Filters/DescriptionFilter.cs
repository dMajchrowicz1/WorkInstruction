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
    public class DescriptionFilter : BindableBase
    {
        private string _size = string.Empty;
        public string Size
        {
            get { return _size ?? (_size = string.Empty); }
            set { SetProperty(ref _size, value); }
        }

        private string _machine = string.Empty;
        public string Machine
        {
            get { return _machine ?? (_machine = string.Empty); }
            set { SetProperty(ref _machine, value); }
        }

        private string _moduleType;
        public string ModuleType
        {
            get { return _moduleType ?? (_moduleType = string.Empty); }
            set { SetProperty(ref _moduleType, value); }
        }

        public  bool Contains(object obj)
        {
            if (obj != null && obj is DescriptionDto description)
            {
                return
                    description.Machine.Filter(Machine) &&
                    description.ModuleType.Filter(ModuleType) &&
                    description.Size.Filter(Size);
            }
            else return false;
        }
    }
}
