using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Wrappers;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MivexBlagajna.UI.ViewModels.MestaTroska
{
    public class MestaTroskaViewModel : ViewModelBase, IDockElement
    {
        private string _header;
        private DockState _state;
        private IMestoTroskaRepository _mestoTroskaRepository;
        private MestoTroska _mestoTroska;
        private bool _hasChanges;

        public MestaTroskaViewModel(
            IMestoTroskaRepository mestoTroskaRepository,
            string header = "Mesta troska",
            DockState state = DockState.Document )
        {
            _header = header;
            _state = state;
            _mestoTroskaRepository = mestoTroskaRepository;
            MestaTroska = new ObservableCollection<MestoTroska>();
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private async void OnSaveExecute()
        {
            await _mestoTroskaRepository.SaveAsync();
            HasChanges = _mestoTroskaRepository.HasChanges();
        }

        private bool OnSaveCanExecute()
        {
            return MestoTroska != null;
        }

        public MestoTroska MestoTroska
        {
            get { return _mestoTroska; }
            set { _mestoTroska = value; OnModelPropertyChanged(); }
        }

        
        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnModelPropertyChanged();
                }
            }
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
        public ObservableCollection<MestoTroska> MestaTroska { get; }
        public override async Task LoadAsync()
        {
            var mesta = await _mestoTroskaRepository.GetAll();
            MestaTroska.Clear();
            foreach (var mesto in mesta)
            {
                MestaTroska.Add(mesto);
            }

        }

        public ICommand SaveCommand { get; }
    }
}