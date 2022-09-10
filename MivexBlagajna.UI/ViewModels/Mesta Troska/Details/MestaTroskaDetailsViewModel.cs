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

        #region Properties
        public MestoTroskaWrapper MestoTroska
        {
            get { return _mestoTroska; }
            set { _mestoTroska = value; OnModelPropertyChanged(); }
        }
        public bool HasChanges
        {
            get { return _hasChanges; }
            set { _hasChanges = value; OnModelPropertyChanged(); ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged(); }
        }
        public bool IsEditable
        {
            get { return _isEditable; }
            set { _isEditable = value; OnModelPropertyChanged(); }
        }

        // Commands
        public ICommand SaveCommand { get; }
        public ICommand EnableEditingCommand { get; }

        #endregion

        #region Methods

        public async Task LoadAsync(int? mestoTroskaId)
        {
            var mestoTroska = await _mestoTroskaRepository.GetByIdAsync(mestoTroskaId.Value);

            MestoTroska = new MestoTroskaWrapper(mestoTroska);

            MestoTroska.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _mestoTroskaRepository.HasChanges();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
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

        #endregion
    }
}
