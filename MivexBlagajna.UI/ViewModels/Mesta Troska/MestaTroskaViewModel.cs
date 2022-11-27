using MivexBlagajna.UI.EventArgs;
using MivexBlagajna.UI.ViewModels.Mesta_Troska.Details;
using MivexBlagajna.UI.ViewModels.Mesta_Troska.Navigation;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MivexBlagajna.UI.ViewModels.MestaTroska
{
    public class MestaTroskaViewModel : ViewModelBase, IDockElement, IMestaTroskaViewModel
    {
        private readonly string? _header;
        private readonly DockState _state;

        public MestaTroskaViewModel(

            IMestaTroskaNavigationViewModel mestaTroskaNavigationViewModel
            , IMestaTroskaDetailsViewModel mestaTroskaDetailsViewModel
            , string header = "Mesta troska"
            , DockState state = DockState.Document

            )
        {
            _header = header;
            _state = state;


            MestaTroskaNavigationViewModel = mestaTroskaNavigationViewModel;
            MestaTroskaDetailsViewModel = mestaTroskaDetailsViewModel;

            MestaTroskaNavigationViewModel.OnMestoSelected += UpdateSelectedMestoTroska;
            MestaTroskaNavigationViewModel.OnMestoSelected += OpenMestoTroskaDetails;

            MestaTroskaDetailsViewModel.OnMestoDeleted += OnMestoDeleted;
            MestaTroskaDetailsViewModel.OnMestoSaved += OnMestoTroskaSaved;
        }

        private async void OpenMestoTroskaDetails(object? sender, MestoTroskaArgs e)
        {
            try
            {
                if (MestaTroskaDetailsViewModel != null && MestaTroskaDetailsViewModel.HasChanges)
                {
                    await MestaTroskaDetailsViewModel.LoadAsync(e.newid);
                }

                await MestaTroskaDetailsViewModel.LoadAsync(e.newid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
        public IMestaTroskaDetailsViewModel MestaTroskaDetailsViewModel { get; }

        private void OnMestoTroskaSaved(object? sender, SavedMestoTroskaArgs e)
        {
            var lookupitem = MestaTroskaNavigationViewModel.MestaTroska.SingleOrDefault(l => l.Id == e.id);

            if (lookupitem == null)
            {
                MestaTroskaNavigationViewModel.MestaTroska.Add(new MestaTroskaNavigationItemViewModel(e.id, e.prefix, e.naziv, e.nadId));
                MestaTroskaNavigationViewModel.SelectedMestoTroska = MestaTroskaNavigationViewModel.MestaTroska.First();
            }
            else
            {
                lookupitem.Sifra = e.prefix; lookupitem.Naziv = e.naziv; lookupitem.Nadredjeni_Id = e.nadId;
            }
        }

        private void OnMestoDeleted(object? sender, MestoTroskaDeletedArgs e)
        {
            var mesto = MestaTroskaNavigationViewModel.MestaTroska.SingleOrDefault(m => m.Id == e.id);

            if (mesto != null)
            {
                MestaTroskaNavigationViewModel.SelectedMestoTroska = MestaTroskaNavigationViewModel.MestaTroska.FirstOrDefault();
                MestaTroskaNavigationViewModel.MestaTroska.Remove(mesto);
            }
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
            MestaTroskaNavigationViewModel.OnMestoSelected -= UpdateSelectedMestoTroska;
            MestaTroskaNavigationViewModel.OnMestoSelected -= OpenMestoTroskaDetails;

            MestaTroskaDetailsViewModel.OnMestoDeleted -= OnMestoDeleted;
            MestaTroskaDetailsViewModel.OnMestoSaved -= OnMestoTroskaSaved;

            base.Dispose();
        }
    }

}