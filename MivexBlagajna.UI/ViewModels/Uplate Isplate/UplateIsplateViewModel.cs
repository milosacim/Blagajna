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
        private TransakcijaWrapper _transakcija;

        #endregion

        #region Constructor
        public UplateIsplateViewModel(
            IKomitentRepository komitentRepository,
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
            SelectKontoCommand = new RelayCommand(SelectKonto);
            SelectVrstaCommand = new RelayCommand(SelectVrsta);

        }

        public void SelectKonto(object? obj)
        {
            if (obj is VrsteKontaEnum && Transakcija.Id == 0)
            {
                switch (obj)
                {
                    case VrsteKontaEnum.DINARI:
                        Transakcija.Konto = Konta.FirstOrDefault(k => k.Naziv == "Dinarski");
                        break;

                    case VrsteKontaEnum.CEKOVI:
                        Transakcija.Konto = Konta.FirstOrDefault(k => k.Naziv == "Cekovi");
                        break;

                    case VrsteKontaEnum.EURO:
                        Transakcija.Konto = Konta.FirstOrDefault(k => k.Naziv == "Devizni");
                        break;
                }
            }
        }

        private void CreateNewTransakcija(object? obj = null)
        {
            var transakcija = new Transakcija();
            transakcija.Datum = DateTime.Now;
            _transakcijeRepository.Add(transakcija);
            Transakcija = new TransakcijaWrapper(transakcija, true);
        }

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
                Transakcija.Komitent = FilteredKomitent.Model;
                Transakcija.MestoTroska = Transakcija.Komitent.MestoTroska;
            }
        }

        public KomitentWrapper? FilteredKomitent
        {
            get { return SearchKomitentText != null ? Komitenti.FirstOrDefault(x => x.Sifra.ToString().Equals(SearchKomitentText, StringComparison.OrdinalIgnoreCase)) : null; }
        }

        public ICommand SelectKontoCommand { get; }
        public ICommand CreateTransakcijaCommand { get; }
        public ICommand SelectVrstaCommand { get; }


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


        public void SelectVrsta(object? obj)
        {
            if (obj is VrsteNalogaEnum && Transakcija.Id == 0)
            {
                switch (obj)
                {
                    case VrsteNalogaEnum.DNEVNICA:
                        Transakcija.VrstaNaloga = "Dinarski";
                        Transakcija.Nalog = String.Format($"D-{0}", (Transakcije.Where(t => t.VrstaNaloga == Transakcija.VrstaNaloga).ToList().Count() + 1).ToString());
                        break;
                    case VrsteNalogaEnum.TROSAK:
                        Transakcija.VrstaNaloga = "Trosak";
                        Transakcija.Nalog = String.Format($"D-{0}", (Transakcije.Where(t => t.VrstaNaloga == Transakcija.VrstaNaloga).ToList().Count() + 1).ToString());
                        break;
                    case VrsteNalogaEnum.PLATA:
                        Transakcija.VrstaNaloga = "Plata";
                        Transakcija.Nalog = String.Format($"D-{0}", (Transakcije.Where(t => t.VrstaNaloga == Transakcija.VrstaNaloga).ToList().Count() + 1).ToString());
                        break;
                    case VrsteNalogaEnum.PAZAR:
                        Transakcija.VrstaNaloga = "Pazar";
                        Transakcija.Nalog = String.Format($"D-{0}", (Transakcije.Where(t => t.VrstaNaloga == Transakcija.VrstaNaloga).ToList().Count() + 1).ToString());
                        break;
                } 
            }
        }

        #endregion
    }
}
