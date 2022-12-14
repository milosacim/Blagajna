using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.Commands.Transakcije;
using MivexBlagajna.UI.Views.Services;
using MivexBlagajna.UI.Wrappers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Uplate_Isplate
{
    public class UplateIsplateViewModel : ViewModelBase, IDockElement, IUplateIsplateViewModel
    {
        #region Fields
        private readonly DockState _dockState;
        private readonly string? _header;
        private readonly ITransakcijeRepository _transakcijeRepository;
        private readonly IMessageDialogService _messageDialogService;
        private bool _hasChanges;
        private TransakcijaWrapper? _transakcija;


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
            Transakcije = new ObservableCollection<TransakcijaWrapper>();

            CreateTransakcijaCommand = new RelayCommand(CreateNewTransakcija);
            CreateBrojNalogaCommand = new RelayCommand(CreateBrojNaloga, CanCreateBrojNaloga);
            EditTransakcijaCommand = new RelayCommand(EditTransakcija, CanEditTransakcija);
            CancelCommand = new RelayCommand(CancelChange, CanCanceChange);
            SaveCommand = new SaveTransakcijaCommand(this);
            DeleteCommand = new DeleteTransakcijaCommand(this);
        }

        private bool CanEditTransakcija(object? arg)
        {
            return Transakcija != null && !Transakcija.IsEditable;
        }

        private bool CanCreateBrojNaloga(object? arg)
        {
            if (Transakcija == null)
            {
                return true;
            }
            else
            {
                return Transakcija.IsEditable;
            }
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
        public RelayCommand EditTransakcijaCommand { get; }
        public RelayCommand CancelCommand { get; }
        public AsyncCommand SaveCommand { get; }
        public AsyncCommand DeleteCommand { get; }

        public ObservableCollection<TransakcijaWrapper> Transakcije { get; }
        public ObservableCollection<Komitent> Komitenti { get; }
        public ObservableCollection<MestoTroska> MestaTroska { get; }
        public ObservableCollection<Konto> Konta { get; }
        public ObservableCollection<VrsteNaloga> VrsteNaloga { get; }
        public TransakcijaWrapper? Transakcija
        {
            get { return _transakcija; }
            set
            {
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
            var listOfKomitenti = await _transakcijeRepository.GetKomitentiAsync();
            var kontaList = await _transakcijeRepository.GetAllKonta();
            var listaNaloga = await _transakcijeRepository.GetAllVrsteNaloga();
            var mestaList = await _transakcijeRepository.GetMestaTroskaAsync();
            var transakcije = await _transakcijeRepository.GetAllAsync();


            if (!Komitenti.Any())
            {
                foreach (var item in listOfKomitenti)
                {
                    Komitenti.Add(item);
                }
            }

            if (!Konta.Any())
            {
                foreach (var konto in kontaList)
                {
                    Konta.Add(konto);
                }
            }

            if (!VrsteNaloga.Any())
            {
                foreach (var vrsta in listaNaloga)
                {
                    VrsteNaloga.Add(vrsta);
                }
            }

            if (!MestaTroska.Any())
            {
                foreach (var mesto in mestaList)
                {
                    MestaTroska.Add(mesto);
                }
            }

            if (!Transakcije.Any())
            {
                foreach (var t in transakcije)
                {
                    Transakcije.Add(new TransakcijaWrapper(t, false));
                }
            }

        }
        public async Task SaveTransakcijaAsync()
        {
            await _transakcijeRepository.SaveAsync();
            Transakcija.EndEdit();
            HasChanges = _transakcijeRepository.HasChanges();
        }
        public async Task DeleteTransakcijaAsync(TransakcijaWrapper transakcija)
        {
            var result = _messageDialogService.ShowOKCancelDialog("Izabrali ste da izbrišete transakciju. Da li ste sigurni?", "Question");

            if (result == MessageDialogResult.Potvrdi)
            {
                await _transakcijeRepository.DeleteAsync(transakcija.Model);
            }
        }
        private void CreateBrojNaloga(object? obj)
        {
            if (Transakcija != null && Transakcija.IsEditable)
            {
                var vrsta = obj as VrsteNaloga;

                switch (vrsta?.VrstaNaloga)
                {
                    case "Dnevnica":
                        {
                            Transakcija.Nalog = $"DN - {Transakcije.Where(t => t.VrstaNaloga.VrstaNaloga == vrsta.VrstaNaloga).Count() + 1}";
                            break;
                        }

                    case "Plata":
                        {
                            Transakcija.Nalog = $"PL - {Transakcije.Where(t => t.VrstaNaloga.VrstaNaloga == vrsta.VrstaNaloga).Count() + 1}";
                            break;
                        }

                    case "Trošak":
                        {
                            Transakcija.Nalog = $"TR - {Transakcije.Where(t => t.VrstaNaloga.VrstaNaloga == vrsta.VrstaNaloga).Count() + 1}";
                            break;
                        }

                    case "Pazar":
                        {
                            Transakcija.Nalog = $"PA - {Transakcije.Where(t => t.VrstaNaloga.VrstaNaloga == vrsta.VrstaNaloga).Count() + 1}";
                            break;
                        }

                    case "Pocetno stanje":
                        {
                            Transakcija.Nalog = $"PS - {Transakcije.Where(t => t.VrstaNaloga.VrstaNaloga == vrsta.VrstaNaloga).Count() + 1}";
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
            BackupTransakcija = Transakcija != null ? Transakcija : Transakcije.FirstOrDefault();
            Transakcija = new TransakcijaWrapper(transakcija, true);
        }

        private void CancelChange(object? obj)
        {
            var result = _messageDialogService.ShowOKCancelDialog("Napravili ste promene. Da li želite da otkažete?", "Question");

            if (result == MessageDialogResult.Potvrdi)
            {
                _transakcijeRepository.CancelChanges();
                HasChanges = _transakcijeRepository.HasChanges();
                Transakcija = BackupTransakcija;
                BackupTransakcija?.EndEdit();
                Transakcija?.EndEdit();
            }
        }

        protected override void Dispose(bool disposing)
        {
            _transakcija = null;
            base.Dispose(disposing);
        }

        #endregion
    }
}
