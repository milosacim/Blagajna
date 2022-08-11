using MivexBlagajna.UI.ViewModels;
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

namespace MivexBlagajna.UI.Views.Navigations
{
    /// <summary>
    /// Interaction logic for KomitentiNavigationView.xaml
    /// </summary>
    public partial class KomitentiNavigationView : UserControl
    {
        public KomitentiNavigationView()
        {
            InitializeComponent();
            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListOfKomitenti.ItemsSource);
            //view.Filter = FilterList;
        }

        //private bool FilterList(object item)
        //{
        //    if (string.IsNullOrEmpty(Pretraga.Text))
        //    {
        //        return true;
        //    } else
        //    {
        //        return (item as KomitentiNavigationItemViewModel).PunNaziv.IndexOf(Pretraga.Text, StringComparison.OrdinalIgnoreCase) >= 0;
        //    }
        //}

        //private void TxtFilter_Changed(object sender, TextChangedEventArgs e)
        //{
        //    CollectionViewSource.GetDefaultView(ListOfKomitenti.ItemsSource).Refresh();
        //}
    }
}
