using Castle.Core.Internal;
using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.Commands.Interfaces;
using MivexBlagajna.UI.Commands.Transakcije;
using MivexBlagajna.UI.Wrappers;
using Syncfusion.Windows.Controls.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
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

        private TransakcijaWrapper _transakcija;
        private List<VrsteNaloga> _vrsteNaloga;
        private bool _hasChanges;
        private string? _komitentFilter;

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

            MestaTroska = new ObservableCollection<MestoTroska>();
            Komitenti = new ObservableCollection<Komitent>();
            FilteredKomitenti = CollectionViewSource.GetDefaultView(Komitenti);
            FilteredKomitenti.Filter += new Predicate<object>(s => GetBySearch(s as Komitent));
            Transakcije = new ObservableCollection<TransakcijaWrapper>();
            Konta = new ObservableCollection<Konto>();
            VrsteNaloga = new List<VrsteNaloga>();

            CreateTransakcijaCommand = new RelayCommand(CreateNewTransakcija);
            CreateBrojNalogaCommand = new RelayCommand(CreateBrojNaloga);
            SelectMestoTroskaCommand = new RelayCommand(SelectMestoTroska);

            SaveCommand = new SaveTransakcijaCommand(this);
        }

        private void SelectMestoTroska(object? obj)
        {
            if (obj != null)
            {
                var komitent = obj as Komitent;
                Transakcija.MestoTroska = komitent.MestoTroska;
            }
        }

        private bool GetBySearch(Komitent? komitent)
        {
            if (komitent.Naziv != null)
            {
                return komitent != null ? KomitentFilter == null || komitent.Naziv.IndexOf(KomitentFilter, StringComparison.OrdinalIgnoreCase) != -1 || komitent.Sifra.ToString().StartsWith(KomitentFilter, StringComparison.OrdinalIgnoreCase) : false;
            }
            else
            {
                return komitent != null ? KomitentFilter == null || komitent.Sifra.ToString().StartsWith(KomitentFilter, StringComparison.OrdinalIgnoreCase) || komitent.Ime.IndexOf(KomitentFilter, StringComparison.OrdinalIgnoreCase) != -1 || komitent.Prezime.IndexOf(KomitentFilter, StringComparison.OrdinalIgnoreCase) != -1 : false;
            }
        }

        public string? KomitentFilter
        {
            get { return _komitentFilter; }
            set
            {
                var oldValue = _komitentFilter;
                _komitentFilter = value;
                OnModelPropertyChanged(oldValue, value);
                FilteredKomitenti.Refresh();
            }
        }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                var oldValue = _hasChanges;
                _hasChanges = value;
                OnModelPropertyChanged(oldValue, value);
            }
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
        public ICommand CreateTransakcijaCommand { get; }
        public ICommand CreateBrojNalogaCommand { get; }
        public ICommand SelectMestoTroskaCommand { get; }
        public IAsyncCommand SaveCommand { get; }

        public ObservableCollection<Komitent> Komitenti { get; }
        public ObservableCollection<MestoTroska> MestaTroska { get; }
        public ICollectionView FilteredKomitenti { get; private set; }
        public ObservableCollection<TransakcijaWrapper> Transakcije { get; }
        public ObservableCollection<Konto> Konta { get; }

        public List<VrsteNaloga> VrsteNaloga
        {
            get { return _vrsteNaloga; }
            set
            {
                var oldValue = _vrsteNaloga;
                _vrsteNaloga = value;
                OnModelPropertyChanged(oldValue, value);
            }
        }
        public TransakcijaWrapper Transakcija
        {
            get { return _transakcija; }
            set
            {
                var oldValue = _transakcija;
                _transakcija = value;
                OnModelPropertyChanged(oldValue, value);

                Transakcija.PropertyChanged += (s, e) =>
                {
                    if (!HasChanges)
                    {
                        HasChanges = _transakcijeRepository.HasChanges();
                    }
                };
            }
        }


        #endregion

        #region Methods
        public override async Task LoadAsync()
        {
            if (Komitenti.IsNullOrEmpty())
            {
                var listOfKomitenti = await _komitentRepository.GetAllAsync();
                foreach (var item in listOfKomitenti)
                {
                    Komitenti.Add(item);
                }
            }

            if (Konta.IsNullOrEmpty())
            {
                var kontaList = await _kontoRepository.GetAllAsync();
                foreach (var konto in kontaList)
                {
                    Konta.Add(konto);
                }
            }

            if (MestaTroska.IsNullOrEmpty())
            {
                var mestaList = await _mestoTroskaRepository.GetAll();
                foreach (var mesto in mestaList)
                {
                    MestaTroska.Add(mesto);
                }
            }

            if (VrsteNaloga.IsNullOrEmpty())
            {
                var listaNaloga = _transakcijeRepository.GetAllVrsteNaloga();
                foreach (var vrsta in listaNaloga)
                {
                    VrsteNaloga.Add(vrsta);
                }
            }

            if (Transakcije.IsNullOrEmpty())
            {
                var transakcije = await _transakcijeRepository.GetAllAsync();
                foreach (var t in transakcije)
                {
                    Transakcije.Add(new TransakcijaWrapper(t, false));
                }
            }
        }
        public async Task SaveTransakcijaAsync()
        {
            await _transakcijeRepository.SaveAsync();
        }
        public void CreateBrojNaloga(object? obj)
        {
            if (HasChanges)
            {
                var vrsta = obj as VrsteNaloga;

                switch (vrsta.VrstaNaloga)
                {
                    case "Dnevnica":
                        {
                            Transakcija.Nalog = String.Format("DN - {0}", (Transakcije.Where(t => t.VrstaNaloga.VrstaNaloga == vrsta.VrstaNaloga).Count() + 1).ToString());
                            break;
                        }

                    case "Plata":
                        {
                            Transakcija.Nalog = String.Format("PL - {0}", (Transakcije.Where(t => t.VrstaNaloga.VrstaNaloga == vrsta.VrstaNaloga).Count() + 1).ToString());
                            break;
                        }

                    case "Trosak":
                        {
                            Transakcija.Nalog = String.Format("TR - {0}", (Transakcije.Where(t => t.VrstaNaloga.VrstaNaloga == vrsta.VrstaNaloga).Count() + 1).ToString());
                            break;
                        }

                    case "Pazar":
                        {
                            Transakcija.Nalog = String.Format("PA - {0}", (Transakcije.Where(t => t.VrstaNaloga.VrstaNaloga == vrsta.VrstaNaloga).Count() + 1).ToString());
                            break;
                        }
                }
            }
        }
        private void CreateNewTransakcija(object? obj)
        {
            var transakcija = new Transakcija();
            _transakcijeRepository.Add(transakcija);


            transakcija.Datum = DateTime.Now;
            Transakcija = new TransakcijaWrapper(transakcija, true);
            //SearchKomitentText = "";
        }

        #endregion
    }
}
