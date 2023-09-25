using ConveyorDoc.Notification.Error;
using ConveyorDoc.Notification.Succes;
using ConveyorDoc.Notification.Warning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToastNotifications;
using ToastNotifications.Core;

namespace ConveyorDoc.Notification
{
    public static class NotificationExtension
    {
        public static void ShowError(this Notifier notifier, string message)
        {
            notifier.Notify<ErrorMessage>(() => new ErrorMessage(message));
        }

        public static void ShowInformation(this Notifier notifier, string message)
        {
            notifier.Notify<InfoMessage>(() => new InfoMessage(message));
        }

        public static void ShowWarning(this Notifier notifier, string message)
        {
            notifier.Notify<WarningMessage>(() => new WarningMessage(message));
        }

        public static void ShowSucces(this Notifier notifier, string message)
        {
            notifier.Notify<SuccesMessage>(() => new SuccesMessage(message));
        }
    }
}
