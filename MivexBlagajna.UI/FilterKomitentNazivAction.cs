using Microsoft.Xaml.Behaviors;
using MivexBlagajna.Data.Models;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Windows.Data;

namespace MivexBlagajna.UI
{
    class FilterKomitentNazivAction : TargetedTriggerAction<ComboBoxAdv>
    {
        protected override void Invoke(object parameter)
        {
            CollectionView items = (CollectionView)CollectionViewSource.GetDefaultView(Target.ItemsSource);

            items.Filter = ((o) =>
            {
                if (string.IsNullOrEmpty(Target.Text))
                    return true;
                else
                {
                    if ((o as Komitent).ToString().Contains(Target.Text, StringComparison.OrdinalIgnoreCase) && (o as Komitent).ToString() != null)
                    {
                        if (Target.IsDropDownOpen == true)
                        {
                            
                        }
                        else
                        {
                            Target.IsDropDownOpen = true;
                        }
                        return true;
                    }
                    else
                        return false;
                }
            });

            items.Refresh();
        }
    }
}
