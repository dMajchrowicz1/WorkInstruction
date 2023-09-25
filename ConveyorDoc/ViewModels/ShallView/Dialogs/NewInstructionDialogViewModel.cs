using ConveyorDoc.Core;
using ConveyorDoc.Descriptions.Interfaces;
using ToastNotifications;

namespace ConveyorDoc.ViewModels.ShallView.Dialogs
{
    public class NewInstructionDialogViewModel : InstructionDialogBase
    {


        public NewInstructionDialogViewModel(Notifier notifier, IMaterialContainer materialContainer, IModuleTypesContainer ModuleTypesContainer) 
            : base($"{Resources.Properties.Resources.Create} {Resources.Properties.Resources.Instruction}",
                  Resources.Properties.Resources.Create, 
                  notifier,materialContainer,ModuleTypesContainer) { }
             

    }
}
