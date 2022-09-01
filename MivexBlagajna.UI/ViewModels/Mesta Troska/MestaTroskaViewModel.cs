using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.MestaTroska
{
    public class MestaTroskaViewModel : ViewModelBase, IDockElement
    {
        private string _header;
        private DockState _state;
        private IMestoTroskaRepository _mestoTroskaRepository;
        private MestoTroska _selectedMestoTroska;

        public MestaTroskaViewModel(
            IMestoTroskaRepository mestoTroskaRepository,
            string header = "Mesta troska",
            DockState state = DockState.Document
            )
        {
            _header = header;
            _state = state;
            _mestoTroskaRepository = mestoTroskaRepository;
            MestaTroska = new ObservableCollection<MestoTroska>();
        }

        public string? Header
        {
            get { return _header; }
            set { }
        }
        public DockState State
        {
            get { return _state; }
            set { }
        }

        public ObservableCollection<MestoTroska> MestaTroska { get; set; }

        public override async Task LoadAsync()
        {
            var mesta = await _mestoTroskaRepository.GetAll();

            foreach (var mesto in mesta)
            {
                MestaTroska.Add(mesto);
            }
        }

        public MestoTroska SelectedMestoTroska
        {
            get { return _selectedMestoTroska; }
            set { _selectedMestoTroska = value; OnModelPropertyChanged(); }
        }

    }
}