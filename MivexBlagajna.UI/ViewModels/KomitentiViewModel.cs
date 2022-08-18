using MivexBlagajna.DataAccess.Services;
using MivexBlagajna.UI.Events;
using MivexBlagajna.UI.Views.Services;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MivexBlagajna.UI.ViewModels
{
    public class KomitentiViewModel : ViewModelBase, IDockElement
    {
        #region Fields
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        private IKomitentiDetailViewModel _komitentiDetailViewModel;
        private IKomitentiNavigationViewModel _komitentiNavigationViewModel;
        private Func<IKomitentiDetailViewModel> _komitentiDetailViewModelCreator;
        private Func<IKomitentiNavigationViewModel> _komitentiNavigationViewModelCreator;
        private string _header;
        private DockState _state;
        #endregion

        #region Constructor
        public KomitentiViewModel(
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            Func<IKomitentiDetailViewModel> komitentiDetailViewModelCreator,
            Func<IKomitentiNavigationViewModel> komitentiNavigationViewModelCreator,
            string header = "Komitenti",
            DockState state = DockState.Document)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _komitentiDetailViewModelCreator = komitentiDetailViewModelCreator;
            _komitentiNavigationViewModelCreator = komitentiNavigationViewModelCreator;
            _eventAggregator.GetEvent<OpenKomitentDetailViewEvent>().Subscribe(OnOpenKomitentDetailView);
            _eventAggregator.GetEvent<OnKomitentCancelChangesEvent>().Subscribe(OnKomitentCancelChanges);
            _eventAggregator.GetEvent<OnKomitentiNavigationFilterChangedEvent>().Subscribe(OnFilteredNavigation);
            _header = header;
            _state = state;
        }
        #endregion

        #region Properties
        public string? Header
        {
            get { return _header; }
            set { _header = value; }
        }
        public DockState State
        {
            get { return _state; }
            set { _state = value; }
        }
        public IKomitentiNavigationViewModel KomitentiNavigationViewModel
        {
            get { return _komitentiNavigationViewModel; }
            set { _komitentiNavigationViewModel = value; OnModelPropertyChanged(); }
        }

        public IKomitentiDetailViewModel KomitentiDetailViewModel
        {
            get { return _komitentiDetailViewModel; }
            set { _komitentiDetailViewModel = value; OnModelPropertyChanged(); }
        }
        #endregion

        #region Methods
        public override async Task LoadAsync()
        {
            KomitentiNavigationViewModel = _komitentiNavigationViewModelCreator();
            await KomitentiNavigationViewModel.LoadAsync("", false, false);
        }
        private async void OnOpenKomitentDetailView(int komitentId)
        {
            if (KomitentiDetailViewModel != null && KomitentiDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOKCancelDialog("Napravili ste promene? Da li zelite da otkazete?", "Question");
                if (result == MessageDialogResult.Otkazi)
                {
                    return;
                }
            }

            KomitentiDetailViewModel = _komitentiDetailViewModelCreator();
            await KomitentiDetailViewModel.LoadAsync(komitentId);
        }
        private async void OnKomitentCancelChanges(int komitentId)
        {
            if (KomitentiDetailViewModel != null && KomitentiDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOKCancelDialog("Napravili ste promene? Da li zelite da otkazete?", "Question");
                if (result == MessageDialogResult.Otkazi)
                {
                    return;
                }
            }

            KomitentiDetailViewModel = _komitentiDetailViewModelCreator();
            await KomitentiDetailViewModel.LoadAsync(komitentId);
        }

        private async void OnFilteredNavigation(FilterChangedArgs obj)
        {
            KomitentiNavigationViewModel = _komitentiNavigationViewModelCreator();
            await KomitentiNavigationViewModel.LoadAsync(obj.NazivFilter, obj.FizickoLiceFilter, obj.PravnoLiceFilter);
        }
        #endregion
    }
}
