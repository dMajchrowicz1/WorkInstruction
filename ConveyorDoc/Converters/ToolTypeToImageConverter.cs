using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ConveyorDoc.Converters
{
    public class ToolTypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            switch (value as string)
            {
                case "Pomiar":
                    return "/Assets/Tools/nc_probe.png";
                case "Toczenie":
                    return "/Assets/Tools/nc_tool_turn_20.png";
                case "Rowkowanie":
                    return "/Assets/Tools/nc_tool_turngroove_20.png";
                case "Wytaczanie":
                    return "/Assets/Tools/nc_tool_boringbar_20.png";
                case "Szlifowanie":
                    return "/Assets/Tools/nc_tool_wedmcontour_20.png";
                case "Wiercenie":
                    return "/Assets/Tools/nc_tool_drill_20.png";
                case "Nawiercanie":
                    return "/Assets/Tools/nc_tool_spotdrill_20.png";
                case "Gwintowanie":
                    return "/Assets/Tools/nc_tool_tap_20.png";
                case "Rozwiercanie":
                    return "/Assets/Tools/nc_tool_reamer_20.png";
                case "Poglebianie":
                    return "/Assets/Tools/nc_tool_plungemill_20.png";
                case "Frezowanie":
                    return "/Assets/Tools/nc_tool_milling_20.png";
                case "FrezowanieGwint":
                    return "/Assets/Tools/nc_tool_threadmill_20.png";
                case "FrezowanieWytaczanie":
                    return "/Assets/Tools/nc_tool_boring_20.png";
                case "Wierszowanie":
                    return "/Assets/Tools/nc_tool_lollipop_20.png";
                case "Fazowanie":
                    return "/Assets/Tools/nc_tool_chamfer_20.png";
                case "FrezowanieRowka":
                    return "/Assets/Tools/nc_sidecutter_20.png";
                default:
                    return "/Assets/Tools/nc_sidecutter_20.png";

            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
