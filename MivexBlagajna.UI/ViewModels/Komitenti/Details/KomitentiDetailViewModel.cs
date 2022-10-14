using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.Commands.Interfaces;
using MivexBlagajna.UI.Commands.Komitenti;
using MivexBlagajna.UI.ViewModels.Komitenti.Interfaces;
using MivexBlagajna.UI.Views.Services;
using MivexBlagajna.UI.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MivexBlagajna.UI.ViewModels.Komitenti.Details
{
    public class KomitentiDetailViewModel : ViewModelBase, IKomitentiDetailViewModel, IEditableObject
    {
        #region Fields
        private readonly IKomitentRepository _komitentRepository;
        private readonly IMestoTroskaRepository _mestoTroskaRepository;
        private readonly IMessageDialogService _messageDialogService;

        private KomitentWrapper? _komitent;
        private KomitentWrapper? _backupkomitent;

        private bool _hasChanges;
        private bool _isPravnoLiceEditable;
        private bool _isFizickoLiceEditable;

        public event EventHandler<KomitentDeletedArgs>? OnKomitentDeleted;
        public event EventHandler<KomitentSavedArgs>? OnKomitentSaved;

        #endregion

        #region Konstruktor
        public KomitentiDetailViewModel(
            IKomitentRepository komitentRepository
            , IMestoTroskaRepository mestoTroskaRepository
            , IMessageDialogService messageDialogService)
        {
            _komitentRepository = komitentRepository;
            _mestoTroskaRepository = mestoTroskaRepository;
            _messageDialogService = messageDialogService;

            _isPravnoLiceEditable = false;
            _isFizickoLiceEditable = false;

            DeleteCommand = new DeleteCommand(this);
            SaveCommand = new SaveKomitentCommand(this);
            CancelCommand = new CancelCommand(this);
            CreateNewKomitentCommand = new CreateNewKomitentCommand(this);

            EditKomitentPropertyCommand = new RelayCommand(EditKomitentProperty);

            MestaTroska = new ObservableCollection<MestoTroska>();
        }

        #endregion

        #region Properties
        public KomitentWrapper? Komitent
        {
            get { return _komitent; }
            set
            {
                var oldValue = _komitent;
                _komitent = value;
                OnModelPropertyChanged(oldValue, value);
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
        public ObservableCollection<MestoTroska> MestaTroska { get; set; }

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
        public bool IsPravnoLiceEditable
        {
            get { return _isPravnoLiceEditable; }
            set
            {
                var oldValue = _isPravnoLiceEditable;
                _isPravnoLiceEditable = value;
                OnModelPropertyChanged(oldValue, value);
            }
        }
        public bool IsFizickoLiceEditable
        {
            get { return _isFizickoLiceEditable; }
            set
            {
                var oldValue = _isFizickoLiceEditable;
                _isFizickoLiceEditable = value;
                OnModelPropertyChanged(oldValue, value);
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
            BeginEdit();
        }

        public KomitentWrapper CreateNewKomitent()
        {
            BeginEdit();

            var komitent = new Komitent();

            _komitentRepository.Add(komitent);

            HasChanges = true;
            Komitent = new KomitentWrapper(komitent);
            return Komitent;
        }

        public async Task LoadAsync(int? komitentId)
        {
            await LoadMestaTroskaAsync();

            if (komitentId == null)
            {
                CreateNewKomitent();
            }
            else
            {
                var komitent = await _komitentRepository.GetByIdAsync(komitentId.Value);

                Komitent = new KomitentWrapper(komitent);
            }

            if (Komitent != null)
            {
                Komitent.PropertyChanged += (s, e) =>
                {
                    if (!HasChanges)
                    {
                        HasChanges = _komitentRepository.HasChanges();
                    }
                };
            }
        }
        public async Task SaveKomitentAsync()
        {
            await _komitentRepository.SaveAsync();
            HasChanges = _komitentRepository.HasChanges();
            IsPravnoLiceEditable = false;
            IsFizickoLiceEditable = false;

            if (Komitent != null)
            {
                if (!Komitent.HasErrors)
                {
                    OnKomitentSaved?.Invoke(this, new KomitentSavedArgs(
                        Komitent.Id,
                        Komitent.PravnoLice == true ? $"{Komitent.Sifra} - {Komitent.Naziv}" : $"{Komitent.Sifra} - {Komitent.Ime} {Komitent.Prezime}"
                        , Komitent.PravnoLice
                        , Komitent.FizickoLice, null));
                }
            }

            EndEdit();
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
        }
        public async Task CancelChange()
        {
            if (HasChanges)
            {
                var result = _messageDialogService.ShowOKCancelDialog("Napravili ste promene? Da li zelite da otkazete?", "Question");
                if (result == MessageDialogResult.Potvrdi)
                {
                    CancelEdit();

                    _komitentRepository.CancelChanges();
                    HasChanges = _komitentRepository.HasChanges();

                    await LoadAsync(Komitent?.Id);
                }
                else
                {
                    return;
                }

                EndEdit();
            }
        }

        public void BeginEdit()
        {
            var model = (KomitentiDetailViewModel)this.MemberwiseClone();
            BackupKomitent = model.Komitent;

            HasChanges = true;

            if (Komitent != null)
            {
                if (Komitent.Id == 0)
                {
                    if (Komitent.PravnoLice == true)
                    {
                        Komitent.Naziv = "";
                        Komitent.Pib = "";
                        Komitent.MaticniBroj = "";

                        Komitent.Ime = null;
                        Komitent.Prezime = null;
                        Komitent.Jmbg = null;

                        IsPravnoLiceEditable = true;
                        IsFizickoLiceEditable = false;
                    }
                    else if (Komitent.FizickoLice == true)
                    {
                        Komitent.Naziv = null;
                        Komitent.Pib = null;
                        Komitent.MaticniBroj = null;

                        Komitent.Ime = "";
                        Komitent.Prezime = "";
                        Komitent.Jmbg = "";

                        IsFizickoLiceEditable = true;
                        IsPravnoLiceEditable = false;
                    }
                }
                else
                {
                    if (Komitent.PravnoLice == true)
                    {
                        IsPravnoLiceEditable = true;
                        IsFizickoLiceEditable = false;
                    }
                    else if (Komitent.FizickoLice == true)
                    {

                        IsFizickoLiceEditable = true;
                        IsPravnoLiceEditable = false;
                    }
                }
            }


        }
        public void CancelEdit()
        {
            Komitent = BackupKomitent;
            IsPravnoLiceEditable = false;
            IsFizickoLiceEditable = false;
        }
        public void EndEdit()
        {
            BackupKomitent = null;
            IsPravnoLiceEditable = false;
            IsFizickoLiceEditable = false;
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

        public async Task LoadMestaTroskaAsync()
        {
            MestaTroska.Clear();
            var mestaTroska = await _komitentRepository.GetAllMestaTroska();
            foreach (var mesto in mestaTroska)
            {
                MestaTroska.Add(mesto);
            }
        }


        #endregion
    }


}
