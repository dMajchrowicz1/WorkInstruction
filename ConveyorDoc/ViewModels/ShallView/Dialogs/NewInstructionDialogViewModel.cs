using ConveyorDoc.Core;
using ConveyorDoc.Descriptions.Interfaces;
using ConveyorDoc.Model.Settings;
using ToastNotifications;

namespace ConveyorDoc.ViewModels.ShallView.Dialogs
{
    public class NewInstructionDialogViewModel : InstructionDialogBase
    {


        public NewInstructionDialogViewModel(Notifier notifier, IDataContainer data) 
            : base($"{Resources.Properties.Resources.Create} {Resources.Properties.Resources.Instruction}",
                  Resources.Properties.Resources.Create, 
                  notifier,data) { }
             

    }
}
