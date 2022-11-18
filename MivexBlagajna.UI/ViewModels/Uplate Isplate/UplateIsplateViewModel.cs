using Castle.Core.Internal;
using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.Commands.Interfaces;
using MivexBlagajna.UI.Commands.Transakcije;
using MivexBlagajna.UI.EventArgs;
using MivexBlagajna.UI.ViewModels.Komitenti.Details;
using MivexBlagajna.UI.Views.Services;
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
        private readonly IKontoRepository _kontoRepository;
        private readonly ITransakcijeRepository _transakcijeRepository;
        private readonly IMessageDialogService _messageDialogService;
        private bool _hasChanges;
        private string? _komitentFilter;
        private TransakcijaWrapper _transakcija;

        public event EventHandler<SelectedTransakcijaArgs>? OnTransakcijaSelected;

        #endregion

        #region Constructor
        public UplateIsplateViewModel(
            IKomitentRepository komitentRepository,
            IKontoRepository kontoRepository,
            ITransakcijeRepository transakcijeRepository,
            IMessageDialogService messageDialogService
            )
        {
            _dockState = DockState.Document;
            _header = "Uplate / Isplate";
            _komitentRepository = komitentRepository;
            _kontoRepository = kontoRepository;
            _transakcijeRepository = transakcijeRepository;
            _messageDialogService = messageDialogService;

            Komitenti = new ObservableCollection<Komitent>();

            FilteredKomitenti = CollectionViewSource.GetDefaultView(Komitenti);
            FilteredKomitenti.Filter += new Predicate<object>(s => GetBySearch(s as Komitent));

            Transakcije = new ObservableCollection<TransakcijaWrapper>();

            Konta = new List<Konto>();
            VrsteNaloga = new List<VrsteNaloga>();

            CreateTransakcijaCommand = new RelayCommand(CreateNewTransakcija);
            CreateBrojNalogaCommand = new RelayCommand(CreateBrojNaloga);
            SetRelatedDataCommand = new RelayCommand(SetRelatedData);
            EditTransakcijaCommand = new RelayCommand(EditTransakcija);

            CancelCommand = new TransakcijaCancelChangeCommand(this);

            SaveCommand = new SaveTransakcijaCommand(this);
        }

        private void EditTransakcija(object? obj)
        {
            if (Transakcija != null)
            {
                var model = (UplateIsplateViewModel)this.MemberwiseClone();
                BackupTransakcija = model.Transakcija;
                Transakcija.BeginEdit();
            }
        }
        private void SetRelatedData(object? obj)
        {
            if (obj != null)
            {
                var komitent = obj as Komitent;
                Transakcija.MestoTroska = komitent.MestoTroska;
                KomitentFilter = komitent.Sifra.ToString();
            }
        }
        private bool GetBySearch(Komitent? komitent)
        {
            if (komitent.Naziv != null)
            {
                return komitent != null ? KomitentFilter == null || komitent.Naziv.IndexOf(KomitentFilter, StringComparison.OrdinalIgnoreCase) != -1 || komitent.Sifra.ToString().Equals(KomitentFilter) : false;
                
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
        public ICommand SetRelatedDataCommand { get; }
        public ICommand EditTransakcijaCommand { get; }
        public ICommand SetSifraCommand { get; private set; }
        public IAsyncCommand CancelCommand { get; }
        public IAsyncCommand SaveCommand { get; }

        public ICollectionView FilteredKomitenti { get; private set; }
        public ICollectionView UplateIsplate { get; set; }
        public ObservableCollection<TransakcijaWrapper> Transakcije { get; }
        public ObservableCollection<Komitent> Komitenti { get; }
        public List<Konto> Konta { get; }
        public List<VrsteNaloga> VrsteNaloga { get; set; }
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
        public TransakcijaWrapper BackupTransakcija { get; set; }

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
                UplateIsplate = CollectionViewSource.GetDefaultView(Transakcije);
            }
        }
        public async Task SaveTransakcijaAsync()
        {
            await _transakcijeRepository.SaveAsync();
            Transakcija.EndEdit();
        }
        public async Task CancelChange()
        {
            var result = _messageDialogService.ShowOKCancelDialog("Napravili ste promene. Da li želite da otkažete?", "Question");

            if (result == MessageDialogResult.Potvrdi)
            {
                _transakcijeRepository.CancelChanges();
                HasChanges = _transakcijeRepository.HasChanges();
                Transakcija?.EndEdit();
            }
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
        }

        #endregion
    }
}
