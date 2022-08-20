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
using Syncfusion.Windows.Tools.Controls;

namespace MivexBlagajna.UI.Views.Details
{
    /// <summary>
    /// Interaction logic for KomitentDetailView.xaml
    /// </summary>
    public partial class KomitentDetailView : UserControl
    {
        public KomitentDetailView()
        {
            InitializeComponent();
        }

        private void EnableTextBox_Click(object sender, RoutedEventArgs e)
        {
            foreach (var el in from Panel p in DetailsGrid.Children
                               from UIElement el in p.Children
                               select el)
            {
                switch (el)
                {
                    case TextBox when ((TextBox)el).Name != "sifraTextBox":
                        el.IsEnabled = true;
                        break;
                    case Panel:
                        {
                            foreach (UIElement el2 in ((Panel)el).Children)
                            {
                                if (el2 is CheckBox)
                                {
                                    el2.IsEnabled = true;
                                }
                            }

                            break;
                        }
                }
            }

        }

        private void DisableTextBox_Click(object sender, RoutedEventArgs e)
        {
            foreach (var el in from Panel p in DetailsGrid.Children
                               from UIElement el in p.Children
                               select el)
            {
                switch (el)
                {
                    case TextBox when ((TextBox)el).IsEnabled == true:
                        el.IsEnabled = false;
                        break;
                    case Panel:
                        {
                            foreach (UIElement el2 in ((Panel)el).Children)
                            {
                                if (el2 is CheckBox)
                                {
                                    el2.IsEnabled = false;
                                }
                            }

                            break;
                        }
                }
            }
        }

        private void AfterSaveDisable_Click(object sender, RoutedEventArgs e)
        {
            foreach (var el in from Panel p in DetailsGrid.Children
                               from UIElement el in p.Children
                               select el)
            {
                switch (el)
                {
                    case TextBox when ((TextBox)el).IsEnabled == true:
                        el.IsEnabled = false;
                        break;
                    case Panel:
                        {
                            foreach (UIElement el2 in ((Panel)el).Children)
                            {
                                if (el2 is CheckBox)
                                {
                                    el2.IsEnabled = false;
                                }
                            }

                            break;
                        }
                }
            }
        }
    }
}
