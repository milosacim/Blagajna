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
        private readonly IMestaTroskaNavigationViewModel _mestaTroskaNavigationViewModel;
        private readonly Func<IMestaTroskaDetailsViewModel> _mestaTroskaDetailsViewModelCreator;
        private readonly IMessageDialogService _messageDialogService;

        private readonly string? _header;
        private readonly DockState _state;

        private IMestaTroskaDetailsViewModel _mestaTroskaDetailsViewModel;


        public MestaTroskaViewModel(

            IMestaTroskaNavigationViewModel mestaTroskaNavigationViewModel
            , Func<IMestaTroskaDetailsViewModel> mestaTroskaDetailsViewModelCreator
            , IMessageDialogService messageDialogService

            , string header = "Mesta troska",
            , DockState state = DockState.Document

            )
        {
            _mestaTroskaNavigationViewModel = mestaTroskaNavigationViewModel;
            _mestaTroskaDetailsViewModelCreator = mestaTroskaDetailsViewModelCreator;
            _messageDialogService = messageDialogService;
            _header = header;
            _state = state;

            MestaTroskaNavigationViewModel = mestaTroskaNavigationViewModel;

            MestaTroskaNavigationViewModel.OnMestoSelected += UpdateSelectedMestoTroska;
            MestaTroskaNavigationViewModel.OnMestoSelected += OpenMestoTroskaDetails;
        }

        private void OpenMestoTroskaDetails(object? sender, MestoTroskaArgs e)
        {

        }

        private void UpdateSelectedMestoTroska(object? sender, MestoTroskaArgs e)
        {
            var model = sender as MestaTroskaNavigationViewModel;
            UpdateIsSelected(e, model);
        }

        private static void UpdateIsSelected(MestoTroskaArgs e, MestaTroskaNavigationViewModel? model)
        {
            if (model != null)
            {
                if (model.SelectedMestoTroska != null)
                {
                    if (e._oldId != null)
                    {
                        var oldItem = model.MestaTroska.Where(m => m.Id == e._oldId && m.IsSelected == true).FirstOrDefault();
                        if (oldItem != null)
                        {
                            oldItem.IsSelected = false;
                            model.SelectedMestoTroska.IsSelected = true;
                        }
                    }
                }
                else
                {
                    model.SelectedMestoTroska.IsSelected = true;
                }
            }
        }

        public string? Header
        {
            get { return _header; }
        }
        public DockState State
        {
            get { return _state; }
        }

        public IMestaTroskaNavigationViewModel MestaTroskaNavigationViewModel { get; }

        public IMestaTroskaDetailsViewModel MestaTroskaDetailsViewModel
        {
            get { return _mestaTroskaDetailsViewModel; }
            set
            {
                var oldValue = _mestaTroskaDetailsViewModel;
                _mestaTroskaDetailsViewModel = value;
                OnModelPropertyChanged(oldValue, value);

                _mestaTroskaDetailsViewModel.OnMestoDeleted += OnMestoDeleted;
                _mestaTroskaDetailsViewModel.OnMestoSaved += OnMestoTroskaSaved;
            }
        }

        private void OnMestoTroskaSaved(object? sender, SavedMestoTroskaArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnMestoDeleted(object? sender, MestoTroskaDeletedArgs e)
        {
            throw new NotImplementedException();
        }

        public override async Task LoadAsync()
        {
            if (MestaTroskaNavigationViewModel != null)
            {
                await MestaTroskaNavigationViewModel.LoadAsync();
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }

}