using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services;
using MivexBlagajna.DataAccess.Services.Lookups;
using MivexBlagajna.UI.Events;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels
{
    public class KomitentiNavigationViewModel : ViewModelBase, IKomitentiNavigationViewModel
    {
        private ILookupKomitentDataService _lookupKomitentDataService;
        private readonly IEventAggregator _eventAggregator;
        private string _nazivFilter;
        private bool _pravnoLiceFilter;
        private bool _fizickoLiceFilter;
        private KomitentiNavigationItemViewModel _selectedKomitent;

        public KomitentiNavigationViewModel(ILookupKomitentDataService lookupKomitentDataService
            , IEventAggregator eventAggregator)
        {
            _lookupKomitentDataService = lookupKomitentDataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<AfterKomitentSavedEvent>().Subscribe(AfterKomitentSaved);
            _nazivFilter = "";
            _fizickoLiceFilter = false;
            _pravnoLiceFilter = false;
            Komitenti = new ObservableCollection<KomitentiNavigationItemViewModel>();
        }

        private void AfterKomitentSaved(AfterKomitentSavedEventArgs obj)
        {
            var lookupitem = Komitenti.Single(l => l.Id == obj.Id);
            lookupitem.PunNaziv = obj.PunNaziv;
        }

        public async Task LoadAsync(string filter, bool fizickoLice, bool pravnoLice)
        {
            var lookup = await _lookupKomitentDataService.GetLookupKomitentAsync();
            IList<LookupKomitent> komitentiList = new List<LookupKomitent>();
            Komitenti.Clear();

            foreach (var item in lookup)
            {
                if (item.PunNaziv.Contains(filter, StringComparison.OrdinalIgnoreCase) || !item.GetType().GetProperty(nameof(item.PravnoLice)).GetValue(item).Equals(pravnoLice))
                {
                    Komitenti.Add(new KomitentiNavigationItemViewModel(item.Id, item.PunNaziv, item.PravnoLice, item.FizickoLice));
                }
            }
        }

        public ObservableCollection<KomitentiNavigationItemViewModel> Komitenti { get; }
        public KomitentiNavigationItemViewModel SelectedKomitent
        {
            get { return _selectedKomitent; }
            set
            {
                _selectedKomitent = value;
                OnModelPropertyChanged();

                if (_selectedKomitent != null)
                {
                    _eventAggregator.GetEvent<OpenKomitentDetailViewEvent>().Publish(_selectedKomitent.Id);
                }
            }
        }

        public string NazivFilter
        {
            get { return _nazivFilter; }
            set
            {
                _nazivFilter = value;
                OnModelPropertyChanged();
                OnFilterChanged();
            }
        }

        public bool PravnoLiceFilter
        {
            get { return _pravnoLiceFilter; }
            set
            {
                _pravnoLiceFilter = value;
                OnModelPropertyChanged();
                OnFilterChanged();
            }
        }

        public bool FizickoLiceFilter
        {
            get { return _fizickoLiceFilter; }
            set
            {
                _fizickoLiceFilter = value;
                OnModelPropertyChanged();
                OnFilterChanged();
            }
        }

        public void OnFilterChanged()
        {
            _eventAggregator.GetEvent<OnKomitentiNavigationFilterChangedEvent>().Publish(
                new FilterChangedArgs
                {
                    FizickoLiceFilter = _fizickoLiceFilter,
                    PravnoLiceFilter = _pravnoLiceFilter,
                    NazivFilter = _nazivFilter
                });
        }
    }
}
