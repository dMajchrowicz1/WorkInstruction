using ConveyorDoc.Business.Queries;
using ConveyorDoc.Core;
using ConveyorDoc.Interfaces;
using ConveyorDoc.Views.InstructionViews.InstructionSubViews.Dialogs;

namespace ConveyorDoc.ViewModels.InstructionViewModels.InstructionSubItemViewModels
{
    public class FixturesViewModel : SubItemBase<FixtureDto>
    {
       
        public IApplicationCommands AppCommands { get; }

        public FixturesViewModel(IWindowsDialogService windowsDialogService, IApplicationCommands applicationCommands) 
            : base(windowsDialogService, typeof(SelectFixtureDialog))
        {
            AppCommands = applicationCommands;
        }



    }
}
