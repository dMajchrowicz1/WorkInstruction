using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Fixtures.ViewModels
{
    public class FixtureNavigatorViewModel : BindableBase
    {

        private FixtureViewModelBase _viewModelBase;
        public FixtureViewModelBase ViewModelBase
        {
            get { return _viewModelBase; }
            set { SetProperty(ref _viewModelBase, value); }
        }



        public FixtureNavigatorViewModel(FixtureViewModelBase viewModelBase)
        {
            _viewModelBase = viewModelBase;
        }
    }
}
