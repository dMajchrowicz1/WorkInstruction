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
    public class FixtureFilter : BindableBase
    {
        private string _itemNumber = string.Empty;
        public string ItemNumber
        {
            get { return _itemNumber ?? (_itemNumber = string.Empty); }
            set { SetProperty(ref _itemNumber, value); }
        }

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

        private string _itemType = string.Empty;
        public string ItemType
        {
            get { return _itemType ?? (_itemType = string.Empty); }
            set { SetProperty(ref _itemType, value); }
        }

        public bool Contains(object obj)
        {
            if (obj is FixtureDto fixture)
            {
                return
                    fixture.ItemNumber.Filter(ItemNumber) &&
                    fixture.Size.Filter(Size) &&
                    fixture.Machine.Filter(Machine) &&
                    fixture.ItemType.Filter(ItemType);
            }
            else
                return false;
        }
    }
}
