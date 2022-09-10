using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services;
using MivexBlagajna.DataAccess.Services.Repositories;
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
        private IKomitentRepository _komitentRepository;
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
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

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanEnecute);
            CancelCommand = new DelegateCommand(OnCancelExecute, OnCancelCanExecute);
            CreateNewKomitentCommand = new DelegateCommand(OnCreateNewKomitentExecute);
            DeleteKomitentCommand = new DelegateCommand(DeleteKomitentAsync);
            EditKomitentPropertyCommand = new DelegateCommand(EditKomitentProperty);
        }


        #endregion

        #region Properties
        public KomitentWrapper Komitent
        {
            get { return _komitent; }
            private set { _komitent = value; OnModelPropertyChanged(); }
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
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();
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
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand CreateNewKomitentCommand { get; }
        public ICommand EditKomitentPropertyCommand { get; }
        public ICommand CancelNewKomitentCommand { get; }
        public ICommand DeleteKomitentCommand { get; }

        #endregion

        #region Methods

        // Loading
        public async Task LoadAsync(int? komitentId)
        {
            var komitent = komitentId.HasValue
                ? await _komitentRepository.GetByIdAsync(komitentId.Value)
                : await CreateNewKomitent();

            Komitent = new KomitentWrapper(komitent);

            Komitent.PropertyChanged += (s, e) =>
              {
                  if (!HasChanges)
                  {
                      HasChanges = _komitentRepository.HasChanges();
                  }

                  if (e.PropertyName == nameof(Komitent.HasErrors))
                  {
                      ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                      ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();
                  }
              };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();
        }

        // Creating
        private async Task<Komitent> CreateNewKomitent()
        {
            var lastKomitentId = await _komitentRepository.GetLastKomitentIdAsync();
            var lastKomitent = await _komitentRepository.GetByIdAsync(lastKomitentId);

            var komitent = new Komitent();
            komitent.Sifra = lastKomitent.Sifra + 1;
            _komitentRepository.Add(komitent);
            HasChanges = true;
            return komitent;
        }
        private async void OnCreateNewKomitentExecute()
        {
            await LoadAsync(null);
            _eventAggregator.GetEvent<OnCreateNewKomitentEvent>().Publish(null);
        }

        // Editing
        private void EditKomitentProperty()
        {
            //HasChanges = true;
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
        private async void OnSaveExecute()
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
        private bool OnSaveCanEnecute()
        {
            return Komitent != null && !Komitent.HasErrors && HasChanges;
        }

        // Deleting
        private async void DeleteKomitentAsync()
        {
            var result = _messageDialogService.ShowOKCancelDialog("Da li ste sigurni da zelite da obrisete komitenta?", "Question");
            if (result == MessageDialogResult.Potvrdi)
            {
                _komitentRepository.Remove(Komitent.Model);
                await _komitentRepository.SaveAsync();
                _eventAggregator.GetEvent<OnKomitentDeletedEvent>().Publish(Komitent.Id);

            }
            else
            {
                return;
            }
        }

        // Canceling
        private bool OnCancelCanExecute()
        {
            return Komitent != null && HasChanges;
        }
        private async void OnCancelExecute()
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
