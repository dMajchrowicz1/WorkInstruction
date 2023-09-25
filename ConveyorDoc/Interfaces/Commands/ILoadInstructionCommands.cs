using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Interfaces
{
    public interface ILoadInstructionCommand
    {
        DelegateCommand<object> LoadVariablesCommand { get;  }

        DelegateCommand<object> LoadDescriptionCommand { get; }

        DelegateCommand<object> LoadToolsCommand { get; }
    }
}
