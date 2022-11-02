using MivexBlagajna.UI.EventArgs;
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
}