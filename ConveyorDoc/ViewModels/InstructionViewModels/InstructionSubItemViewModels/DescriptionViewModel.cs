
using ConveyorDoc.Business.Queries;
using ConveyorDoc.Core;
using ConveyorDoc.Interfaces;
using ConveyorDoc.Views.InstructionViews.InstructionSubViews.Dialogs;


namespace ConveyorDoc.ViewModels.InstructionViewModels.InstructionSubItemViewModels
{
    public class DescriptionViewModel : SubItemBase<DescriptionDto>
    {
        public ILoadInstructionCommand LoadInstructionCommands { get; }


        public DescriptionViewModel(IWindowsDialogService windowsDialogService, ILoadInstructionCommand loadInstruction) 
            :base(windowsDialogService, typeof(SelectDescriptionDialog))
        {
            LoadInstructionCommands = loadInstruction;
            
        }


    }
}
