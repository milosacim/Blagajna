using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Mesta_Troska.Navigation
{
    public interface IMestaTroskaNavigationViewModel
    {
        Task LoadAsync();
        event MestaTroskaNavigationViewModel.OnOpenMestoTroskaDetails OpenDetails;
        ObservableCollection<MestaTroskaNavigationItemViewModel> MestaTroska { get; }
        MestaTroskaNavigationItemViewModel SelectedMestoTroska { get; set; }
        void Dispose();

    }
}