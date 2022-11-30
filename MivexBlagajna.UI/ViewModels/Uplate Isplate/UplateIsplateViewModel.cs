using Castle.Core.Internal;
using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.Commands.Interfaces;
using MivexBlagajna.UI.Commands.Transakcije;
using MivexBlagajna.UI.Views.Services;
using MivexBlagajna.UI.Wrappers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;

namespace MivexBlagajna.UI.ViewModels.Uplate_Isplate
{
    public class UplateIsplateViewModel : ViewModelBase, IDockElement, IUplateIsplateViewModel
    {
        #region Fields
        private readonly DockState _dockState;
        private readonly string _header;
        private readonly ITransakcijeRepository _transakcijeRepository;
        private readonly IMessageDialogService _messageDialogService;
        private bool _hasChanges;
        private string? _komitentFilter;
        private TransakcijaWrapper _transakcija;

        #endregion

        #region Constructor
        public UplateIsplateViewModel(
            ITransakcijeRepository transakcijeRepository,
            IMessageDialogService messageDialogService
            )
        {
            _dockState = DockState.Document;
            _header = "Uplate / Isplate";
            _transakcijeRepository = transakcijeRepository;
            _messageDialogService = messageDialogService;

            Komitenti = new ObservableCollection<Komitent>();
            MestaTroska = new ObservableCollection<MestoTroska>();
            Konta = new ObservableCollection<Konto>();
            VrsteNaloga = new ObservableCollection<VrsteNaloga>();

            FilteredKomitenti = CollectionViewSource.GetDefaultView(Komitenti);

            FilteredKomitenti.Filter += new Predicate<object>(s => GetBySearch(s as Komitent));

            Transakcije = new ObservableCollection<TransakcijaWrapper>();
            UplateIsplate = CollectionViewSource.GetDefaultView(Transakcije);

            CreateTransakcijaCommand = new RelayCommand(CreateNewTransakcija);
            CreateBrojNalogaCommand = new RelayCommand(CreateBrojNaloga);
            SetFilterCommand = new RelayCommand(SetFilter);
            EditTransakcijaCommand = new RelayCommand(EditTransakcija);
            CancelCommand = new RelayCommand(CancelChange, CanCanceChange);

            SaveCommand = new SaveTransakcijaCommand(this);
            DeleteCommand = new DeleteTransakcijaCommand(this);
        }

        private bool CanCanceChange(object? obj)
        {
            return Transakcija != null ? HasChanges || Transakcija.IsEditable : false;
        }

        private void EditTransakcija(object? obj)
        {
            if (Transakcija != null)
            {
                BackupTransakcija = Transakcija.DeepClone();
                Transakcija.BeginEdit();
            }
        }

        private void SetFilter(object? obj)
        {
            if (obj != null && Transakcija.IsEditable == true)
            {
                var komitent = obj as Komitent;
                KomitentFilter = komitent?.Sifra.ToString();
                Transakcija.MestoTroska = komitent.MestoTroska;
            }
        }

