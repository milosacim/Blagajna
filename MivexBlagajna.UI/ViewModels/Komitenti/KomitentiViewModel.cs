using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.ViewModels.Komitenti.Details;
using MivexBlagajna.UI.ViewModels.Komitenti.Interfaces;
using MivexBlagajna.UI.ViewModels.Komitenti.Navigation;
using MivexBlagajna.UI.Views.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Komitenti
{
    public class KomitentiViewModel : ViewModelBase, IDockElement, IKomitentiViewModel
    {
        #region Fields

        private readonly IMessageDialogService _messageDialogService;
        //private IKomitentiDetailViewModel? _komitentiDetailViewModel;
        //private readonly Func<IKomitentiDetailViewModel> _komitentiDetailViewModelCreator;
        private readonly string _header;
        private readonly DockState _state;

        #endregion

        #region Constructor
        public KomitentiViewModel(

            IKomitentiNavigationViewModel komitentiNavigationViewModel,
            IKomitentiDetailViewModel komitentiDetailViewModel,
            IMessageDialogService messageDialogService,
            string header = "Komitenti",
            DockState state = DockState.Document)
        {
            _messageDialogService = messageDialogService;
            _header = header;
            _state = state;

            KomitentiNavigationViewModel = komitentiNavigationViewModel;
            KomitentiDetailViewModel = komitentiDetailViewModel;

            KomitentiNavigationViewModel.OnkomitentSelected += OnOpenDetails;
            KomitentiNavigationViewModel.OnkomitentSelected += UpdateisSelected;

            KomitentiDetailViewModel.OnKomitentDeleted += OnKomitentDeleted;
            KomitentiDetailViewModel.OnKomitentSaved += OnKomitentSaved;
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
        public IKomitentiNavigationViewModel KomitentiNavigationViewModel { get; }
        public IKomitentiDetailViewModel KomitentiDetailViewModel { get; }

        #endregion

        #region Methods
        public override async Task LoadAsync()
        {
            if (KomitentiNavigationViewModel != null)
            {
                await KomitentiNavigationViewModel.LoadAsync();
            }
        }
        private void UpdateisSelected(object? sender, SelectedKomitentArgs e)
        {
            var model = sender as KomitentiNavigationViewModel;

            if (model != null)
            {
                if (model.SelectedKomitent != null)
                {
                    if (e.oldId != null)
                    {
                        var oldItem = model.Komitenti.Where(k => k.Id == e.oldId && k.IsSelected == true).ToList().FirstOrDefault();
                        if (oldItem != null)
                        {
                            oldItem.IsSelected = false;
                            model.SelectedKomitent.IsSelected = true;
                        }
                    }
                    else
                    {
                        model.SelectedKomitent.IsSelected = true;
                    }
                }
            }
        }
        private async void OnOpenDetails(object? sender, SelectedKomitentArgs e)
        {

            if (KomitentiDetailViewModel != null && KomitentiDetailViewModel.HasChanges)
            {
                await KomitentiDetailViewModel.LoadAsync(e.newid);
            }

            await KomitentiDetailViewModel.LoadAsync(e.newid);
        }
        private void OnKomitentSaved(object? sender, KomitentSavedArgs e)
        {
            var lookupitem = KomitentiNavigationViewModel.Komitenti.SingleOrDefault(l => l.Id == e.id);

            if (lookupitem == null)
            {
                KomitentiNavigationViewModel.Komitenti.Add(new KomitentiNavigationItemViewModel(e.id, e.naziv, e.pravno, e.fizicko));
                KomitentiNavigationViewModel.SelectedKomitent = KomitentiNavigationViewModel.Komitenti.First();
            }

            else { lookupitem.PunNaziv = e.naziv; }
        }
        private void OnKomitentDeleted(object? sender, KomitentDeletedArgs e)
        {
            var komitent = KomitentiNavigationViewModel.Komitenti.SingleOrDefault(k => k.Id == e.id);

            if (komitent != null)
            {
                KomitentiNavigationViewModel.Komitenti.Remove(komitent);
                KomitentiNavigationViewModel.SelectedKomitent = KomitentiNavigationViewModel.Komitenti.Last();
            }
        }

        public override void Dispose()
        {
            if (KomitentiDetailViewModel != null && KomitentiNavigationViewModel != null)
            {
                if (KomitentiDetailViewModel.HasChanges)
                {
                    var result = _messageDialogService.ShowOKCancelDialog("Napravili ste promene? Da li zelite da otkazete?", "Question");
                    if (result == MessageDialogResult.Potvrdi)
                    {

                        KomitentiDetailViewModel.OnKomitentDeleted -= OnKomitentDeleted;
                        KomitentiNavigationViewModel.OnkomitentSelected -= OnOpenDetails;
                        KomitentiDetailViewModel.OnKomitentSaved -= OnKomitentSaved;

                        KomitentiDetailViewModel.Dispose();
                        KomitentiNavigationViewModel.Dispose();
                    }
                }

                KomitentiDetailViewModel.OnKomitentDeleted -= OnKomitentDeleted;
                KomitentiNavigationViewModel.OnkomitentSelected -= OnOpenDetails;
                KomitentiDetailViewModel.OnKomitentSaved -= OnKomitentSaved;

                KomitentiDetailViewModel.Dispose();
                KomitentiNavigationViewModel.Dispose();
            }
            base.Dispose();
        }
        #endregion
    }
}
