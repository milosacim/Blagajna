using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.Commands.Mesta_Troska;
using MivexBlagajna.UI.Views.Services;
using MivexBlagajna.UI.Wrappers;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MivexBlagajna.UI.ViewModels.Mesta_Troska.Details
{
    public class MestaTroskaDetailsViewModel : ViewModelBase, IMestaTroskaDetailsViewModel
    {
        #region Fields
        private readonly IMestoTroskaRepository _mestoTroskaRepository;
        private readonly IMessageDialogService _messageDialogService;
        private MestoTroskaWrapper _mestoTroska;
        private MestoTroskaWrapper _nadredjenoMestoTroska;
        private bool _hasChanges;
        private bool _isEditable;

        #endregion

        #region Constructor
        public MestaTroskaDetailsViewModel(
            IMestoTroskaRepository mestoTroskaRepository,
            IMessageDialogService messageDialogService
            )

        {
            _mestoTroskaRepository = mestoTroskaRepository;
            _messageDialogService = messageDialogService;
            _isEditable = false;


            EnableEditingCommand = new RelayCommand(OnEditExecute);

            SaveCommand = new SaveMestoTroskaCommand(this);
            CreateCommand = new CreateNewMestoCommand(this);
            CancelCommand = new CancelCommand(this);
            DeleteCommand = new DeleteCommand(this);

        }

        #endregion

        #region Properties

        public MestoTroskaWrapper NadredjenoMestoTroska
        {
            get { return _nadredjenoMestoTroska; }
            set { 
                var oldValue = _nadredjenoMestoTroska;
                _nadredjenoMestoTroska = value; 
                OnModelPropertyChanged(oldValue, value); 
            }
        }
        public MestoTroskaWrapper MestoTroska
        {
            get { return _mestoTroska; }
            set {
                var oldValue = _mestoTroska;
                _mestoTroska = value;
                OnModelPropertyChanged(oldValue, value);
            }
        }
        public bool HasChanges
        {
            get { return _hasChanges; }
            set {
                var oldValue = _hasChanges;
                _hasChanges = value;
                OnModelPropertyChanged(oldValue, value);
            }
        }
        public bool IsEditable
        {
            get { return _isEditable; }
            set
            {
                var oldValue = _isEditable;
                _isEditable = value;
                OnModelPropertyChanged(oldValue, value);
            }
        }

        // Commands
        public ICommand SaveCommand { get; }
        public AsyncCommand CancelCommand { get; }
        public AsyncCommand CreateCommand { get; }
        public AsyncCommand DeleteCommand { get; }

        public ICommand EnableEditingCommand { get; }


        #endregion

        #region Methods

        // Load method implementation
        public async Task LoadAsync(int? mestoTroskaId)
        {

            if (mestoTroskaId == null)
            {
                await CreateNewMestoTroska();
            }
            else
            {
                var mesto = await _mestoTroskaRepository.GetByIdAsync(mestoTroskaId.Value);
                MestoTroska = new MestoTroskaWrapper(mesto);
            }


            MestoTroska.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _mestoTroskaRepository.HasChanges();
                }
            };
        }

        public async Task LoadNadredjenoMestoTroskaAsync(int id)
        {
            var mesto = id != 0
                ? await _mestoTroskaRepository.GetByIdAsync(id)
                : await _mestoTroskaRepository.GetByIdAsync(id + 1);

            NadredjenoMestoTroska = new MestoTroskaWrapper(mesto);

            NadredjenoMestoTroska.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _mestoTroskaRepository.HasChanges();
                }
            };

        }

        // Creating implementation
        private async Task<MestoTroskaWrapper> CreateNewMestoTroska()
        {
            var lastId = await _mestoTroskaRepository.GetLastIdAsync();

            var mesto = new MestoTroska
            {
                Prefix = $"0{lastId + 1}",
                Naziv = ""
            };

            _mestoTroskaRepository.Add(mesto);
            MestoTroska = new MestoTroskaWrapper(mesto);

            HasChanges = true;

            return MestoTroska;
        }

        // Command methods

        // Saving

        public async Task SaveMestoAsync()
        {
            await _mestoTroskaRepository.SaveAsync();
            HasChanges = _mestoTroskaRepository.HasChanges();
            IsEditable = false;
            OnMestoSaved?.Invoke(this, new SavedMestoTroskaArgs(MestoTroska.Id, MestoTroska.Prefix, MestoTroska.Naziv, MestoTroska.Nivo, MestoTroska.Nadiredjeni_Id));
        }

        // Deleting
        public async Task DeleteAsync()
        {
            var result = _messageDialogService.ShowOKCancelDialog("Da li ste sigurni da zelite da obrisete mesto troška?", "Question");
            if (result == MessageDialogResult.Potvrdi)
            {
                await _mestoTroskaRepository.RemoveAsync(MestoTroska.Model);
                OnMestoDeleted.Invoke(this, EventArgs.Empty);
            }
            else
            {
                return;
            }
        }

        //Canceling
        public async Task CancelChange()
        {
            var mestoId = await _mestoTroskaRepository.GetLastIdAsync();
            if (HasChanges)
            {
                var result = _messageDialogService.ShowOKCancelDialog("Napravili ste promene? Da li zelite da otkazete?", "Question");
                if (result == MessageDialogResult.Potvrdi)
                {
                    _mestoTroskaRepository.CancelChanges();
                    await LoadAsync(mestoId);
                    HasChanges = _mestoTroskaRepository.HasChanges();
                    IsEditable = false;

                }
                else
                {
                    return;
                }
            }
            else
            {
                IsEditable = false;
            }
        }

        //Editing
        private void OnEditExecute(object? obj)
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

        //Dispose method
        public override void Dispose()
        {
            if (HasChanges)
            {
                _mestoTroskaRepository.CancelChanges();
                HasChanges = _mestoTroskaRepository.HasChanges();
            }

            base.Dispose();
        }

        #endregion


        #region Events and delegates

        public event EventHandler<SavedMestoTroskaArgs> OnMestoSaved;
        public event EventHandler OnMestoDeleted;

        #endregion
    }
}
