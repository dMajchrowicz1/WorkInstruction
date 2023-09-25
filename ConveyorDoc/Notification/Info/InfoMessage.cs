using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;
using ToastNotifications.Core;

namespace ConveyorDoc.Notification
{
    public class InfoMessage : MessageBase<InfoDisplayPart>
    {
        public InfoMessage(string message) : this(message, new MessageOptions())
        {
        }

        public InfoMessage(string message, MessageOptions options) : base(message, options)
        {
        }

        protected override InfoDisplayPart CreateDisplayPart()
        {
            return new InfoDisplayPart(this, Options);
        }

        protected override void UpdateDisplayOptions(InfoDisplayPart displayPart, MessageOptions options)
        {
            if (options.FontSize != null)
                displayPart.Text.FontSize = options.FontSize.Value;

            displayPart.CloseButton.Visibility = options.ShowCloseButton ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
