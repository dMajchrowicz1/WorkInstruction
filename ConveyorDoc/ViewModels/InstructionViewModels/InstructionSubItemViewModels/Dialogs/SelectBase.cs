using ConveyorDoc.ViewModels.Commands;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ConveyorDoc.ViewModels
{
    public abstract class SelectBase<TDto, TFilter> : BindableBase
    {

        public abstract ICollectionView  Collection { get;  set; }

        private TFilter _filter;
        public TFilter Filter
        {
            get { return _filter; }
            set { SetProperty(ref _filter, value); }
        }

        private DelegateCommand<object> _closeRowDetailsCommand;
        public DelegateCommand<object> CloseRowDetailsCommand =>
            _closeRowDetailsCommand ?? (_closeRowDetailsCommand = new DelegateCommand<object>(ExecuteCloseRowDetailsCommand));

        public SelectBase(TFilter filter)
        {
            _filter = filter;
        }


        private void ExecuteCloseRowDetailsCommand(object parameter)
        {
            if (parameter is DataGrid grid)
            {
                grid.SelectedIndex = -1;
            }
        }

    }
}
