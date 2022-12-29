using System;
using System.Globalization;
using System.Windows.Data;

namespace MivexBlagajna.UI.Converters
{
    class NullItemConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null) { return value; }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
