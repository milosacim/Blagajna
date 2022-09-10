using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Events.Mesta_Troska;
using MivexBlagajna.UI.Views.Services;
using MivexBlagajna.UI.Wrappers;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MivexBlagajna.UI.ViewModels.Mesta_Troska.Details
{
    public class MestaTroskaDetailsViewModel : ViewModelBase, IMestaTroskaDetailsViewModel
    {
        #region Fields
        private readonly IMestoTroskaRepository _mestoTroskaRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;

        private MestoTroskaWrapper _mestoTroska;
        private bool _hasChanges;
        private bool _isEditable;

        #endregion

        #region Constructor
        public MestaTroskaDetailsViewModel(IMestoTroskaRepository mestoTroskaRepository, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)

        {
            _mestoTroskaRepository = mestoTroskaRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;

            _isEditable = false;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            EnableEditingCommand = new DelegateCommand(OnEditExecute);
            CancelCommand = new DelegateCommand(OnCancelExecute, OnSaveCanExecute);
            CreateCommand = new DelegateCommand(OnCreateExecute);
            DeleteCommand = new DelegateCommand(DeleteAsync);
        }

        private async void OnCreateExecute()
        {
            await LoadAsync(null);
        }

        #endregion

        #region Properties
        public MestoTroskaWrapper MestoTroska
        {
            get { return _mestoTroska; }
            set { _mestoTroska = value; OnModelPropertyChanged(); }
        }
        public bool HasChanges
        {
            get { return _hasChanges; }
            set { _hasChanges = value; OnModelPropertyChanged(); ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged(); ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged(); }
        }
        public bool IsEditable
        {
            get { return _isEditable; }
            set { _isEditable = value; OnModelPropertyChanged(); }
        }

        // Commands
        public ICommand SaveCommand { get; }
        public ICommand EnableEditingCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand CreateCommand { get; }
        public ICommand DeleteCommand { get; }


        #endregion

        #region Methods

        // Load method implementation
        public async Task LoadAsync(int? mestoTroskaId)
        {
            var mestoTroska = mestoTroskaId.HasValue
                ? await _mestoTroskaRepository.GetByIdAsync(mestoTroskaId.Value)
                : await CreateNewMestoTroska();

            MestoTroska = new MestoTroskaWrapper(mestoTroska);

            MestoTroska.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _mestoTroskaRepository.HasChanges();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();
        }

        // Creating implementation
        private async Task<MestoTroska> CreateNewMestoTroska()
        {
            var lastId = await _mestoTroskaRepository.GetLastIdAsync();
            var lastMestoTroska = await _mestoTroskaRepository.GetByIdAsync(lastId);

            var mesto = new MestoTroska();
            mesto.Sifra = $"0{lastId + 1}";
            _mestoTroskaRepository.Add(mesto);
            HasChanges = true;
            return mesto;
        }

        // Command methods
        private async void OnSaveExecute()
        {
            await _mestoTroskaRepository.SaveAsync();
            HasChanges = _mestoTroskaRepository.HasChanges();

            _eventAggregator.GetEvent<AfterMestoTroskaSavedEvent>().Publish(
                new AfterMestoTroskaSavedArgs
                {
                    Id = MestoTroska.Id,
                    Sifra = MestoTroska.Sifra,
                    Naziv = MestoTroska.Naziv
                }
                );
        }
        private bool OnSaveCanExecute()
        {
            return MestoTroska != null && HasChanges;
        }
        private async void DeleteAsync()
        {
            var result = _messageDialogService.ShowOKCancelDialog("Da li ste sigurni da zelite da obrisete komitenta?", "Question");
            if (result == MessageDialogResult.Potvrdi)
            {
                _mestoTroskaRepository.Remove(MestoTroska.Model);
                await _mestoTroskaRepository.SaveAsync();
                _eventAggregator.GetEvent<OnMestoTroskaDeletedEvent>().Publish(MestoTroska.Id);

            }
            else
            {
                return;
            }
        }
        private async void OnCancelExecute()
        {
            if (HasChanges)
            {
                var result = _messageDialogService.ShowOKCancelDialog("Napravili ste promene? Da li zelite da otkazete?", "Question");
                if (result == MessageDialogResult.Potvrdi)
                {
                    _mestoTroskaRepository.CancelChanges();
                    HasChanges = _mestoTroskaRepository.HasChanges();
                    if (MestoTroska.Id != 0)
                    {
                        await LoadAsync(MestoTroska.Id);
                    }
                    else
                    {
                        return ;
                    }

                }
                else
                {
                    return;
                }
            }
        }
        private void OnEditExecute()
        {
            if (IsEditable == false)
            {
                IsEditable = true;
            }
            else if (IsEditable == true)
            {
                IsEditable = false;
            }
        }

        #endregion
    }
}
