using ConveyorDoc.Descriptions.Interfaces;
using ConveyorDoc.Descriptions.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ConveyorDoc.Descriptions.ViewModels
{
    public class DescriptionContentViewModel : BindableBase
    {


        private DelegateCommand<object> _closeRowDetailsCommand;
        public DelegateCommand<object> CloseRowDetailsCommand =>
            _closeRowDetailsCommand ?? (_closeRowDetailsCommand = new DelegateCommand<object>(ExecuteCloseRowDetailsCommand));


        public DescriptionViewModelBase ViewModelBase { get; }


        public DescriptionContentViewModel(DescriptionViewModelBase viewModelBase)
        {
            ViewModelBase = viewModelBase;
          
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
