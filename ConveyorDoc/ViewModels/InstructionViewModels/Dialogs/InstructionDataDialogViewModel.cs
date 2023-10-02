using ConveyorDoc.Core;
using ConveyorDoc.Descriptions.Interfaces;
using ConveyorDoc.Interfaces;
using ConveyorDoc.Model.Settings;
using ConveyorDoc.ViewModels.ShallView.Dialogs;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToastNotifications;

namespace ConveyorDoc.ViewModels.InstructionViewModels.Dialogs
{
    public class InstructionDataDialogViewModel : InstructionDialogBase
    {

        public InstructionDataDialogViewModel(Notifier notifier, IDataContainer data) 
            : base(Resources.Properties.Resources.InstructionData, 
                  Resources.Properties.Resources.Save, 
                  notifier,
                  data)
        {
            
        }


    }
}
