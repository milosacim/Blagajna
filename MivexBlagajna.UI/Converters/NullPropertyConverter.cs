using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace MivexBlagajna.UI.Converters
{
    class NullPropertyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return new ComboBoxItem().Content = " - ";
            }
            else
            {
                return value;
            }
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var returnValue = value as ComboBoxItem;

            if ((string?)returnValue?.Content != " - ")
            {
                return value;
            }
            else
            {
                return null;
            }
        }
    }
}
