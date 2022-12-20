using Castle.Core.Internal;
using MivexBlagajna.DataAccess.Services.Lookups;
using MivexBlagajna.UI.EventArgs;
using MivexBlagajna.UI.ViewModels.Komitenti.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MivexBlagajna.UI.ViewModels.Komitenti.Navigation
{
    public class KomitentiNavigationViewModel : ViewModelBase, IKomitentiNavigationViewModel
    {
        #region Fields
        private readonly ILookupKomitentDataService _lookupKomitentDataService;
        private string? _nazivFilter;
        private bool _pravnoLiceFilter;
        private bool _fizickoLiceFilter;
        private KomitentiNavigationItemViewModel? _selectedKomitent;

        public event EventHandler<SelectedKomitentArgs>? OnkomitentSelected;
        private readonly Predicate<object>? filterDelegate;

        #endregion

        #region Constructor
        public KomitentiNavigationViewModel(ILookupKomitentDataService lookupKomitentDataService)
        {
            _lookupKomitentDataService = lookupKomitentDataService;
            Komitenti = new ObservableCollection<KomitentiNavigationItemViewModel>();
            FilteredList = CollectionViewSource.GetDefaultView(Komitenti);
            filterDelegate = new Predicate<object>(o => FilterNaziv(o as KomitentiNavigationItemViewModel) && FilterPravnoLice(o as KomitentiNavigationItemViewModel) && FilterFizickoLice(o as KomitentiNavigationItemViewModel));
            FilteredList.Filter += filterDelegate;
        }

        #endregion

        #region Properties
        public ObservableCollection<KomitentiNavigationItemViewModel> Komitenti { get; }
        public KomitentiNavigationItemViewModel? SelectedKomitent
        {
            get { return _selectedKomitent; }
            set
            {
                var oldValue = _selectedKomitent;
                _selectedKomitent = value;
                OnModelPropertyChanged(oldValue, value);

                if (value != null)
                {
                    OnkomitentSelected?.Invoke(this, new SelectedKomitentArgs(_selectedKomitent.Id, _selectedKomitent.IsSelected, oldValue == null ? null : oldValue.Id));
                }
            }
        }

        public ICollectionView FilteredList { get; private set; }

        public string? NazivFilter
        {
            get { return _nazivFilter; }
            set
            {
                var oldValue = _nazivFilter;
                _nazivFilter = value;
                OnModelPropertyChanged(oldValue, value);
                FilteredList.Refresh();
            }
        }
        public bool PravnoLiceFilter
        {
            get { return _pravnoLiceFilter; }
            set
            {
                var oldValue = _pravnoLiceFilter;
                _pravnoLiceFilter = value;
                OnModelPropertyChanged(oldValue, value);
                FilteredList.Refresh();
            }
        }
        public bool FizickoLiceFilter
        {
            get { return _fizickoLiceFilter; }
            set
            {
                var oldValue = _fizickoLiceFilter;
                _fizickoLiceFilter = value;
                OnModelPropertyChanged(oldValue, value);
                FilteredList.Refresh();
            }
        }

        #endregion

        #region Methods
        public async override Task LoadAsync()
        {
            if (Komitenti.IsNullOrEmpty())
            {
                var lookup = await _lookupKomitentDataService.GetLookupKomitentAsync();
                foreach (var item in lookup)
                {
                    if (item != null)
                    {
                        Komitenti.Add(
                            new KomitentiNavigationItemViewModel(item.Id, item.PunNaziv, item.PravnoLice, item.FizickoLice));

                    }
                }
            }
            SelectedKomitent = Komitenti.FirstOrDefault();
        }

        private bool FilterNaziv(KomitentiNavigationItemViewModel? item)
        {
            return item != null && (NazivFilter == null || item.PunNaziv.IndexOf(NazivFilter, StringComparison.OrdinalIgnoreCase) != -1);
        }
        private bool FilterPravnoLice(KomitentiNavigationItemViewModel? item)
        {
            if (item != null)
            {
                if (PravnoLiceFilter == FizickoLiceFilter)
                {
                    return true;
                }
                else
                {
                    return PravnoLiceFilter == false || item.PravnoLice == true;
                }
            }
            else
            {
                return false;
            }
        }
        private bool FilterFizickoLice(KomitentiNavigationItemViewModel? item)
        {
            if (item != null)
            {
                if (PravnoLiceFilter == FizickoLiceFilter)
                {
                    return true;
                }
                else
                {
                    return FizickoLiceFilter == false || item.FizickoLice == true;
                }
            }
            else
            {
                return false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            FilteredList.Filter -= filterDelegate;

            foreach (var item in Komitenti)
            {
                item?.Dispose();
            }

            SelectedKomitent?.Dispose();

            base.Dispose(disposing);
        }

        #endregion
    }
}
