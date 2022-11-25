using Syncfusion.UI.Xaml.Grid.Helpers;
using System.Windows;
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
        private void UplateIsplateView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SetDefaultDataBinding();
        }

        // EventHandler kada se kreira novi nalog
        // menja DataBinding textBox-a za sifru komitenta
        // tako da je moguca pretraga komitenata
        private void novNalogOrEditBtn_Click(object sender, RoutedEventArgs e)
        {
            SetCustomDataBinding();
        }

        // EventHandler kada se menja text u textBoxu za sifru komitenta
        // ako je isEnabled property comboBox-a true onda ce na promeni texta biti otvorena padajuca lista
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (KomitentNaziv.IsEnabled == true && KomitentNaziv.SelectedItem == null)
            {
                KomitentNaziv.IsDropDownOpen = true;
            }
            else
            {
                KomitentNaziv.IsDropDownOpen = false;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultDataBinding();
            tabela.RefreshColumns();
        }

        private void SetDefaultDataBinding()
        {
            var defaultTextBoxBinding = "Transakcija.Komitent.Sifra";
            var defaultComboBoxBinding = "Komitenti";

            Binding textBoxBinding = new Binding(defaultTextBoxBinding);
            textBoxBinding.Source = DataContext;
            textBoxBinding.Mode = BindingMode.TwoWay;
            textBoxBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            SearchBox.SetBinding(TextBox.TextProperty, textBoxBinding);

            Binding comboBoxBinding = new Binding(defaultComboBoxBinding);
            comboBoxBinding.Source = DataContext;
            KomitentNaziv.SetBinding(ItemsControl.ItemsSourceProperty, comboBoxBinding);
        }

        private void SetCustomDataBinding()
        {
            var alternateTextBoxBinding = "KomitentFilter";
            var alternateComboBoxBinding = "FilteredKomitenti";

            Binding textBoxBinding = new Binding(alternateTextBoxBinding);
            textBoxBinding.Source = DataContext;
            textBoxBinding.Mode = BindingMode.TwoWay;
            textBoxBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            SearchBox.SetBinding(TextBox.TextProperty, textBoxBinding);

            Binding comboBoxBinding = new Binding(alternateComboBoxBinding);
            comboBoxBinding.Source = DataContext;
            KomitentNaziv.SetBinding(ItemsControl.ItemsSourceProperty, comboBoxBinding);
        }

        private void KomitentNaziv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KomitentNaziv.IsDropDownOpen = false;
        }
    }
}
