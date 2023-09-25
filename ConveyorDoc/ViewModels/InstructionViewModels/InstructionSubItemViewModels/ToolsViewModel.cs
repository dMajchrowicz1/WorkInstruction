using ConveyorDoc.Business.Queries;
using ConveyorDoc.Core;
using ConveyorDoc.Interfaces;
using ConveyorDoc.Views.InstructionViews.InstructionSubViews.Dialogs;

namespace ConveyorDoc.ViewModels.InstructionViewModels.InstructionSubItemViewModels
{
    public class ToolsViewModel : SubItemBase<ToolDto>
    {
            
        public ILoadInstructionCommand LoadInstructionCommands { get; }

        public IApplicationCommands AppCommands { get; }


        public ToolsViewModel(IWindowsDialogService windowsDialogService, 
            IApplicationCommands applicationCommands, 
            ILoadInstructionCommand loadInstruction) 
            : base(windowsDialogService, typeof(SelectToolDialog))
        {

            AppCommands = applicationCommands;

            LoadInstructionCommands = loadInstruction;
            
        }

    }
}
