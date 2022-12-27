using Microsoft.Xaml.Behaviors;
using MivexBlagajna.UI.Wrappers;
using Syncfusion.Windows.Tools.Controls;
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
                    if ((o as KomitentWrapper).Naziv.Contains(Target.Text))
                        return true;
                    else
                        return false;
                }
            });
            items.Refresh();
        }
    }
}
