using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.Commands.Interfaces;
using MivexBlagajna.UI.Commands.Komitenti;
using MivexBlagajna.UI.Events.Komitenti;
using MivexBlagajna.UI.ViewModels.Komitenti.Interfaces;
using MivexBlagajna.UI.Views.Services;
using MivexBlagajna.UI.Wrappers;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MivexBlagajna.UI.ViewModels.Komitenti.Details
{
    public class KomitentiDetailViewModel : ViewModelBase, IKomitentiDetailViewModel
    {
        #region Fields
        private readonly IKomitentRepository _komitentRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private KomitentWrapper _komitent;
        private bool _hasChanges;
        private bool _isPravnoLiceEditable;
        private bool _isFizickoLiceEditable;

        #endregion

        #region Konstruktor
        public KomitentiDetailViewModel(IKomitentRepository komitentRepository
            , IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _komitentRepository = komitentRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;

            _isPravnoLiceEditable = false;
            _isFizickoLiceEditable = false;
            
            DeleteCommand = new DeleteCommand(this);
            SaveCommand = new SaveKomitentCommand(this);
            CancelCommand = new CancelCommand(this);

            CreateNewKomitentCommand = new DelegateCommand(OnCreateNewKomitentExecute);
            EditKomitentPropertyCommand = new DelegateCommand(EditKomitentProperty);
        }

        #endregion

        #region Properties

        public IAsyncCommand TestCommand { get; set; }
        public KomitentWrapper Komitent
        {
            get { return _komitent; }
            set { _komitent = value; OnModelPropertyChanged(); }
        }
        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnModelPropertyChanged();
                }

            }
        }

        public bool IsPravnoLiceEditable
        {
            get { return _isPravnoLiceEditable; }
            set { _isPravnoLiceEditable = value; OnModelPropertyChanged(); }
        }
        public bool IsFizickoLiceEditable
        {
            get { return _isFizickoLiceEditable; }
            set { _isFizickoLiceEditable = value; OnModelPropertyChanged(); }
        }

        // Commands
        public IAsyncCommand SaveCommand { get; }
        public IAsyncCommand CancelCommand { get; }
        public ICommand CreateNewKomitentCommand { get; }
        public ICommand EditKomitentPropertyCommand { get; }
        public IAsyncCommand DeleteCommand { get; }

        #endregion

        #region Methods

        // Loading
        public async Task LoadAsync(int? komitentId)
        {
            var komitent = komitentId.HasValue
                ? await _komitentRepository.GetByIdAsync(komitentId.Value)
                : CreateNewKomitent();

            Komitent = new KomitentWrapper(komitent);

            Komitent.PropertyChanged += (s, e) =>
              {
                  if (!HasChanges)
                  {
                      HasChanges = _komitentRepository.HasChanges();
                  }
              };
        }

        // Creating
        private Komitent CreateNewKomitent()
        {
            var komitent = new Komitent();
            _komitentRepository.Add(komitent);
            HasChanges = true;
            return komitent;
        }
        private async void OnCreateNewKomitentExecute()
        {
            await LoadAsync(null);
        }

        // Editing
        private void EditKomitentProperty()
        {
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

        // Saving
        public async Task SaveKomitentAsync()
        {
            await _komitentRepository.SaveAsync();
            HasChanges = _komitentRepository.HasChanges();
            if (!Komitent.HasErrors)
            {
                _eventAggregator.GetEvent<AfterKomitentSavedEvent>().Publish(
                new AfterKomitentSavedEventArgs
                {
                    Id = Komitent.Id,
                    PunNaziv = Komitent.PravnoLice == true ? $"{Komitent.Sifra} - {Komitent.Naziv}" : $"{Komitent.Sifra} - {Komitent.Ime} {Komitent.Prezime}",
                    PravnoLice = Komitent.PravnoLice,
                    FizickoLice = Komitent.FizickoLice,

                });
            }
        }

        // Deleting
        public async Task DeleteKomitentAsync()
        {
            var result = _messageDialogService.ShowOKCancelDialog("Da li ste sigurni da zelite da obrisete komitenta?", "Question");
            if (result == MessageDialogResult.Potvrdi)
            {
                await _komitentRepository.DeleteAsync(Komitent.Model);
                _eventAggregator.GetEvent<OnKomitentDeletedEvent>().Publish(Komitent.Id);
            }
            else
            {
                return;
            }
        }

        // Canceling
        public async Task CancelChange()
        {
            var komitentId = await _komitentRepository.GetLastKomitentIdAsync();
            if (HasChanges)
            {
                var result = _messageDialogService.ShowOKCancelDialog("Napravili ste promene? Da li zelite da otkazete?", "Question");
                if (result == MessageDialogResult.Potvrdi)
                {
                    _komitentRepository.CancelChanges();
                    await LoadAsync(komitentId);
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

        //Disposing

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
