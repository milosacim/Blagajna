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
    public class KomitentiEnableTextBoxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var textboxStatus = (TextBoxStatus)value;
            var test =  textboxStatus == TextBoxStatus.Disabled ? false : true;
            return test;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
