using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MivexBlagajna.UI.EventArgs;

namespace MivexBlagajna.UI.ViewModels.Mesta_Troska.Navigation
{
    public interface IMestaTroskaNavigationViewModel : IDisposable
    {
        Task LoadAsync();

        event EventHandler<MestoTroskaArgs> OnMestoSelected;
        ObservableCollection<MestaTroskaNavigationItemViewModel> MestaTroska { get; }
        MestaTroskaNavigationItemViewModel SelectedMestoTroska { get; set; }
    }
}