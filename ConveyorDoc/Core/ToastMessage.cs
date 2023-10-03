using ConveyorDoc.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications;

namespace ConveyorDoc.Core
{
    public class ToastMessage : IToastMessage
    {
        private  readonly Notifier _notifier;

        public ToastMessage(Notifier notifier)
        {
            _notifier = notifier;
        }

        public void ShowError(string message)
        {
            Application.Current.Dispatcher.InvokeAsync(() => _notifier.ShowError(message));
        }

        public void ShowInfo(string message)
        {
            Application.Current.Dispatcher.InvokeAsync(() => _notifier.ShowInformation(message));
        }

        public void ShowSucces(string message)
        {
            Application.Current.Dispatcher.InvokeAsync(() => _notifier.ShowSucces(message));
        }

        public void ShowWarning(string message)
        {
            Application.Current.Dispatcher.InvokeAsync(() => _notifier.ShowWarning(message));
        }
    }
}
