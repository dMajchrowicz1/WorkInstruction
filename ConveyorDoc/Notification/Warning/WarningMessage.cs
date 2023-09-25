using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications.Core;

namespace ConveyorDoc.Notification.Warning
{
    public class WarningMessage : MessageBase<WarningDisplayPart>
    {
        public WarningMessage(string message) : this(message, new MessageOptions())
        {
        }

        public WarningMessage(string message, MessageOptions options) : base(message, options)
        {
        }

        protected override WarningDisplayPart CreateDisplayPart()
        {
            return new WarningDisplayPart(this);
        }

        protected override void UpdateDisplayOptions(WarningDisplayPart displayPart, MessageOptions options)
        {
            if (options.FontSize != null)
                displayPart.Text.FontSize = options.FontSize.Value;

            displayPart.CloseButton.Visibility = options.ShowCloseButton ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
