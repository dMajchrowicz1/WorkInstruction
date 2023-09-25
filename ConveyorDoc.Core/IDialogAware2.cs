using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConveyorDoc.Core
{
    public interface IDialogAware2 
    {
        //
        // Summary:
        //     The title of the dialog that will show in the window title bar.
        string Title { get; }
        /// <summary>
        ///  Primary button text
        /// </summary>
        string PrimaryButtonText { get; }

        //
        // Summary:
        //     Instructs the Prism.Services.Dialogs.IDialogWindow to close the dialog.
        event Action<IDialogResult> RequestClose;


        /// <summary>
        /// Method will be invoked when user clic primary button
        /// </summary>
        void OnPrimaryButtonPressed();


        void OnDialogOpened(IDialogParameters parameters);

    }
}
