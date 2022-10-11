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
        private readonly IMestoTroskaRepository _mestoTroskaRepository;
        private readonly IKontoRepository _kontoRepository;
        private readonly ITransakcijeRepository _transakcijeRepository;

        private string? _searchKomitentText;
        private ICommand _selectKontoCommand;
        private VrsteNalogaEnum _selectedVrstaNaloga;

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
            _mestoTroskaRepository = mestoTroskaRepository;
            _kontoRepository = kontoRepository;
            _transakcijeRepository = transakcijeRepository;

            Komitenti = new ObservableCollection<KomitentWrapper>();
            MestaTroska = new ObservableCollection<MestoTroskaWrapper>();
            Transakcije = new ObservableCollection<TransakcijaWrapper>();
            Konta = new ObservableCollection<Konto>();

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
        public VrsteNalogaEnum SelectedVrstaNaloga
        {
            get { return _selectedVrstaNaloga; }
            set
            {
                var oldValue = _selectedVrstaNaloga;
                _selectedVrstaNaloga = value;
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
                return _selectKontoCommand ??= new RelayCommand(x => { SelectKonto(SelectedVrstaNaloga); });
            }
        }

        public ObservableCollection<KomitentWrapper> Komitenti { get; }
        public ObservableCollection<MestoTroskaWrapper> MestaTroska { get; }
        public ObservableCollection<Konto> Konta { get; }
        public TransakcijaWrapper Transakcija { get; set; }
        public ObservableCollection<TransakcijaWrapper> Transakcije { get; }
        #endregion

        #region Methods
        public override async Task LoadAsync()
        {
            var listOfKomitenti = await _komitentRepository.GetAllAsync();
            Komitenti.Clear();
            foreach (var item in listOfKomitenti)
            {
                var komitent = new KomitentWrapper(item);
                Komitenti.Add(komitent);
            }

            var konta = await _kontoRepository.GetAllAsync();

            foreach (var konto in konta)
            {
                Konta.Add(konto);
            }
        }
        public async Task<Transakcija> CreateNewTransakcija()
        {
            var transakcija = new Transakcija();
            return transakcija;
        }

        public Konto SelectKonto(VrsteNalogaEnum vrstaNaloga)
        {
            switch (vrstaNaloga)
            {
                case VrsteNalogaEnum.DINARI:
                    SelectedKonto = Konta.FirstOrDefault(k => k.Naziv == "Dinarski");
                    return SelectedKonto;

                case VrsteNalogaEnum.CEKOVI:
                    SelectedKonto = Konta.FirstOrDefault(k => k.Naziv == "Cekovi");
                    return SelectedKonto;

                case VrsteNalogaEnum.EURO:
                    SelectedKonto = Konta.FirstOrDefault(k => k.Naziv == "Devizni");
                    return SelectedKonto;

                default:
                    return null;
            }
        }


        #endregion
    }
}
