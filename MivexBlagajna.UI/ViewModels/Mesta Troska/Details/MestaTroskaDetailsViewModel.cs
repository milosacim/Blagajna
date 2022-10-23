using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.Commands.Interfaces;
using MivexBlagajna.UI.Commands.Mesta_Troska;
using MivexBlagajna.UI.Views.Services;
using MivexBlagajna.UI.Wrappers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MivexBlagajna.UI.ViewModels.Mesta_Troska.Details
{
    public class MestaTroskaDetailsViewModel : ViewModelBase, IMestaTroskaDetailsViewModel
    {
        private readonly IMestoTroskaRepository _mestoTroskaRepository;
        private readonly IMessageDialogService _messageDialogService;


        private MestoTroskaWrapper _mestoTroska;
        private MestoTroskaWrapper _backupMestoTroska;
        private MestoTroska _nadredjenoMestoTroska;
        private bool _hasChanges;

        public event EventHandler<SavedMestoTroskaArgs> OnMestoSaved;
        public event EventHandler<MestoTroskaDeletedArgs> OnMestoDeleted;

        public MestaTroskaDetailsViewModel(
            IMestoTroskaRepository mestoTroskaRepository
            , IMessageDialogService messageDialogService)
        {
            _mestoTroskaRepository = mestoTroskaRepository;
            _messageDialogService = messageDialogService;

            SaveCommand = new SaveMestoTroskaCommand(this);
            CreateCommand = new CreateNewMestoCommand(this);
            CancelCommand = new CancelCommand(this);
            DeleteCommand = new DeleteCommand(this);

            EditMestoTroskaPropertyCommand = new RelayCommand(EditMestoTroskaProperty);

            MestaTroska = new ObservableCollection<MestoTroska>();
        }

        public ObservableCollection<MestoTroska> MestaTroska { get; }

        public MestoTroskaWrapper? MestoTroska
        {
            get { return _mestoTroska; }
            set
            {
                var oldValue = _mestoTroska;
                _mestoTroska = value;
                OnModelPropertyChanged(oldValue, value);
            }
        }
        public MestoTroskaWrapper? BackupMestoTroska
        {
            get { return _backupMestoTroska; }
            set
            {
                var oldValue = _backupMestoTroska;
                _backupMestoTroska = value;
                OnModelPropertyChanged(oldValue, value);
            }
        }


        public MestoTroska NadredjenoMestoTroska
        {
            get { return _nadredjenoMestoTroska; }
            set
            {
                if (value != null)
                {
                    var oldValue = _nadredjenoMestoTroska;
                    _nadredjenoMestoTroska = value;
                    OnModelPropertyChanged(oldValue, value); 

                    if (oldValue != null && oldValue.Id != value.Id)
                    {
                        MestoTroska.NadredjenoMesto_Id = value.Id;
                    }
                }
            }
        }

        public IAsyncCommand SaveCommand { get; }
        public IAsyncCommand CreateCommand { get; }
        public IAsyncCommand CancelCommand { get; }
        public IAsyncCommand DeleteCommand { get; }

        public RelayCommand EditMestoTroskaPropertyCommand { get; }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                var oldValue = _hasChanges;
                _hasChanges = value;
                OnModelPropertyChanged(oldValue, value);
            }
        }

        

        public async Task CancelChange()
        {
            var result = _messageDialogService.ShowOKCancelDialog("Napravili ste promene. Da li želite da otkažete?", "Question");

            if (result == MessageDialogResult.Potvrdi)
            {
                _mestoTroskaRepository.CancelChanges();
                HasChanges = _mestoTroskaRepository.HasChanges();
            }

            if (MestoTroska != null)
            {
                MestoTroska?.EndEdit();
                await LoadAsync(BackupMestoTroska?.Id, BackupMestoTroska?.NadredjenoMesto_Id);
            }
        }
        public async Task DeleteAsync()
        {
            if (MestoTroska != null)
            {
                var result = _messageDialogService.ShowOKCancelDialog("Da li ste sigurni da zelite da obrisete mesto troška?", "Question");

                if (result == MessageDialogResult.Potvrdi)
                {
                    await _mestoTroskaRepository.RemoveAsync(MestoTroska.Model);
                    OnMestoDeleted?.Invoke(this, new MestoTroskaDeletedArgs(MestoTroska.Id, MestoTroska.NadredjenoMesto_Id));
                }
                else
                {
                    return;
                }
            }

            if (MestoTroska.IsEditable == true)
            {
                MestoTroska?.EndEdit();
            }
        }
        public async Task LoadAllMestaTroska()
        {
            MestaTroska.Clear();
            var mestaTroska = await _mestoTroskaRepository.GetAll();

            foreach (var mesto in mestaTroska)
            {
                MestaTroska.Add(mesto);
            }
        }
        public async Task LoadAsync(int? mestoTroskaId, int? nadMestoTroskaId)
        {
            await LoadAllMestaTroska();

            await InitializeMestoTroska(mestoTroskaId, nadMestoTroskaId);

        }
        public async Task SaveMestoAsync()
        {
            if (MestoTroska != null)
            {
                if (!MestoTroska.HasErrors)
                {
                    OnMestoSaved?.Invoke(this, new SavedMestoTroskaArgs(
                        MestoTroska.Id
                        , MestoTroska.Prefix
                        , MestoTroska.Naziv
                        , MestoTroska.Nivo
                        , MestoTroska.NadredjenoMesto_Id
                        )
                    );
                }
            }

            MestoTroska?.EndEdit();
            await _mestoTroskaRepository.SaveAsync();
            HasChanges = _mestoTroskaRepository.HasChanges();
            
        }

        private void EditMestoTroskaProperty(object? obj)
        {
            var model = (MestaTroskaDetailsViewModel)this.MemberwiseClone();
            BackupMestoTroska = model.MestoTroska;
            MestoTroska?.BeginEdit();
        }
        private MestoTroskaWrapper CreateNewMestoTroska()
        {
            var model = (MestaTroskaDetailsViewModel)this.MemberwiseClone();
            BackupMestoTroska = model.MestoTroska;

            var mesto = new MestoTroska();
            _mestoTroskaRepository.Add(mesto);

            MestoTroska = new MestoTroskaWrapper(mesto, true);
            return MestoTroska;
        }

        private async Task InitializeMestoTroska(int? mestoTroskaId, int? nadMestoTroskaId)
        {
            if (mestoTroskaId == null)
            {
                CreateNewMestoTroska();
            }
            else
            {
                var mestoTroska = await _mestoTroskaRepository.GetByIdAsync(mestoTroskaId.Value);

                MestoTroska = new MestoTroskaWrapper(mestoTroska, false);

                NadredjenoMestoTroska = MestaTroska.Single(m => m.Id == nadMestoTroskaId);

                MestoTroska.PropertyChanged += (s, e) =>
                {
                    HasChanges = _mestoTroskaRepository.HasChanges();
                };
            }
        }


    }

}
