using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using ModernWpf;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ConveyorDoc.Resources.Converters
{
    public class EnumToBoolConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == parameter) 
                return true;
            return false;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}