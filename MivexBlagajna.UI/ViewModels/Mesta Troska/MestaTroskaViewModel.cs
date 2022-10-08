using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Events;
using MivexBlagajna.UI.ViewModels.Mesta_Troska.Details;
using MivexBlagajna.UI.ViewModels.Mesta_Troska.Navigation;
using MivexBlagajna.UI.Views.Services;
using MivexBlagajna.UI.Wrappers;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MivexBlagajna.UI.ViewModels.MestaTroska
{
    public class MestaTroskaViewModel : ViewModelBase, IDockElement
    {
        #region Fields

        private readonly Func<IMestaTroskaDetailsViewModel> _mestaTroskaDetailsViewModelsCreator;
        private readonly IMessageDialogService _messageDialogService;
        private string _header;
        private DockState _state;
        private IMestaTroskaDetailsViewModel _mestaTroskaDetailsViewModel;

        #endregion

        #region Constructor

        public MestaTroskaViewModel(
            IMestaTroskaNavigationViewModel mestaTroskaNavigationViewModel,
            Func<IMestaTroskaDetailsViewModel> mestaTroskaDetailsViewModelsCreator,
            IMessageDialogService messageDialogService,
            string header = "Mesta troska",
            DockState state = DockState.Document)
        {
            _header = header;
            _state = state;
            _messageDialogService = messageDialogService;
            _mestaTroskaDetailsViewModelsCreator = mestaTroskaDetailsViewModelsCreator;

            MestaTroskaNavigationViewModel = mestaTroskaNavigationViewModel;
            MestaTroskaNavigationViewModel.OpenDetails += OpenMestoTroskaDetails;
        }
        #endregion

        #region Properties
        public string? Header
        {
            get { return _header; }
            set { }
        }
        public DockState State
        {
            get { return _state; }
            set { }
        }
        public IMestaTroskaNavigationViewModel MestaTroskaNavigationViewModel { get; }
        public IMestaTroskaDetailsViewModel MestaTroskaDetailsViewModel
        {
            get { return _mestaTroskaDetailsViewModel; }
            set { 
                _mestaTroskaDetailsViewModel = value; 
                OnModelPropertyChanged();
                _mestaTroskaDetailsViewModel.OnMestoSaved += OnMestoTroskaSaved;
                _mestaTroskaDetailsViewModel.OnMestoDeleted += OnMestoDeleted;
            }
        }

        private async Task OnMestoDeleted()
        {
            if (MestaTroskaNavigationViewModel != null)
            {
                await MestaTroskaNavigationViewModel.LoadAsync();
            }
        }

        private void OnMestoTroskaSaved(object sender, SavedMestoTroskaArgs e)
        {
            var lookupitem = MestaTroskaNavigationViewModel.MestaTroska.SingleOrDefault(l => l.Id == e.id);

            if (lookupitem == null)
            {
                MestaTroskaNavigationViewModel.MestaTroska.Add(new MestaTroskaNavigationItemViewModel(e.id, e.prefix, e.naziv, e.nivo, e.nadId));
                MestaTroskaNavigationViewModel.SelectedMestoTroska = MestaTroskaNavigationViewModel.MestaTroska.First();
            } else
            {
                lookupitem.Sifra = e.prefix; lookupitem.Naziv = e.naziv;
            }
        }

        #endregion

        #region Methods

        public async override Task LoadAsync()
        {
            if (MestaTroskaNavigationViewModel != null)
            {
                await MestaTroskaNavigationViewModel.LoadAsync();
            }
        }
        private async Task OpenMestoTroskaDetails(object sender, MestoTroskaArgs e)
        {
            MestaTroskaDetailsViewModel = _mestaTroskaDetailsViewModelsCreator();
            await MestaTroskaDetailsViewModel.LoadAsync(e.id);
        }
        public override void Dispose()
        {
            if (MestaTroskaDetailsViewModel != null && MestaTroskaNavigationViewModel != null)
            {
                if (MestaTroskaDetailsViewModel.HasChanges)
                {
                    var result = _messageDialogService.ShowOKCancelDialog("Napravili ste promene? Da li zelite da otkazete?", "Question");
                    if (result == MessageDialogResult.Potvrdi)
                    {
                        MestaTroskaDetailsViewModel.Dispose();
                        MestaTroskaNavigationViewModel.Dispose();
                    }
                }
            }
            base.Dispose();
        }

        #endregion
    }
}