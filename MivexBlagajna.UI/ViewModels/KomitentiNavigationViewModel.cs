using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services;
using MivexBlagajna.UI.Events;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
            Komitenti = new ObservableCollection<KomitentiNavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterKomitentSavedEvent>().Subscribe(AfterKomitentSaved);
        }

        private void AfterKomitentSaved(AfterKomitentSavedEventArgs obj)
        {
            var lookupitem = Komitenti.Single(l => l.Id == obj.Id);
            lookupitem.PunNaziv = obj.PunNaziv;
        }

        public async override Task LoadAsync()
        {
            var lookup = await _lookupKomitentDataService.GetLookupKomitentAsync();
            Komitenti.Clear();
            foreach (var item in lookup)
            {
                Komitenti.Add(new KomitentiNavigationItemViewModel(item.Id, item.PunNaziv));
            }
        }
        public ObservableCollection<KomitentiNavigationItemViewModel> Komitenti { get; }

        private KomitentiNavigationItemViewModel _selectedKomitent;

        public KomitentiNavigationItemViewModel SelectedKomitent
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
