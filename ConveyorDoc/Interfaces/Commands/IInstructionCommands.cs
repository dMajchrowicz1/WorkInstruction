
using Prism.Commands;
using System.Windows;

namespace ConveyorDoc.Interfaces.Commands
{
    public interface IInstructionCommands
    {
        DelegateCommand AddInstructionCommand { get; }

        DelegateCommand<object> NavigateInstructionSubItemsCommand { get; }

        DelegateCommand<object> RemoveInstructionCommand { get; }

        DelegateCommand GenerateInstructionCommand { get; }

        DelegateCommand<DragEventArgs> DropInstructionCommand { get; }

        DelegateCommand<object> LoadAllCommand { get; }

        DelegateCommand ChangeInstructionDataCommand { get; }



    }
}
