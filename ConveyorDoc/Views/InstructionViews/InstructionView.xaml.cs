
using System.Windows;
using System.Windows.Controls;

namespace ConveyorDoc.Views.Programs
{
    /// <summary>
    /// Interaction logic for InstructionView.xaml
    /// </summary>
    public partial class InstructionView : UserControl
    {
        public InstructionView()
        {
            InitializeComponent();

            InstructionNavigator.DataContextChanged += InstructionNavigator_DataContextChanged;
        }

        private void InstructionNavigator_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //When datacontext change Naviagtor will reset old data
            if (InstructionNavigator != null) 
            {
                InstructionNavigator.SelectedItem = null;
                InstructionContent.Content = null;
            }
        }
    }
}
