using ConveyorDoc.Descriptions.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Descriptions.ViewModels
{
    public class DescriptionFiltersViewModel : BindableBase
    {
        private DescriptionRecord _filter = new DescriptionRecord();
        public DescriptionRecord Filter
        {
            get { return _filter; }
            set { SetProperty(ref _filter, value); }
        }

        public DescriptionViewModelBase ViewModelBase { get; }


        public DescriptionFiltersViewModel(DescriptionViewModelBase viewModelBase)
        {
            ViewModelBase = viewModelBase;

            ViewModelBase.DescriptionCollection.Filter = new Predicate<object>(Filter.Contains);
            Filter.PropertyChanged += Filter_PropertyChanged;
        }

        private void Filter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ViewModelBase.DescriptionCollection.Refresh();
        }
    }
}
