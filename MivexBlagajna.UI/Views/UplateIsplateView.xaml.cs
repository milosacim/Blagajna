using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;
using System.Windows.Data;

namespace MivexBlagajna.UI.Views
{
    /// <summary>
    /// Interaction logic for UplateIsplateView.xaml
    /// </summary>
    public partial class UplateIsplateView : UserControl
    {
        public UplateIsplateView()
        {
            InitializeComponent();

            this.DataContextChanged += UplateIsplateView_DataContextChanged;
        }

        // EventHandler kada se setuje DataContext na ucitavanju kontrole
        // Setuje podrazumevani DataBinding za komitente
        private void UplateIsplateView_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            Binding textBoxBinding = new Binding("Transakcija.Komitent.Sifra");
            textBoxBinding.Source = this.DataContext;
            textBoxBinding.Mode = BindingMode.TwoWay;
            textBoxBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            SearchBox.SetBinding(TextBox.TextProperty, textBoxBinding);

            Binding comboBoxBinding = new Binding("Komitenti");
            comboBoxBinding.Source = DataContext;
            KomitentNaziv.SetBinding(ItemsControl.ItemsSourceProperty, comboBoxBinding);
        }

        // EventHandler kada se kreira novi nalog
        // menja DataBinding textBox-a za sifru komitenta
        // tako da je moguca pretraga komitenata
        private void novNalogBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Binding textBoxBinding = new Binding("KomitentFilter");
            textBoxBinding.Source = this.DataContext;
            textBoxBinding.Mode = BindingMode.TwoWay;
            textBoxBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            SearchBox.SetBinding(TextBox.TextProperty, textBoxBinding);

            Binding comboBoxBinding = new Binding("FilteredKomitenti");
            comboBoxBinding.Source = DataContext;
            KomitentNaziv.SetBinding(ItemsControl.ItemsSourceProperty, comboBoxBinding);
        }

        // EventHandler kada se menja text u textBoxu za sifru komitenta
        // ako je isEnabled property comboBox-a true onda ce na promeni texta biti otvorena padajuca lista
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (KomitentNaziv.IsEnabled == true)
            {
                KomitentNaziv.IsDropDownOpen = true;
            }
            else
            {
                KomitentNaziv.IsDropDownOpen = false;
            }
        }
    }
}
