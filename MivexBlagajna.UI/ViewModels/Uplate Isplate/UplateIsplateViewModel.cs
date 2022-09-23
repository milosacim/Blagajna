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

        private readonly DockState _dockState;
        private readonly string _header;
        private readonly IKomitentRepository _komitentRepository;
        private readonly IMestoTroskaRepository _mestoTroskaRepository;
        private readonly IKontoRepository _kontoRepository;
        private readonly ITransakcijeRepository _transakcijeRepository;

        private string? _searchKomitentText;
        private ICommand _selectKontoCommand;



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
            Transakcije = new ObservableCollection<Transakcija>();
            Konta = new ObservableCollection<Konto>();

            SelectKontoCommand = new DelegateCommand(SelectKonto);
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
            set { _searchKomitentText = value; OnModelPropertyChanged(); OnModelPropertyChanged(nameof(FilteredKomitent)); }
        }

        public KomitentWrapper? FilteredKomitent
        {
            get { return SearchKomitentText != null ? Komitenti.FirstOrDefault(x => x.Sifra.ToString().Equals(SearchKomitentText, StringComparison.OrdinalIgnoreCase)) : null; }
        }

        public ObservableCollection<KomitentWrapper> Komitenti { get; }
        public ObservableCollection<MestoTroskaWrapper> MestaTroska { get; }
        public ObservableCollection<Konto> Konta { get; }

        public ObservableCollection<Transakcija> Transakcije { get; }

        public object? SelectedKonto { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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


        public ICommand SelectKontoCommand
        {
            get
            {
                return _selectKontoCommand ?? (_selectKontoCommand = new RelayCommand(x => { SelectKonto() }));
            }
        }

        public async Task<Transakcija> CreateNewTransakcija()
        {
            //if (FilteredKomitent != null)
            //{
            //    var komitent = FilteredKomitent;
            //    var mesto = FilteredKomitent.MestoTroska;
            //    var konta = SelectedKonto;
            //}
            return null;
        }
        public object SelectKonto(VrsteNalogaEnum vrstaNaloga)
        {
            switch (vrstaNaloga)
            {
                case VrsteNalogaEnum.DINARI:
                    if (SelectedKonto != null)
                    {
                        SelectedKonto = Konta.FirstOrDefault(k => k.Naziv == "Dinarski");
                    }

                    return SelectedKonto;
                    break;

                case VrsteNalogaEnum.CEKOVI:
                    if (SelectedKonto != null)
                    {
                        SelectedKonto = Konta.FirstOrDefault(k => k.Naziv == "Cekovi");
                    }
                    return SelectedKonto;
                    break;

                case VrsteNalogaEnum.EURO:
                    if (SelectedKonto != null)
                    {
                        SelectedKonto = Konta.FirstOrDefault(k => k.Naziv == "Devizni");
                    }
                    return SelectedKonto;
                    break;

                default:
                    throw new ArgumentException("Morate izabrati konto", "vrstaNaloga");
                    break;
            }
        }
    }
}
