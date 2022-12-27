using Microsoft.Xaml.Behaviors;
using MivexBlagajna.Data.Models;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Windows.Data;

namespace MivexBlagajna.UI
{
    public class FilterKontoNazivAction : TargetedTriggerAction<ComboBoxAdv>
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
                    if ((o as Konto).Naziv.Contains(Target.Text, StringComparison.OrdinalIgnoreCase) && (o as Konto).Naziv != null)
                    {
                        Target.IsDropDownOpen = true;
                        return true;
                    }
                    else
                    {
                        Target.IsDropDownOpen = true;
                        return false;
                    }

                    if (Target.Text == "")
                    {
                        Target.IsDropDownOpen = false;
                    }
                }
            });
        }
    }
}
