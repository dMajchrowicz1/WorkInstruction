using ConveyorDoc.Business.Model;
using ConveyorDoc.Model;
using ConveyorDoc.Resources.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ConveyorDoc.Converters
{
    public class WordInstructionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Word word)
            {
                return new List<InstructinSubItem>
                {
                    new InstructinSubItem(Resources.Properties.Resources.Tools,"/Assets/TreeView/icons8-tools-48.png","ToolsItemView",word)

                    ,new InstructinSubItem(Resources.Properties.Resources.Fixtures,"/Assets/TreeView/icons8-services-48.png","FixturesItemView",word)

                    ,new InstructinSubItem(Resources.Properties.Resources.Variables,"/Assets/TreeView/icons8-variable-48.png","VariablesItemView",word)

                    ,new InstructinSubItem(Resources.Properties.Resources.Description,"/Assets/TreeView/icons8-new-document-48.png","DescriptionItemView",word)

                    ,new InstructinSubItem(Resources.Properties.Resources.Drawings,"/Assets/TreeView/icons8-pdf-48.png","DrawingsItemView",word)

                };
            }
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
