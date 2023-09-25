
using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;
using System;
using System.CodeDom;
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

namespace ConveyorDoc.Views.InstructionViews.InstructionSubViews
{
    /// <summary>
    /// Interaction logic for DrawingsView.xaml
    /// </summary>
    public partial class DrawingsItemView : UserControl
    {
        private Flyout flyout;


        public DrawingsItemView()
        {
            InitializeComponent();

        }


        private void DrawingVariables_Opening(object sender, object e)
        {
            flyout = sender as Flyout;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            flyout.Hide();
        }
    }
}
