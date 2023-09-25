using ConveyorDoc.Core;
using ConveyorDoc.Interfaces;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace ConveyorDoc.ViewModels.Dialogs
{
    public class AskForSaveViewModel : BindableBase, IDialogAware2
    {
        public string Title => Resources.Properties.Resources.SaveInstruction;

        public event Action<IDialogResult> RequestClose;

        public string PrimaryButtonText => Resources.Properties.Resources.Save;


        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }

        public void OnPrimaryButtonPressed()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Yes));
        }

    }
}
