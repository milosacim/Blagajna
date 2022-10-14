using MivexBlagajna.UI.ViewModels.Komitenti.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Komitenti.Interfaces
{
    public interface IKomitentiNavigationViewModel
    {

        event EventHandler<SelectedKomitentArgs> OnkomitentSelected;
        KomitentiNavigationItemViewModel? SelectedKomitent { get; set; }
        ObservableCollection<KomitentiNavigationItemViewModel> Komitenti { get; }
        Task LoadAsync();
        void Dispose();
    }

    public class SelectedKomitentArgs
    {
        public readonly int? oldId;
        public readonly int newid;
        public readonly bool isSelected;

        public SelectedKomitentArgs(int newid, bool isSelected, int? oldId = null)
        {
            this.oldId = oldId;
            this.newid = newid;
            this.isSelected = isSelected;
        }
    }
}