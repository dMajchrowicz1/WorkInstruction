
using ConveyorDoc.Business.Model;
using ConveyorDoc.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace ConveyorDoc.Views.InstructionViews
{

    public class WordInstructionTemplateSelector : DataTemplateSelector
    {
        public DataTemplate WordInstructionTemplate { get; set; }

        public DataTemplate WordInstructionSubItemsTemplate { get; set; }


        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {


            if (item is Word word)
            {

                return WordInstructionTemplate;

            }
            else
            {
               return WordInstructionSubItemsTemplate;
            }

        }
    }
}
