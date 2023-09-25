using ConveyorDoc.Core.Extension;
using ConveyorDoc.Fixtures.Interfaces;
using ConveyorDoc.Fixtures.Model;
using Prism.Mvvm;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Fixtures.ViewModels
{
    public class FixtureFiltersViewModel : BindableBase
    {

       
        private FixtureRecord _filter = new FixtureRecord();
        public FixtureRecord Filter
        {
            get { return _filter; }
            set { SetProperty(ref _filter, value); }
        }


        public FixtureViewModelBase ViewModelBase { get; }


        public FixtureFiltersViewModel(FixtureViewModelBase viewModelBase)
        {
            ViewModelBase = viewModelBase;    
            ViewModelBase.FixtureCollection.Filter = new Predicate<object>(Filter.Contains);
            
            Filter.PropertyChanged += Filter_PropertyChanged;
        }



        private void Filter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
           ViewModelBase.FixtureCollection.Refresh();
        }
    }
}
