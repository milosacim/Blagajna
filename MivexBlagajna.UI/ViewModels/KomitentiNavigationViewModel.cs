using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services;
using MivexBlagajna.UI.Events;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels
{
    public class KomitentiNavigationViewModel : ViewModelBase, IKomitentiNavigationViewModel
    {
        private ILookupKomitentDataService _lookupKomitentDataService;
        private readonly IEventAggregator _eventAggregator;

        public KomitentiNavigationViewModel(ILookupKomitentDataService lookupKomitentDataService
            , IEventAggregator eventAggregator)
        {
            _lookupKomitentDataService = lookupKomitentDataService;
            _eventAggregator = eventAggregator;
            Komitenti = new ObservableCollection<LookupKomitent>();
        }
        public async Task LoadAsync()
        {
            var lookup = await _lookupKomitentDataService.GetLookupKomitentAsync();
            Komitenti.Clear();
            foreach (var item in lookup)
            {
                Komitenti.Add(item);
            }
        }
        public ObservableCollection<LookupKomitent> Komitenti { get; }

        private LookupKomitent _selectedKomitent;

        public LookupKomitent SelectedKomitent
        {
            get { return _selectedKomitent; }
            set
            {
                _selectedKomitent = value; 
                OnPropertyChanged();
                if (_selectedKomitent != null)
                {
                    _eventAggregator.GetEvent<OpenKomitentDetailViewEvent>().Publish(_selectedKomitent.Id);
                }
            }
        }


    }
}
