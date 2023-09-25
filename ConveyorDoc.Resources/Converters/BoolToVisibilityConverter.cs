using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ConveyorDoc.Resources.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isVisible)
            {
                return isVisible is true
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }

            throw new ArgumentException("BoolToVisibilityConverterValueMustBeBool");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("BoolToVisibilityConverterConvertBack");
        }
    }
}