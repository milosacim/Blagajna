using MivexBlagajna.DataAccess.Services;
using Syncfusion.Windows.Shared;
using System.Collections.ObjectModel;

namespace MivexBlagajna.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<ViewModelBase>? _workspaces;
        private ViewModelBase? _selectedViewModel;
        private ViewModelBase? _activeDocument;
        private DelegateCommand<object>? _openNewTabCommand;
        private IKomitentDataService _service;
        private KomitentiViewModel _komitentiViewModel;

        public MainViewModel(IKomitentDataService service, KomitentiViewModel komitentiViewModel)
        {
            Workspaces = new ObservableCollection<ViewModelBase>();
            _service = service;
            _komitentiViewModel = komitentiViewModel;
        }

        public ObservableCollection<ViewModelBase> Workspaces
        {
            get { return _workspaces; }
            set { _workspaces = value; }
        }

        public ViewModelBase ActiveViewModel
        {
            get { return _activeDocument; }
            set { _activeDocument = value; RaisePropertyChanged("ActiveViewModel"); }
        }

        public void OpenViewinWorkspaceTab()
        {
            KomitentiViewModel viewModel = _komitentiViewModel;
            Workspaces.Add(viewModel);
            ActiveViewModel = viewModel;
        }

        public async void SelectViewModel(object parameter)
        {
            SelectedViewModel = parameter as ViewModelBase;
            if (SelectedViewModel != null)
            {
                await SelectedViewModel.LoadAsync();
            }
        }

        public ViewModelBase SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
            }
        }

        public DelegateCommand<object> OpenNewTabCommand
        {
            get
            {
                if (_openNewTabCommand == null)
                {
                    _openNewTabCommand = new DelegateCommand<object>(param => OpenViewinWorkspaceTab());
                }
                return _openNewTabCommand;
            }
        }
    }
}
