using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.Commands.Interfaces;
using MivexBlagajna.UI.Commands.Komitenti;
using MivexBlagajna.UI.ViewModels.Komitenti.Interfaces;
using MivexBlagajna.UI.Views.Services;
using MivexBlagajna.UI.Wrappers;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MivexBlagajna.UI.ViewModels.Komitenti.Details
{
    public class KomitentiDetailViewModel : ViewModelBase, IKomitentiDetailViewModel
    {
        #region Fields
        private readonly IKomitentRepository _komitentRepository;
        private readonly IMessageDialogService _messageDialogService;
        private KomitentWrapper _komitent;
        private bool _hasChanges;
        private bool _isPravnoLiceEditable;
        private bool _isFizickoLiceEditable;

        public event EventHandler<KomitentDeletedArgs> OnKomitentDeleted;
        public event EventHandler<KomitentSavedArgs> OnKomitentSaved;

        #endregion

        #region Konstruktor
        public KomitentiDetailViewModel(IKomitentRepository komitentRepository
            , IMessageDialogService messageDialogService)
        {
            _komitentRepository = komitentRepository;
            _messageDialogService = messageDialogService;

            _isPravnoLiceEditable = false;
            _isFizickoLiceEditable = false;

            DeleteCommand = new DeleteCommand(this);
            SaveCommand = new SaveKomitentCommand(this);
            CancelCommand = new CancelCommand(this);
            CreateNewKomitentCommand = new CreateNewKomitentCommand(this);

            EditKomitentPropertyCommand = new RelayCommand(EditKomitentProperty);
        }

        #endregion

        #region Properties

        public IAsyncCommand TestCommand { get; set; }
        public KomitentWrapper Komitent
        {
            get { return _komitent; }
            set
            {
                var oldValue = _komitent;
                _komitent = value;
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
            set { 
                var oldValue = _isFizickoLiceEditable;
                _isFizickoLiceEditable = value;
                OnModelPropertyChanged(oldValue, value);
            }
        }

        // Commands
        public IAsyncCommand SaveCommand { get; }
        public IAsyncCommand CancelCommand { get; }
        public IAsyncCommand CreateNewKomitentCommand { get; }
        public IAsyncCommand DeleteCommand { get; }
        public ICommand EditKomitentPropertyCommand { get; }

        #endregion

        #region Methods

        // Loading
        public async Task LoadAsync(int? komitentId)
        {

            if (komitentId == null)
            {
                CreateNewKomitent();
            }
            else
            {
                var komitent = await _komitentRepository.GetByIdAsync(komitentId.Value);
                Komitent = new KomitentWrapper(komitent);
            }

            Komitent.PropertyChanged += (s, e) =>
                {
                    if (!HasChanges)
                    {
                        HasChanges = _komitentRepository.HasChanges();
                    }
                };
        }
        public KomitentWrapper CreateNewKomitent()
        {
            var komitent = new Komitent();
            _komitentRepository.Add(komitent);
            HasChanges = true;

            Komitent = new KomitentWrapper(komitent);

            return Komitent;
        }
        private void EditKomitentProperty(object? obj = null)
        {
            HasChanges = true;
            IsPravnoLiceEditable = true;
            IsFizickoLiceEditable = true;
            if (Komitent.Id == 0)
            {
                EnableEditPropertyOnCreate();
            }
            else
            {
                EnableEditPropertyOnEdit();
            }
        }
        private void EnableEditPropertyOnCreate()
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
        private void EnableEditPropertyOnEdit()
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
        public async Task SaveKomitentAsync()
        {
            await _komitentRepository.SaveAsync();
            HasChanges = _komitentRepository.HasChanges();

            if (!Komitent.HasErrors)
            {
                OnKomitentSaved?.Invoke(this, new KomitentSavedArgs(
                    Komitent.Id,
                    Komitent.PravnoLice == true ? $"{Komitent.Sifra} - {Komitent.Naziv}" : $"{Komitent.Sifra} - {Komitent.Ime} {Komitent.Prezime}"
                    , Komitent.PravnoLice
                    , Komitent.FizickoLice, null));
            }
        }
        public async Task DeleteKomitentAsync()
        {
            var result = _messageDialogService.ShowOKCancelDialog("Da li ste sigurni da zelite da obrisete komitenta?", "Question");
            if (result == MessageDialogResult.Potvrdi)
            {
                await _komitentRepository.DeleteAsync(Komitent.Model);

                OnKomitentDeleted.Invoke(this, new KomitentDeletedArgs(Komitent.Id));
            }
            else
            {
                return;
            }
        }
        public async Task CancelChange()
        {
            if (HasChanges)
            {
                var result = _messageDialogService.ShowOKCancelDialog("Napravili ste promene? Da li zelite da otkazete?", "Question");
                if (result == MessageDialogResult.Potvrdi)
                {
                    _komitentRepository.CancelChanges();
                    HasChanges = _komitentRepository.HasChanges();

                    IsPravnoLiceEditable = false;
                    IsFizickoLiceEditable = false;
                }
                else
                {
                    return;
                }
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
