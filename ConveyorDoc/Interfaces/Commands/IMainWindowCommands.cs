using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Interfaces.Commands
{
    public interface IMainWindowCommands
    {
        DelegateCommand<object> NavigateCommand { get; }

        DelegateCommand NewWorkInstructionCommand { get; }

        DelegateCommand OpenWorkInstructionCommand { get; }

        DelegateCommand SaveWorkInstructionCommand { get; }
    }
}
