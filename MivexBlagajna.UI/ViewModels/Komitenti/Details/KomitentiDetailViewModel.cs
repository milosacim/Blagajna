using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.Commands.Interfaces;
using MivexBlagajna.UI.Commands.Komitenti;
using MivexBlagajna.UI.EventArgs;
using MivexBlagajna.UI.ViewModels.Komitenti.Interfaces;
using MivexBlagajna.UI.Views.Services;
using MivexBlagajna.UI.Wrappers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MivexBlagajna.UI.ViewModels.Komitenti.Details
{
    public class KomitentiDetailViewModel : ViewModelBase, IKomitentiDetailViewModel
    {
        #region Fields
        private readonly IKomitentRepository _komitentRepository;
        private readonly IMessageDialogService _messageDialogService;

        private KomitentWrapper? _komitent;
        private KomitentWrapper? _backupkomitent;

        private bool _hasChanges;

        public event EventHandler<KomitentDeletedArgs>? OnKomitentDeleted;
        public event EventHandler<KomitentSavedArgs>? OnKomitentSaved;

        #endregion

        #region Konstruktor
        public KomitentiDetailViewModel(
            IKomitentRepository komitentRepository
            , IMessageDialogService messageDialogService)
        {
            _komitentRepository = komitentRepository;
            _messageDialogService = messageDialogService;

            EditKomitentPropertyCommand = new RelayCommand(EditKomitentProperty);

            SaveCommand = new SaveKomitentCommand(this);
            CreateNewKomitentCommand = new CreateNewKomitentCommand(this);
            DeleteCommand = new DeleteCommand(this);
            CancelCommand = new CancelCommand(this);

            MestaTroska = new ObservableCollection<MestoTroska>();
        }

        #endregion

        #region Properties
        public ObservableCollection<MestoTroska> MestaTroska { get; }
        public KomitentWrapper? Komitent
        {
            get { return _komitent; }
            set
            {
                var oldValue = _komitent;
                _komitent = value;
                OnModelPropertyChanged(oldValue, value);

                Komitent.PropertyChanged += (s, e) =>
                {
                    if (!HasChanges)
                    {
                        HasChanges = _komitentRepository.HasChanges();

                    }
                };
            }
        }
        public KomitentWrapper? BackupKomitent
        {
            get { return _backupkomitent; }
            set
            {
                var oldValue = _backupkomitent;
                _backupkomitent = value;
                OnModelPropertyChanged(oldValue, value);
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

        public IAsyncCommand SaveCommand { get; }
        public IAsyncCommand CancelCommand { get; }
        public IAsyncCommand CreateNewKomitentCommand { get; }
        public IAsyncCommand DeleteCommand { get; }
        public ICommand EditKomitentPropertyCommand { get; }

        #endregion

        #region Methods

        private void EditKomitentProperty(object? obj = null)
        {
            var model = (KomitentiDetailViewModel)this.MemberwiseClone();
            BackupKomitent = model.Komitent;
            Komitent?.BeginEdit();
        }
        public KomitentWrapper CreateNewKomitent()
        {
            var model = (KomitentiDetailViewModel)this.MemberwiseClone();
            BackupKomitent = model.Komitent;

            var komitent = new Komitent();
            _komitentRepository.Add(komitent);

            Komitent = new KomitentWrapper(komitent, true, true, true);

            return Komitent;
        }
        public async Task LoadAsync(int? komitentId)
        {
            await LoadMestaTroskaAsync();
            await InitializeKomitent(komitentId);
        }

        private async Task InitializeKomitent(int? komitentId)
        {
            if (komitentId == null)
            {
                CreateNewKomitent();
            }

            else
            {
                var komitent = await _komitentRepository.GetByIdAsync(komitentId.Value);
                Komitent = new KomitentWrapper(komitent, false, false, false);
            }
        }

        public async Task LoadMestaTroskaAsync()
        {
            MestaTroska.Clear();
            var mestaTroska = await _komitentRepository.GetAllMestaTroska();
            foreach (var mesto in mestaTroska)
            {
                MestaTroska.Add(mesto);
            }
        }
        public async Task SaveKomitentAsync()
        {
            await _komitentRepository.SaveAsync();

            if (Komitent != null)
            {
                if (!Komitent.HasErrors)
                {
                    OnKomitentSaved?.Invoke(this, new KomitentSavedArgs(
                        Komitent.Id,
                        Komitent.PravnoLice == true ? $"{Komitent.Sifra} - {Komitent.Naziv}" : $"{Komitent.Sifra} - {Komitent.Ime} {Komitent.Prezime}"
                        , Komitent.PravnoLice
                        , Komitent.FizickoLice));
                }
            }

            HasChanges = _komitentRepository.HasChanges();
            Komitent?.EndEdit();
        }
        public async Task DeleteKomitentAsync()
        {
            if (Komitent != null)
            {
                var result = _messageDialogService.ShowOKCancelDialog("Da li ste sigurni da zelite da obrisete komitenta?", "Question");
                if (result == MessageDialogResult.Potvrdi)
                {
                    await _komitentRepository.DeleteAsync(Komitent.Model);
                    OnKomitentDeleted?.Invoke(this, new KomitentDeletedArgs(Komitent.Id));
                }
                else
                {
                    return;
                }
            }

            if (Komitent?.IsEditable == true)
            {
                Komitent?.EndEdit();
            }
        }
        public async Task CancelChange()
        {
            var result = _messageDialogService.ShowOKCancelDialog("Napravili ste promene? Da li zelite da otkazete?", "Question");
            if (result == MessageDialogResult.Potvrdi)
            {
                _komitentRepository.CancelChanges();
                HasChanges = _komitentRepository.HasChanges();
            }
            
            if (Komitent != null)
            {
                Komitent?.EndEdit();
                await LoadAsync(BackupKomitent?.Id);
            }
        }
        public override void Dispose()
        {
            if (HasChanges)
            {
                _komitentRepository.CancelChanges();
                HasChanges = _komitentRepository.HasChanges();
            }

            base.Dispose();
        }

        #endregion
    }


}
