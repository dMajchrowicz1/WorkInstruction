using Prism.Commands;
using System.Collections.Generic;

namespace ConveyorDoc.Interfaces.Commands
{
    public interface IDataGridCommand<Tmodel>
    {
        List<Tmodel> SelectedItems { get; }
        DelegateCommand<object> SelectionCommand { get; }
    }
}