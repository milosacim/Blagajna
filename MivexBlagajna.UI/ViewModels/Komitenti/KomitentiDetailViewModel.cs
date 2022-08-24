using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Events;
using MivexBlagajna.UI.ViewModels.Komitenti.Interfaces;
using MivexBlagajna.UI.Views.Services;
using MivexBlagajna.UI.Wrappers;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MivexBlagajna.UI.ViewModels.Komitenti
{
    public class KomitentiDetailViewModel : ViewModelBase, IKomitentiDetailViewModel
    {
        #region Fields
        private readonly IKomitentRepository _komitentRepository;
        private readonly IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        private KomitentWrapper _komitent;
        private bool _hasChanges;
        private TextBoxStatus _textBoxStatus;
        private KomitentStatus _komitentStatus;

        #endregion

        #region Konstruktor
        public KomitentiDetailViewModel(IKomitentRepository komitentRepository
            , IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _komitentRepository = komitentRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanEnecute);
            CancelCommand = new DelegateCommand(OnCancelExecute, OnCancelCanExecute);
            CreateNewKomitentCommand = new DelegateCommand(OnCreateNewKomitentExecute);
            DeleteKomitentCommand = new DelegateCommand(DeleteKomitentAsync);
            ChangeTextBoxStatusCommand = new DelegateCommand(ChangeTextBoxStatus);
            ChangeKomitentStatusCommand = new DelegateCommand(ChangeKomitentStatus);
        }

        private void ChangeKomitentStatus()
        {
            KomitentStatus = KomitentStatus == KomitentStatus.Fizicko
                ? KomitentStatus.Pravno
                : KomitentStatus.Fizicko;
        }

        private void ChangeTextBoxStatus()
        {
            TextBoxStatus = TextBoxStatus == TextBoxStatus.Enabled
            ? TextBoxStatus.Disabled
            : TextBoxStatus.Enabled;
        }

        #endregion

        #region Properties
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
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();
                }

            }
        }
        public TextBoxStatus TextBoxStatus
        {
            get { return _textBoxStatus; }
            set { _textBoxStatus = value; OnModelPropertyChanged(); }
        }
        public KomitentStatus KomitentStatus
        {
            get { return _komitentStatus; }
            set { _komitentStatus = value; OnModelPropertyChanged(); }
        }

        // Komande
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand CreateNewKomitentCommand { get; }
        public ICommand CancelNewKomitentCommand { get; }
        public ICommand DeleteKomitentCommand { get; }
        public ICommand ChangeTextBoxStatusCommand { get; }
        public ICommand ChangeKomitentStatusCommand { get; }


        #endregion

        #region Methods

        // Loading
        public async Task LoadAsync(int? komitentId)
        {

            var komitent = komitentId.HasValue
                ? await _komitentRepository.GetByIdAsync(komitentId.Value)
                : await CreateNewKomitent();

            var hasChanges = _komitentRepository.HasChanges();

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
            return komitent;
        }
        private async void OnCreateNewKomitentExecute()
        {
            await LoadAsync(null);
            TextBoxStatus = TextBoxStatus.Disabled;
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
            TextBoxStatus = TextBoxStatus.Enabled;
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
            HasChanges = _komitentRepository.HasChanges();

            if (HasChanges)
            {
                var result = _messageDialogService.ShowOKCancelDialog("Napravili ste promene? Da li zelite da otkazete?", "Question");
                if (result == MessageDialogResult.Potvrdi)
                {
                    _komitentRepository.CancelChanges();
                    await LoadAsync(komitentId);
                    HasChanges = _komitentRepository.HasChanges();
                    TextBoxStatus = TextBoxStatus.Enabled;
                }
                else
                {
                    return;
                }
            }
        }

        #endregion
    }
    public enum TextBoxStatus
    {
        Enabled,
        Disabled,
    }
    public enum KomitentStatus
    {
        Pravno,
        Fizicko,
    }
}
