using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Events;
using MivexBlagajna.UI.Wrappers;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MivexBlagajna.UI.ViewModels
{
    public class KomitentiDetailViewModel : ViewModelBase, IKomitentiDetailViewModel
    {
        #region Fields

        private readonly IKomitentRepository _komitentRepository;
        private readonly IEventAggregator _eventAggregator;
        private KomitentWrapper _komitent;
        private bool _hasChanges;


        #endregion

        #region Konstruktor
        public KomitentiDetailViewModel(IKomitentRepository komitentRepository
            , IEventAggregator eventAggregator)
        {
            _komitentRepository = komitentRepository;
            _eventAggregator = eventAggregator;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanEnecute);
            CancelCommand = new DelegateCommand(OnCancelExecute, OnCancelCanExecute);
        }

        private bool OnCancelCanExecute()
        {
            return Komitent != null && !Komitent.HasErrors && HasChanges;
        }

        private async void OnCancelExecute()
        {
            var komitentId = Komitent.Id;
            await _komitentRepository.CancelChanges(komitentId);
            HasChanges = _komitentRepository.HasChanges();
            _eventAggregator.GetEvent<OnKomitentCancelChangesEvent>().Publish(komitentId);
        }

        #endregion

        #region Properties
        public KomitentWrapper Komitent
        {
            get { return _komitent; }
            set { _komitent = value; OnModelPropertyChanged(); }
        }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        #endregion

        #region Methods
        public async Task LoadAsync(int komitentId)
        {
            var komitent = await _komitentRepository.GetByIdAsync(komitentId);

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

        private async void OnSaveExecute()
        {
            await _komitentRepository.SaveAsync();
            HasChanges = _komitentRepository.HasChanges();
            _eventAggregator.GetEvent<AfterKomitentSavedEvent>().Publish(
                new AfterKomitentSavedEventArgs
                {
                    Id = Komitent.Id,
                    PunNaziv = Komitent.PravnoLice == true ? $"{Komitent.Sifra} - {Komitent.Naziv}" : $"{Komitent.Sifra} - {Komitent.Ime} {Komitent.Prezime}"
                });
        }
        private bool OnSaveCanEnecute()
        {
            return Komitent != null && !Komitent.HasErrors && HasChanges;
        }
        #endregion
    }
}
