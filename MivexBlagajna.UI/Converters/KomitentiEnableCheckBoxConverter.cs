using MivexBlagajna.UI.ViewModels.Komitenti;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MivexBlagajna.UI.Converters
{
    public class KomitentiEnableCheckBoxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var textboxStatus = (CheckBoxStatus)value;
            return textboxStatus == CheckBoxStatus.Disabled ? false : true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
