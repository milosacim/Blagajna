using MivexBlagajna.UI.ViewModels.Mesta_Troska.Details;
using MivexBlagajna.UI.ViewModels.Mesta_Troska.Navigation;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.MestaTroska
{
    public class MestaTroskaViewModel : ViewModelBase, IDockElement
    {
        private readonly Func<IMestaTroskaDetailsViewModel> _mestaTroskaDetailsViewModelCreator;

        private readonly string? _header;
        private readonly DockState _state;

        private IMestaTroskaDetailsViewModel _mestaTroskaDetailsViewModel;


        public MestaTroskaViewModel(

            IMestaTroskaNavigationViewModel mestaTroskaNavigationViewModel
            , Func<IMestaTroskaDetailsViewModel> mestaTroskaDetailsViewModelCreator
            , string header = "Mesta troska"
            , DockState state = DockState.Document

            )
        {
            _header = header;
            _state = state;

            _mestaTroskaDetailsViewModelCreator = mestaTroskaDetailsViewModelCreator;

            MestaTroskaNavigationViewModel = mestaTroskaNavigationViewModel;

            MestaTroskaNavigationViewModel.OnMestoSelected += UpdateSelectedMestoTroska;
            MestaTroskaNavigationViewModel.OnMestoSelected += OpenMestoTroskaDetails;
        }

        private async void OpenMestoTroskaDetails(object? sender, MestoTroskaArgs e)
        {
            if (MestaTroskaDetailsViewModel != null && MestaTroskaDetailsViewModel.HasChanges)
            {
                MestaTroskaDetailsViewModel = _mestaTroskaDetailsViewModelCreator();

                await MestaTroskaDetailsViewModel.CancelChange();
                await MestaTroskaDetailsViewModel.LoadAsync(e.newid, e.newNadId);
            }

            MestaTroskaDetailsViewModel = _mestaTroskaDetailsViewModelCreator();
            await MestaTroskaDetailsViewModel.LoadAsync(e.newid, e.newNadId);
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
                    if (e.oldId != null)
                    {
                        var oldItem = model.MestaTroska.Where(m => m.Id == e.oldId && m.IsSelected == true).FirstOrDefault();
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
            var lookupitem = MestaTroskaNavigationViewModel.MestaTroska.SingleOrDefault(l => l.Id == e.id);

            if (lookupitem == null)
            {
                MestaTroskaNavigationViewModel.MestaTroska.Add(new MestaTroskaNavigationItemViewModel(e.id, e.prefix, e.naziv, e.nivo, e.nadId));
                MestaTroskaNavigationViewModel.SelectedMestoTroska = MestaTroskaNavigationViewModel.MestaTroska.Last();
            }
            else
            {
                lookupitem.Sifra = e.prefix; lookupitem.Naziv = e.naziv; lookupitem.Nadredjeni_Id = e.nadId;
            }

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