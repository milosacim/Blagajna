using Microsoft.Win32;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.UI.Xaml.Grid.Helpers;
using Syncfusion.XlsIO;
using System.IO;
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
            if (KomitentNaziv.IsEnabled == true)
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
            //var defaultComboBoxBinding = "Komitenti";

            Binding textBoxBinding = new Binding(defaultTextBoxBinding);
            textBoxBinding.Source = DataContext;
            textBoxBinding.Mode = BindingMode.TwoWay;
            textBoxBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            SearchBox.SetBinding(TextBox.TextProperty, textBoxBinding);

            //Binding comboBoxBinding = new Binding(defaultComboBoxBinding);
            //comboBoxBinding.Source = DataContext;
            //KomitentNaziv.SetBinding(ItemsControl.ItemsSourceProperty, comboBoxBinding);
        }

        private void SetCustomDataBinding()
        {
            var alternateTextBoxBinding = "KomitentFilter";
            //var alternateComboBoxBinding = "FilteredKomitenti";

            Binding textBoxBinding = new Binding(alternateTextBoxBinding);
            textBoxBinding.Source = DataContext;
            textBoxBinding.Mode = BindingMode.OneWayToSource;
            textBoxBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            SearchBox.SetBinding(TextBox.TextProperty, textBoxBinding);

            //Binding comboBoxBinding = new Binding(alternateComboBoxBinding);
            //comboBoxBinding.Source = DataContext;
            //KomitentNaziv.SetBinding(ItemsControl.ItemsSourceProperty, comboBoxBinding);
        }

        private void KomitentNaziv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KomitentNaziv.IsDropDownOpen = false;
        }

        private void ExportExcel_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel();
        }

        private void ExportToExcel()
        {
            var options = new ExcelExportingOptions();
            options.ExcelVersion = ExcelVersion.Excel2016;

            var excelEngine = tabela.ExportToExcel(tabela.View, options);
            var workbook = excelEngine.Excel.Workbooks[0];

            FileStream fileStream = new FileStream("Tabela.xlsx", FileMode.Create);

            SaveFileDialog sfd = new SaveFileDialog()
            {
                FilterIndex = 3,
                Filter = "Excel 97 to 2003 Files(*.xls)|*.xls|Excel 2007 to 2010 Files(*.xlsx)|*.xlsx|Excel 2013 File(*.xlsx)|*.xlsx"
            };

            if (sfd.ShowDialog() == true)
            {
                using (Stream stream = sfd.OpenFile())
                {
                    if (sfd.FilterIndex == 1)
                    {
                        workbook.Version = ExcelVersion.Excel97to2003;
                    }

                    else if (sfd.FilterIndex == 2)
                    {
                        workbook.Version = ExcelVersion.Excel2010;
                    }
                    else
                    {
                        workbook.Version = ExcelVersion.Excel2013;
                    }
                    workbook.SaveAs(fileStream);
                }
            }

            if (MessageBox.Show("Do you want to view the workbook?", "Workbook has been created",
                        MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                //Launching the Excel file using the default Application.[MS Excel Or Free ExcelViewer]
                System.Diagnostics.Process.Start(sfd.FileName);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultDataBinding();
        }
    }
}
