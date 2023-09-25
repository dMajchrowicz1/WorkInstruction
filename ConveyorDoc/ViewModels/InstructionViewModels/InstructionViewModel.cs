using ConveyorDoc.Business.Queries;
using ConveyorDoc.Core;
using ConveyorDoc.Interfaces;
using ConveyorDoc.Interfaces.Commands;


namespace ConveyorDoc.ViewModels.InstructionViewModels
{
    public class InstructionViewModel
    {
        public IApplicationCommands AppCommands { get; }

        public InstructionViewModelBase ViewModelBase { get; }

        public IInstructionCommands Commands {get;}
        
        public InstructionViewModel(
            IApplicationCommands applicationCommands,
            IInstructionCommands instructionCommands,
            InstructionViewModelBase viewModelBase) 
        {


            AppCommands = applicationCommands;
            Commands = instructionCommands;
            ViewModelBase = viewModelBase;


        }


    }
}
