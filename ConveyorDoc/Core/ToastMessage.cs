using ConveyorDoc.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _notifier.ShowError(message);
        }

        public void ShowInfo(string message)
        {
            _notifier.ShowInformation(message);
        }

        public void ShowSucces(string message)
        {
            _notifier.ShowSucces(message);
        }

        public void ShowWarning(string message)
        {
            _notifier.ShowWarning(message);
        }
    }
}
