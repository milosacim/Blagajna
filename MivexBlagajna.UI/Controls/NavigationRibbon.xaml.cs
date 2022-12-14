using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MivexBlagajna.UI.Controls
{
    /// <summary>
    /// Interaction logic for NavigationRibbon.xaml
    /// </summary>
    public partial class NavigationRibbon : UserControl
    {
        public NavigationRibbon()
        {
            InitializeComponent();
            Loaded += NavigationRibbon_Loaded;
        }

        private void NavigationRibbon_Loaded(object sender, RoutedEventArgs e)
        {
            _ribbon.BackStageButton.Visibility = Visibility.Collapsed;
        }
    }
}
