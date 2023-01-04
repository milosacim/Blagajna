using Microsoft.Win32;
using MivexBlagajna.Data.Models;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.UI.Xaml.Grid.Helpers;
using Syncfusion.Windows.Controls.Input;
using Syncfusion.XlsIO;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

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

        private void KomitentNaziv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                SearchBox.Text = ((Komitent)e.AddedItems[0]).Sifra.ToString();
                mestaComboBox.SelectedItem = ((Komitent)e.AddedItems[0]).MestoTroska;

                e.AddedItems.Clear();
            }
            KomitentNaziv.IsDropDownOpen = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var collection = CollectionViewSource.GetDefaultView(KomitentNaziv.ItemsSource);
            collection.Filter = (o) =>
            {
                return true;
            };

            tabela.RefreshColumns();
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
                    workbook.SaveAs(stream);
                }
            }

            if (MessageBox.Show("Do you want to view the workbook?", "Workbook has been created",
                        MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                //Launching the Excel file using the default Application.[MS Excel Or Free ExcelViewer]
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
            }
        }

        private void UplateBoxExt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (uplataTextBox.Text == "")
            {
                uplataTextBox.Text = "0";
            }
        }

        private void IsplateTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isplateTxtBox.Text == "")
            {
                isplateTxtBox.Text = "0";
            }
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && SearchBox.IsEnabled == true)
            {
                var collection = CollectionViewSource.GetDefaultView(KomitentNaziv.ItemsSource);

                collection.Filter = (o) =>
                {
                    return FilterItems(sender, o);
                };

                collection.Refresh();

                if (!KomitentNaziv.IsDropDownOpen)
                {
                    KomitentNaziv.IsDropDownOpen = true;
                }
            }
        }

        private bool FilterItems(object sender, object o)
        {
            if (o.ToString().Contains((sender as TextBox).Text, StringComparison.OrdinalIgnoreCase) && o.ToString() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            var collection = CollectionViewSource.GetDefaultView(KomitentNaziv.ItemsSource);
            collection.Filter = (o) =>
            {
                return true;
            };
        }
    }
}
