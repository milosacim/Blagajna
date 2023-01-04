using MivexBlagajna.Data.Models;
using Syncfusion.Data.Extensions;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace MivexBlagajna.UI.Views
{
    /// <summary>
    /// Interaction logic for FinansijskaKarticaParameterView.xaml
    /// </summary>
    public partial class FinansijskaKarticaView : UserControl
    {
        public FinansijskaKarticaView()
        {
            InitializeComponent();

        }

        private void SfTextBoxExt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CompositeCollection collection = (CompositeCollection)CollectionViewSource.GetDefaultView(Komitenti.ItemsSource).SourceCollection;

                CollectionContainer container = new();

                foreach (var item in collection)
                {
                    if (item is CollectionContainer)
                    {

                        container = (CollectionContainer)item;

                        var items = (ListCollectionView)container.Collection;

                        items.Refresh();

                        items.Filter = (o) =>
                        {
                            return FilterItems(sender, o);
                        };

                        items.Refresh();
                    }
                }

                if (!Komitenti.IsDropDownOpen)
                {
                    Komitenti.IsDropDownOpen = true;
                }
            }
        }

        private static bool FilterItems(object sender, object o)
        {
            if (o.ToString().Contains((sender as TextBox).Text, StringComparison.OrdinalIgnoreCase) && o.ToString() != null)
            {
                return true;
            }
            else
                return false;
        }

        private void Komitenti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0 && e.AddedItems[0] is not ComboBoxItem)
            {
                SearchBox.Text = ((Komitent)e.AddedItems[0]).Sifra.ToString();
                Komitenti.Items.Refresh();
                e.AddedItems.Clear();
            }
            else if (e.AddedItems[0] is ComboBoxItem)
            {
                SearchBox.Text = string.Empty;
            }
            Komitenti.IsDropDownOpen = false;
        }
    }
}
