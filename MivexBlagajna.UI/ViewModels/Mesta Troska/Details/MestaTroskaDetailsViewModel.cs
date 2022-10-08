using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services.Lookups;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.Commands.Mesta_Troska;
using MivexBlagajna.UI.Events.Mesta_Troska;
using MivexBlagajna.UI.Views.Services;
using MivexBlagajna.UI.Wrappers;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
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
            //_eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _isEditable = false;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            EnableEditingCommand = new DelegateCommand(OnEditExecute);
            CancelCommand = new DelegateCommand(OnCancelExecute, OnCancelCanExecute);
            CreateCommand = new DelegateCommand(OnCreateExecute);
            DeleteCommand = new DeleteCommand(this);

        }

        #endregion

        #region Properties

        public MestoTroskaWrapper NadredjenoMestoTroska
        {
            get { return _nadredjenoMestoTroska; }
            set { _nadredjenoMestoTroska = value; OnModelPropertyChanged(); }
        }
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
            set
            {
                _isEditable = value;
                OnModelPropertyChanged();
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();
            }
        }

        // Commands
        public ICommand SaveCommand { get; }
        public ICommand EnableEditingCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand CreateCommand { get; }
        public AsyncCommand DeleteCommand { get; }

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

                if (e.PropertyName == nameof(MestoTroska.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();

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

                if (e.PropertyName == nameof(MestoTroska.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();
        }

        // Creating implementation
        private async Task<MestoTroska> CreateNewMestoTroska()
        {
            var lastId = await _mestoTroskaRepository.GetLastIdAsync();

            var mesto = new MestoTroska();

            mesto.Prefix = $"0{lastId + 1}";
            mesto.Naziv = "";

            _mestoTroskaRepository.Add(mesto);
            HasChanges = true;
            return mesto;
        }

        // Command methods

        // Saving
        private async void OnSaveExecute()
        {
            await _mestoTroskaRepository.SaveAsync();
            HasChanges = _mestoTroskaRepository.HasChanges();
            OnMestoSaved?.Invoke(this, new SavedMestoTroskaArgs(MestoTroska.Id, MestoTroska.Prefix, MestoTroska.Naziv, MestoTroska.Nivo, MestoTroska.Nadiredjeni_Id));
            IsEditable = false;
        }
        private bool OnSaveCanExecute()
        {
            return MestoTroska != null && HasChanges && !MestoTroska.HasErrors;
        }

        // Deleting
        public async Task DeleteAsync()
        {
            var result = _messageDialogService.ShowOKCancelDialog("Da li ste sigurni da zelite da obrisete mesto troška?", "Question");
            if (result == MessageDialogResult.Potvrdi)
            {
                _mestoTroskaRepository.Remove(MestoTroska.Model);
                await _mestoTroskaRepository.SaveAsync();
                OnMestoDeleted?.Invoke();
            }
            else
            {
                return;
            }
        }

        //Canceling
        private bool OnCancelCanExecute()
        {
            return IsEditable;
        }
        private async void OnCancelExecute()
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

        // Creating
        private async void OnCreateExecute()
        {
            IsEditable = true;
            await LoadAsync(null);
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

        public delegate void SaveMestoTroskaHandler(object sender, SavedMestoTroskaArgs e);
        public delegate Task DeleteMestoTroskaHandler();

        public event SaveMestoTroskaHandler OnMestoSaved;
        public event DeleteMestoTroskaHandler OnMestoDeleted;

        #endregion
    }

    public class DeletedMestoTroskaArgs
    {
        public readonly int id;
        public DeletedMestoTroskaArgs(int id)
        {
            this.id = id;
        }
    }

    public class SavedMestoTroskaArgs
    {
        public readonly int id;
        public readonly string prefix;
        public readonly string naziv;
        public readonly int nivo;
        public readonly int nadId;

        public SavedMestoTroskaArgs(int id, string prefix, string naziv, int nivo, int nadId)
        {
            this.id = id;
            this.prefix = prefix;
            this.naziv = naziv;
            this.nivo = nivo;
            this.nadId = nadId;
        }
    }
}
