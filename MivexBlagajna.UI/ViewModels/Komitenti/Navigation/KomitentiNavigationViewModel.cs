using MivexBlagajna.DataAccess.Services.Lookups;
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
        private string _nazivFilter;
        private bool _pravnoLiceFilter;
        private bool _fizickoLiceFilter;

        private KomitentiNavigationItemViewModel? _selectedKomitent;

        public event EventHandler<SelectedKomitentArgs> OnkomitentSelected;

        #endregion

        #region Constructor
        public KomitentiNavigationViewModel(ILookupKomitentDataService lookupKomitentDataService)
        {
            _lookupKomitentDataService = lookupKomitentDataService;
            Komitenti = new ObservableCollection<KomitentiNavigationItemViewModel>();
            FilteredList = CollectionViewSource.GetDefaultView(Komitenti);
            FilteredList.Filter += new Predicate<object>(o => FilterNaziv(o as KomitentiNavigationItemViewModel) && FilterPravnoLice(o as KomitentiNavigationItemViewModel) && FilterFizickoLice(o as KomitentiNavigationItemViewModel));
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
                OnkomitentSelected?.Invoke(this, new SelectedKomitentArgs(_selectedKomitent.Id));
                OnModelPropertyChanged();
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
        public async override Task LoadAsync()
        {
            Komitenti.Clear();
            var lookup = await _lookupKomitentDataService.GetLookupKomitentAsync();
            foreach (var item in lookup)
            {
                if (item != null)
                {
                    Komitenti.Add(new KomitentiNavigationItemViewModel(item.Id, item.PunNaziv, item.PravnoLice, item.FizickoLice, item.MestoTroska));
                }
            }
            SelectedKomitent = Komitenti.FirstOrDefault();
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
                return PravnoLiceFilter == false || item.PravnoLice == true;
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
                return FizickoLiceFilter == false || item.FizickoLice == true;
            }
        }
        public override void Dispose()
        {
            SelectedKomitent.Dispose();
            base.Dispose();
        }

        #endregion
    }
}
