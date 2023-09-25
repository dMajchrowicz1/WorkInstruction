using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConveyorDoc.Core
{
    public interface IWindowsDialogService
    {
       void ShowFolderBrowseDialog(Action<IDialogResult> callback, string initialDirecotry);

       void ShowSaveFileDialog(Action<IDialogResult> callback, string initialDirecotry, string filename = "");

       void ShowOpenFileDialog(string title, string filter, Action<IDialogResult> callback, string initialDirectory, bool multiSelection = false);

        Task ShowContentDialog(Type view, Action<IDialogResult> callback, IDialogParameters parameters = null);

    }
}
