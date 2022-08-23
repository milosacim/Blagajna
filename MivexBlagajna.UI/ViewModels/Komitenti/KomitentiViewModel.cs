using MivexBlagajna.UI.Events;
using MivexBlagajna.UI.ViewModels.Komitenti.Interfaces;
using MivexBlagajna.UI.Views.Services;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Komitenti
{
    public class KomitentiViewModel : ViewModelBase, IDockElement
    {
        #region Fields
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        private IKomitentiDetailViewModel _komitentiDetailViewModel;
        private Func<IKomitentiDetailViewModel> _komitentiDetailViewModelCreator;
        private string _header;
        private DockState _state;
        #endregion

        #region Constructor
        public KomitentiViewModel(
            IEventAggregator eventAggregator,
            Func<IKomitentiDetailViewModel> komitentiDetailViewModelCreator,
            IKomitentiNavigationViewModel komitentiNavigationViewModel,
            IMessageDialogService messageDialogService,
            string header = "Komitenti",
            DockState state = DockState.Document)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _komitentiDetailViewModelCreator = komitentiDetailViewModelCreator;
            _eventAggregator.GetEvent<OpenKomitentDetailViewEvent>().Subscribe(OnOpenKomitentDetailView);
            _header = header;
            _state = state;

            KomitentiNavigationViewModel = komitentiNavigationViewModel;
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
        public IKomitentiDetailViewModel KomitentiDetailViewModel
        {
            get { return _komitentiDetailViewModel; }
            set { _komitentiDetailViewModel = value; OnModelPropertyChanged(); }
        }
        #endregion

        #region Methods
        public override async Task LoadAsync()
        {
            await KomitentiNavigationViewModel.LoadAsync();
        }
        private async void OnOpenKomitentDetailView(int? komitentId)
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
        #endregion
    }
}
