using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services;
using MivexBlagajna.DataAccess.Services.Lookups;
using MivexBlagajna.UI.Events;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MivexBlagajna.UI.ViewModels
{
    public class KomitentiNavigationViewModel : ViewModelBase, IKomitentiNavigationViewModel
    {
        #region Fields
        private ILookupKomitentDataService _lookupKomitentDataService;
        private readonly IEventAggregator _eventAggregator;
        private string _nazivFilter;
        private bool _pravnoLiceFilter;
        private bool _fizickoLiceFilter;
        private KomitentiNavigationItemViewModel _selectedKomitent;
        #endregion

        #region Constructor
        public KomitentiNavigationViewModel(ILookupKomitentDataService lookupKomitentDataService
            , IEventAggregator eventAggregator)
        {
            _lookupKomitentDataService = lookupKomitentDataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<AfterKomitentSavedEvent>().Subscribe(AfterKomitentSaved);
            Komitenti = new ObservableCollection<KomitentiNavigationItemViewModel>();
            FilteredList = CollectionViewSource.GetDefaultView(Komitenti);

            FilteredList.Filter = new Predicate<object>(o => FilterNaziv(o as KomitentiNavigationItemViewModel) && FilterPravnoLice(o as KomitentiNavigationItemViewModel) && FilterFizickoLice(o as KomitentiNavigationItemViewModel));

            //FilteredList.Filter = new Predicate<object>(o => FilterPravnoLice(o as KomitentiNavigationItemViewModel));

        }
        #endregion

        #region Properties
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
        public ICollectionView FilteredList { get; private set; }
        public string NazivFilter
        {
            get { return _nazivFilter; }
            set { _nazivFilter = value; OnModelPropertyChanged(); FilteredList.Refresh(); }
        }
        public bool PravnoLiceFilter
        {
            get { return _pravnoLiceFilter; }
            set { _pravnoLiceFilter = value; OnModelPropertyChanged(); FilteredList.Refresh(); }
        }
        public bool FizickoLiceFilter
        {
            get { return _fizickoLiceFilter; }
            set { _fizickoLiceFilter = value; OnModelPropertyChanged(); FilteredList.Refresh(); }
        }
        #endregion

        #region Methods
        public async Task LoadAsync()
        {
            var lookup = await _lookupKomitentDataService.GetLookupKomitentAsync();
            Komitenti.Clear();

            foreach (var item in lookup)
            {
                Komitenti.Add(new KomitentiNavigationItemViewModel(item.Id, item.PunNaziv, item.PravnoLice, item.FizickoLice));
            }
        }
        private void AfterKomitentSaved(AfterKomitentSavedEventArgs obj)
        {
            var lookupitem = Komitenti.Single(l => l.Id == obj.Id);
            lookupitem.PunNaziv = obj.PunNaziv;
        }
        private bool FilterNaziv(KomitentiNavigationItemViewModel item)
        {
            return NazivFilter == null
                || item.PunNaziv.IndexOf(NazivFilter, StringComparison.OrdinalIgnoreCase) != -1;
        }
        private bool FilterPravnoLice(KomitentiNavigationItemViewModel item)
        {
            if (PravnoLiceFilter == FizickoLiceFilter)
            {
                return true;
            }
            else
            {
                return PravnoLiceFilter == false || item.PravnoLice == false;
            }
        }
        private bool FilterFizickoLice(KomitentiNavigationItemViewModel item)
        {
            if (PravnoLiceFilter == FizickoLiceFilter)
            {
                return true;
            }
            else
            {
                return FizickoLiceFilter == false || item.FizickoLice == false;
            }
        }
        #endregion
    }
}
