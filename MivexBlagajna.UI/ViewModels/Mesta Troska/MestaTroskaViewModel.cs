using MivexBlagajna.UI.ViewModels.Mesta_Troska.Details;
using MivexBlagajna.UI.ViewModels.Mesta_Troska.Navigation;
using MivexBlagajna.UI.Views.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.MestaTroska
{
    public class MestaTroskaViewModel : ViewModelBase, IDockElement
    {
        #region Fields

        private readonly Func<IMestaTroskaDetailsViewModel> _mestaTroskaDetailsViewModelsCreator;
        private readonly IMessageDialogService _messageDialogService;
        private readonly string _header;
        private readonly DockState _state;
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

        private async void OpenMestoTroskaDetails(object? sender, MestoTroskaArgs e)
        {
            MestaTroskaDetailsViewModel = _mestaTroskaDetailsViewModelsCreator();
            await MestaTroskaDetailsViewModel.LoadAsync(e.id);
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
                var oldValue = _mestaTroskaDetailsViewModel;
                _mestaTroskaDetailsViewModel = value;
                OnModelPropertyChanged(oldValue, value);
                _mestaTroskaDetailsViewModel.OnMestoSaved += OnMestoTroskaSaved;
                _mestaTroskaDetailsViewModel.OnMestoDeleted += OnMestoDeleted;
            }
        }

        private void OnMestoTroskaSaved(object? sender, SavedMestoTroskaArgs e)
        {
            var lookupitem = MestaTroskaNavigationViewModel.MestaTroska.SingleOrDefault(l => l.Id == e.id);

            if (lookupitem == null)
            {
                MestaTroskaNavigationViewModel.MestaTroska.Add(new MestaTroskaNavigationItemViewModel(e.id, e.prefix, e.naziv, e.nivo, e.nadId));
                MestaTroskaNavigationViewModel.SelectedMestoTroska = MestaTroskaNavigationViewModel.MestaTroska.First();
            }
            else
            {
                lookupitem.Sifra = e.prefix; lookupitem.Naziv = e.naziv;
            }
        }

        private async void OnMestoDeleted(object? sender, EventArgs e)
        {
            if (MestaTroskaNavigationViewModel != null)
            {
                await MestaTroskaNavigationViewModel.LoadAsync();
                MestaTroskaNavigationViewModel.SelectedMestoTroska = MestaTroskaNavigationViewModel.MestaTroska.Last();
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

        public override void Dispose()
        {
            if (MestaTroskaDetailsViewModel != null && MestaTroskaNavigationViewModel != null)
            {
                if (MestaTroskaDetailsViewModel.HasChanges)
                {
                    var result = _messageDialogService.ShowOKCancelDialog("Napravili ste promene? Da li zelite da otkazete?", "Question");
                    if (result == MessageDialogResult.Potvrdi)
                    {
                        MestaTroskaNavigationViewModel.OpenDetails -= OpenMestoTroskaDetails;

                        MestaTroskaDetailsViewModel.OnMestoSaved -= OnMestoTroskaSaved;
                        MestaTroskaDetailsViewModel.OnMestoDeleted -= OnMestoDeleted;

                        MestaTroskaDetailsViewModel.Dispose();
                        MestaTroskaNavigationViewModel.Dispose();
                    }
                }
            }
            MestaTroskaNavigationViewModel.OpenDetails -= OpenMestoTroskaDetails;

            MestaTroskaDetailsViewModel.OnMestoSaved -= OnMestoTroskaSaved;
            MestaTroskaDetailsViewModel.OnMestoDeleted -= OnMestoDeleted;

            MestaTroskaDetailsViewModel.Dispose();
            MestaTroskaNavigationViewModel.Dispose();

            base.Dispose();
        }

        #endregion
    }
}