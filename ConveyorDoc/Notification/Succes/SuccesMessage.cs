using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications.Core;

namespace ConveyorDoc.Notification.Succes
{
    public class SuccesMessage : MessageBase<SuccesDisplayPart>
    {
        public SuccesMessage(string message) : this(message, new MessageOptions())
        {
        }

        public SuccesMessage(string message, MessageOptions options) : base(message, options)
        {
        }

        protected override SuccesDisplayPart CreateDisplayPart()
        {
            return new SuccesDisplayPart(this);
        }

        protected override void UpdateDisplayOptions(SuccesDisplayPart displayPart, MessageOptions options)
        {
            if (options.FontSize != null)
                displayPart.Text.FontSize = options.FontSize.Value;

            displayPart.CloseButton.Visibility = options.ShowCloseButton ? Visibility.Visible : Visibility.Collapsed;
        }


    }
}

