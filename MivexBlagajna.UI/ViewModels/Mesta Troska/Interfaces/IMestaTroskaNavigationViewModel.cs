using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Mesta_Troska.Navigation
{
    public interface IMestaTroskaNavigationViewModel
    {
        Task LoadAsync();

        event EventHandler<MestoTroskaArgs> OpenDetails;
        ObservableCollection<MestaTroskaNavigationItemViewModel> MestaTroska { get; }
        MestaTroskaNavigationItemViewModel SelectedMestoTroska { get; set; }
        void Dispose();

    }
    public class MestoTroskaArgs
    {
        public readonly int id;

        public MestoTroskaArgs(int id)
        {
            this.id = id;
        }
    }
}