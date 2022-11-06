using System.Windows.Controls;

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
        }

        private void SfTextBoxExt_TextChanged(object sender, TextChangedEventArgs e)
        {
            KomitentNaziv.IsDropDownOpen = true;
        }
    }
}
