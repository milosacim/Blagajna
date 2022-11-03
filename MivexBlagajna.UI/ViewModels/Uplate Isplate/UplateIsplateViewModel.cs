using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.Wrappers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MivexBlagajna.UI.ViewModels.Uplate_Isplate
{
    public class UplateIsplateViewModel : ViewModelBase, IDockElement, IUplateIsplateViewModel
    {
        #region Fields
        private readonly DockState _dockState;
        private readonly string _header;
        private readonly IKomitentRepository _komitentRepository;
        private readonly IKontoRepository _kontoRepository;
        private readonly ITransakcijeRepository _transakcijeRepository;

        private string? _searchKomitentText;
        private ICommand _selectKontoCommand;
        private VrsteKontaEnum _selectedVrstaKonta;
        private TransakcijaWrapper _transakcija;
        private KomitentWrapper? _filteredKomitent;

        #endregion

        #region Constructor
        public UplateIsplateViewModel(
            IKomitentRepository komitentRepository,
            IMestoTroskaRepository mestoTroskaRepository,
            IKontoRepository kontoRepository,
            ITransakcijeRepository transakcijeRepository
            )
        {
            _dockState = DockState.Document;
            _header = "Uplate / Isplate";
            _komitentRepository = komitentRepository;
            _kontoRepository = kontoRepository;
            _transakcijeRepository = transakcijeRepository;

            Komitenti = new ObservableCollection<KomitentWrapper>();
            Transakcije = new ObservableCollection<TransakcijaWrapper>();
            Konta = new ObservableCollection<Konto>();

            CreateTransakcijaCommand = new RelayCommand(CreateNewTransakcija);

        }

        private void CreateNewTransakcija(object? obj = null)
        {
            var transakcija = new Transakcija();
            transakcija.Datum = DateTime.Now;
            _transakcijeRepository.Add(transakcija);
            Transakcija = new TransakcijaWrapper(transakcija, true);
        }
        #endregion

        #region Properties
        public DockState State
        {
            get { return _dockState; }
            set { }
        }
        public string Header
        {
            get { return _header; }
            set { }
        }
        public string SearchKomitentText
        {
            get { return _searchKomitentText; }
            set
            {
                var oldValue = _searchKomitentText;
                _searchKomitentText = value;
                OnModelPropertyChanged(oldValue, value, nameof(FilteredKomitent));
            }
        }
        public VrsteKontaEnum SelectedVrstaKonta
        {
            get { return _selectedVrstaKonta; }
            set
            {
                var oldValue = _selectedVrstaKonta;
                _selectedVrstaKonta = value;
                Transakcija.Konto = SelectedKonto;
                OnModelPropertyChanged(oldValue, value);
            }
        }

        public KomitentWrapper? FilteredKomitent
        {
            get { return SearchKomitentText != null ? Komitenti.FirstOrDefault(x => x.Sifra.ToString().Equals(SearchKomitentText, StringComparison.OrdinalIgnoreCase)) : null; }
        }
        public Konto? SelectedKonto { get; set; }
        public ICommand SelectKontoCommand
        {
            get
            {
                return _selectKontoCommand ??= new RelayCommand(x => { SelectKonto(SelectedVrstaKonta); });
            }
        }

        public ObservableCollection<KomitentWrapper> Komitenti { get; }
        public ObservableCollection<Konto> Konta { get; }
        public TransakcijaWrapper Transakcija
        {
            get { return _transakcija; }
            set
            {
                var oldValue = _transakcija;
                _transakcija = value;
                OnModelPropertyChanged(oldValue, value);
            }
        }

        public ObservableCollection<TransakcijaWrapper> Transakcije { get; }
        #endregion

        #region Methods
        public override async Task LoadAsync()
        {
            Komitenti.Clear();
            var listOfKomitenti = await _komitentRepository.GetAllAsync();
            foreach (var item in listOfKomitenti)
            {
                var komitent = new KomitentWrapper(item, false, false, false);
                Komitenti.Add(komitent);
            }

            Konta.Clear();
            var kontaList = await _kontoRepository.GetAllAsync();
            foreach (var konto in kontaList)
            {
                Konta.Add(konto);
            }
        }

        public RelayCommand CreateTransakcijaCommand { get; }

        public Konto SelectKonto(VrsteKontaEnum vrstaNaloga)
        {
            switch (vrstaNaloga)
            {
                case VrsteKontaEnum.DINARI:
                    SelectedKonto = Konta.FirstOrDefault(k => k.Naziv == "Dinarski");
                    return SelectedKonto;

                case VrsteKontaEnum.CEKOVI:
                    SelectedKonto = Konta.FirstOrDefault(k => k.Naziv == "Cekovi");
                    return SelectedKonto;

                case VrsteKontaEnum.EURO:
                    SelectedKonto = Konta.FirstOrDefault(k => k.Naziv == "Devizni");
                    return SelectedKonto;

                default:
                    return null;
            }
        }


        #endregion
    }
}
