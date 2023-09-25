using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToastNotifications.Core;

namespace ConveyorDoc.Notification
{
    /// <summary>
    /// Interaction logic for InfoDisplayPart.xaml
    /// </summary>
    public partial class InfoDisplayPart : NotificationDisplayPart
    {
        public InfoDisplayPart(InfoMessage information, MessageOptions options)
        {
            InitializeComponent();
            Bind(information);
        }

        private void OnClose(object sender, RoutedEventArgs e)
        {
            Notification.Close();
        }
    }
}
