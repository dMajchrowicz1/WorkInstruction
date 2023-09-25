using ConveyorDoc.Model.Settings;
using ModernWpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ConveyorDoc.Converters
{
    public class AppThemeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case ApplicationTheme.Light:
                    return AppTheme.Light;
                case ApplicationTheme.Dark:
                    return AppTheme.Dark;
                  default:
                    return AppTheme.Light;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is AppTheme appTheme)
            {
                return appTheme.Value;
            }

            return AppTheme.Light;
        }
    }
}