        private bool GetBySearch(Komitent? komitent)
        {
            if (komitent?.Naziv != null)
            {
                if (komitent != null)
                {
                    return KomitentFilter == null || komitent.Naziv?.IndexOf(KomitentFilter, StringComparison.OrdinalIgnoreCase) != -1 || komitent.Sifra.ToString().Equals(KomitentFilter);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return komitent != null ? KomitentFilter == null || komitent.Sifra.ToString().Equals(KomitentFilter, StringComparison.OrdinalIgnoreCase) || komitent.Ime?.IndexOf(KomitentFilter, StringComparison.OrdinalIgnoreCase) != -1 || komitent.Prezime?.IndexOf(KomitentFilter, StringComparison.OrdinalIgnoreCase) != -1 : false;
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
                if (_hasChanges != value)
                {
                    var oldValue = _hasChanges;
                    _hasChanges = value;
                    OnModelPropertyChanged(oldValue, value);
                }
            }
        }

        public string Header
        {
            get { return _header; }
        }
        public DockState State
        {
            get { return _dockState; }
        }

        public TransakcijaWrapper BackupTransakcija { get; set; }
        public RelayCommand CreateTransakcijaCommand { get; }
        public RelayCommand CreateBrojNalogaCommand { get; }
        public RelayCommand SetFilterCommand { get; }
        public RelayCommand EditTransakcijaCommand { get; }
        public RelayCommand CancelCommand { get; }
        public IAsyncCommand SaveCommand { get; }
        public IAsyncCommand DeleteCommand { get; }
        public ICollectionView FilteredKomitenti { get; private set; }
        public ICollectionView UplateIsplate { get; private set; }
        public ObservableCollection<TransakcijaWrapper> Transakcije { get; }
        public ObservableCollection<Komitent> Komitenti { get; }
        public ObservableCollection<MestoTroska> MestaTroska { get; }
        public ObservableCollection<Konto> Konta { get; }
        public ObservableCollection<VrsteNaloga> VrsteNaloga { get; set; }
        public TransakcijaWrapper Transakcija
        {
            get { return _transakcija; }
            set
            {
                if (value == _transakcija) return;
                var oldValue = _transakcija;
                _transakcija = value;

                OnModelPropertyChanged(oldValue, value);

                if (Transakcija != null)
                {
                    Transakcija.PropertyChanged += (s, e) =>
                    {
                        if (!HasChanges)
                        {
                            HasChanges = _transakcijeRepository.HasChanges();
                        }
                    };
                }
            }
        }

        #endregion

        #region Methods
        public override async Task LoadAsync()
        {
            if (Komitenti.IsNullOrEmpty())
            {
                var listOfKomitenti = await _transakcijeRepository.GetAllKomitenti();
                foreach (var item in listOfKomitenti)
                {
                    Komitenti.Add(item);
                }
            }

            if (Konta.IsNullOrEmpty())
            {
                var kontaList = await _transakcijeRepository.GetAllKonta();
                foreach (var konto in kontaList)
                {
                    Konta.Add(konto);
                }
            }

            if (VrsteNaloga.IsNullOrEmpty())
            {
                var listaNaloga = await _transakcijeRepository.GetAllVrsteNaloga();
                foreach (var vrsta in listaNaloga)
                {
                    VrsteNaloga.Add(vrsta);
                }
            }

            if (MestaTroska.IsNullOrEmpty())
            {
                var mestaList = await _transakcijeRepository.GetAllMestaTroska();
                foreach (var mesto in mestaList)
                {
                    MestaTroska.Add(mesto);
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
            HasChanges = _transakcijeRepository.HasChanges();
            Transakcije.Add(Transakcija);
            UplateIsplate.Refresh();
            Transakcija.EndEdit();
        }
        public async Task DeleteTransakcijaAsync(TransakcijaWrapper transakcija)
        {
            var result = _messageDialogService.ShowOKCancelDialog("Izabrali ste da izbrišete transakciju. Da li ste sigurni?", "Question");

            if (result == MessageDialogResult.Potvrdi)
            {
                await _transakcijeRepository.DeleteAsync(transakcija.Model);
                Transakcija = Transakcije[Transakcije.IndexOf(transakcija) + 1];
                Transakcije.RemoveAt(Transakcije.IndexOf(transakcija));
                UplateIsplate.Refresh();
            }
        }
        private void CreateBrojNaloga(object? obj)
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
        private void CreateNewTransakcija(object? obj)
        {
            var transakcija = new Transakcija();
            _transakcijeRepository.Add(transakcija);
            transakcija.Datum = DateTime.Now;
            Transakcija = new TransakcijaWrapper(transakcija, true);
        }
        private void CancelChange(object? obj)
        {
            var result = _messageDialogService.ShowOKCancelDialog("Napravili ste promene. Da li želite da otkažete?", "Question");

            if (result == MessageDialogResult.Potvrdi)
            {
                _transakcijeRepository.CancelChanges();
                HasChanges = _transakcijeRepository.HasChanges();
                KomitentFilter = "";

                Transakcije[Transakcije.IndexOf(Transakcija)] = BackupTransakcija;
                Transakcija = Transakcije[Transakcije.IndexOf(BackupTransakcija)];

                UplateIsplate.Refresh();

                Transakcija?.EndEdit();
                FilteredKomitenti.Refresh();
            }
        }

        ~UplateIsplateViewModel()
        {
        }


        public override void Dispose()
        {
            base.Dispose();
        }


        #endregion
    }
}
