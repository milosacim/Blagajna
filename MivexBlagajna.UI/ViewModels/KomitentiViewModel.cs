using MivexBlagajna.DataAccess.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels
{
    public class KomitentiViewModel : ViewModelBase, IDockElement
    {
        private string _header;
        private DockState _state;
        private IKomitentDataService _komitentiDataService;
        private SingleKomitentViewModel _selectedKomitent;

        public KomitentiViewModel(
            IKomitentDataService komitentiDataService,
            string header = "Komitenti",
            DockState state = DockState.Document)
        {
            _header = header;
            _state = state;
            _komitentiDataService = komitentiDataService;
        }

        public string? Header
        {
            get { return _header; }
            set { _header = value; }
        }

        public DockState State
        {
            get { return _state; }
            set { _state = value; }
        }

        public ObservableCollection<SingleKomitentViewModel>? Komitenti { get; } = new();

        public override async Task LoadAsync()
        {
            var komitenti = await _komitentiDataService.GetAllAsync();
            if (komitenti != null)
            {
                foreach (var komitent in komitenti)
                {
                    komitenti.Add(komitent);
                }
            }
            SelectedKomitent = Komitenti.Last();
        }

        public SingleKomitentViewModel SelectedKomitent
        {
            get { return _selectedKomitent; }
            set { _selectedKomitent = value; OnPropertyChanged(); }
        }
    }
}
