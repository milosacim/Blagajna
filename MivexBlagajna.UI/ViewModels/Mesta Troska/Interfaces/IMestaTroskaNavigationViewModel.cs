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
        public int _newid;
        public int _newNadId;
        public bool _isSelected;
        public int? _oldId;

        public MestoTroskaArgs(int newid, int newNadId, bool isSelected, int? oldId = null)
        {
            _newid = newid;
            _newNadId = newNadId;
            _isSelected = isSelected;
            _oldId = oldId;
        }
    }
}