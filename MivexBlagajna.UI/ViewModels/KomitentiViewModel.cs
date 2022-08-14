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
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        private IKomitentiDetailViewModel _komitentiDetailViewModel;
        private Func<IKomitentiDetailViewModel> _komitentiDetailViewModelCreator;
        private string _header;
        private DockState _state;

        public KomitentiViewModel(
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IKomitentiNavigationViewModel komitentiNavigationViewModel,
            Func<IKomitentiDetailViewModel> komitentiDetailViewModelCreator,
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

        public IKomitentiNavigationViewModel KomitentiNavigationViewModel { get; }

        public IKomitentiDetailViewModel KomitentiDetailViewModel
        {
            get { return _komitentiDetailViewModel; }
            set { _komitentiDetailViewModel = value; OnModelPropertyChanged(); }
        }

        public override async Task LoadAsync()
        {
            await KomitentiNavigationViewModel.LoadAsync();
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

    }
}
