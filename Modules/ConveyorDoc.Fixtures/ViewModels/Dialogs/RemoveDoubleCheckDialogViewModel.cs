using ConveyorDoc.Core;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Fixtures.ViewModels.Dialogs
{
    public class RemoveDoubleCheckDialogViewModel : BindableBase, IDialogAware2
    {
        public string Title => ConveyorDoc.Resources.Properties.Resources.RemoveItem;

        public string PrimaryButtonText => ConveyorDoc.Resources.Properties.Resources.Remove;

        public event Action<IDialogResult> RequestClose;

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }

        public void OnPrimaryButtonPressed()
        {
           RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }
    }
}
