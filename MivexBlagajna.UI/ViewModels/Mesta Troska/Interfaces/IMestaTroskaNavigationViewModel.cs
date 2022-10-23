using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Mesta_Troska.Navigation
{
    public interface IMestaTroskaNavigationViewModel
    {
        Task LoadAsync();

        event EventHandler<MestoTroskaArgs> OnMestoSelected;
        ObservableCollection<MestaTroskaNavigationItemViewModel> MestaTroska { get; }
        MestaTroskaNavigationItemViewModel SelectedMestoTroska { get; set; }
        void Dispose();

    }
    public class MestoTroskaArgs
    {
        public int newid;
        public int? newNadId;
        public bool isSelected;
        public int? oldId;

        public MestoTroskaArgs(int newid, int? newNadId, bool isSelected, int? oldId = null)
        {
            this.newid = newid;
            this.newNadId = newNadId;
            this.isSelected = isSelected;
            this.oldId = oldId;
        }
    }
}