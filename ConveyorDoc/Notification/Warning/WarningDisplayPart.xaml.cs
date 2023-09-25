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

namespace ConveyorDoc.Notification.Warning
{
    /// <summary>
    /// Interaction logic for WarningDisplayPart.xaml
    /// </summary>
    public partial class WarningDisplayPart : NotificationDisplayPart
    {
        public WarningDisplayPart(WarningMessage warning)
        {
            InitializeComponent();
            Bind(warning);
        }

        private void OnClose(object sender, RoutedEventArgs e)
        {
            Notification.Close();
        }
    }
}
