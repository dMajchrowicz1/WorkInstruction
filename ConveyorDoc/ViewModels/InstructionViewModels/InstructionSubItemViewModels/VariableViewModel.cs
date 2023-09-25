using ConveyorDoc.Business.Model;
using ConveyorDoc.Core;
using ConveyorDoc.Interfaces;
using ConveyorDoc.Model;

namespace ConveyorDoc.ViewModels.InstructionViewModels.InstructionSubItemViewModels
{
    public class VariableViewModel : SubItemBase<Variable>
    {
        public ILoadInstructionCommand LoadInstructionCommands { get; }
       
        public VariableViewModel(IWindowsDialogService windowsDialogService, ILoadInstructionCommand loadInstruction)
            : base(windowsDialogService, null)
        {

            LoadInstructionCommands = loadInstruction;
        }


    }
}
