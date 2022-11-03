using Castle.Core.Internal;
using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.Commands.Interfaces;
using MivexBlagajna.UI.Commands.Mesta_Troska;
using MivexBlagajna.UI.EventArgs;
using MivexBlagajna.UI.Views.Services;
using MivexBlagajna.UI.Wrappers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Mesta_Troska.Details
{
    public class MestaTroskaDetailsViewModel : ViewModelBase, IMestaTroskaDetailsViewModel
    {
        private readonly IMestoTroskaRepository _mestoTroskaRepository;
        private readonly IMessageDialogService _messageDialogService;

        private MestoTroskaWrapper _mestoTroska;
        private MestoTroskaWrapper _backupMestoTroska;
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
            CreatePrefixCommand = new RelayCommand(CreatePrefix);

            MestaTroska = new ObservableCollection<MestoTroska>();
        }

        public ObservableCollection<MestoTroska> MestaTroska { get; }

        public MestoTroskaWrapper MestoTroska
        {
            get { return _mestoTroska; }
            set
            {
                var oldValue = _mestoTroska;
                _mestoTroska = value;

                OnModelPropertyChanged(oldValue, value);

                MestoTroska.PropertyChanged += (s, e) =>
                {
                    if (!HasChanges)
                    {
                        HasChanges = _mestoTroskaRepository.HasChanges();
                    }
                };
            }
        }

        public RelayCommand CreatePrefixCommand
        {
            get;
        }

        public void SetPrefix(object? obj)
        {
            CreatePrefix(obj);
        }

        public void CreatePrefix(object? obj)
        {
            if (obj == null)
            {
                MestoTroska.Prefix = "0" + (MestaTroska.Select(m => m.NadredjenoMesto_Id == null)).Count().ToString() + ".";
            }
            else
            {
                if (HasChanges)
                {
                    int mesto_Id = (int)obj;

                    MestoTroska mesto = MestaTroska.Single(m => m.Id == mesto_Id);

                    if (mesto.DecaMestoTroska != null)
                    {
                        MestoTroska.Prefix = mesto.Prefix + "0" + (mesto.DecaMestoTroska.Count() + 1).ToString() + ".";
                    }
                }
            }
        }

        public MestoTroskaWrapper? BackupMestoTroska
        {
            get { return _backupMestoTroska; }
            set
            {
                _backupMestoTroska = value;
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
                await LoadAsync(BackupMestoTroska?.Id);
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
            if (MestaTroska.IsNullOrEmpty())
            {
                var mestaTroska = await _mestoTroskaRepository.GetAll();

                foreach (var mesto in mestaTroska)
                {
                    MestaTroska.Add(mesto);
                }
            }
        }
        public async Task LoadAsync(int? mestoTroskaId)
        {
            if (mestoTroskaId != null)
            {
                await LoadAllMestaTroska();
            }
            await InitializeMestoTroska(mestoTroskaId);

        }

        public async Task SaveMestoAsync()
        {
            await _mestoTroskaRepository.SaveAsync();

            if (MestoTroska != null)
            {
                if (!MestoTroska.HasErrors)
                {
                    OnMestoSaved?.Invoke(this, new SavedMestoTroskaArgs(
                        MestoTroska.Id
                        , MestoTroska.Prefix
                        , MestoTroska.Naziv
                        , MestoTroska.NadredjenoMesto_Id
                        )
                    );
                }
            }

            HasChanges = _mestoTroskaRepository.HasChanges();
            MestoTroska?.EndEdit();
        }

        private void EditMestoTroskaProperty(object? obj)
        {
            BackupMestoTroska = new MestoTroskaWrapper(MestoTroska.Model);
            MestoTroska?.BeginEdit();
        }
        private MestoTroskaWrapper CreateNewMestoTroska()
        {
            BackupMestoTroska = new MestoTroskaWrapper(MestoTroska.Model);
            var mesto = new MestoTroska();
            _mestoTroskaRepository.Add(mesto);

            MestoTroska = new MestoTroskaWrapper(mesto, true);

            MestoTroska.Naziv = "";

            return MestoTroska;
        }

        private async Task InitializeMestoTroska(int? mestoTroskaId)
        {
            if (mestoTroskaId == null)
            {
                CreateNewMestoTroska();
            }
            else
            {
                var mestoTroska = await _mestoTroskaRepository.GetByIdAsync(mestoTroskaId.Value);
                MestoTroska = new MestoTroskaWrapper(mestoTroska, false);
            }
        }
    }
}
