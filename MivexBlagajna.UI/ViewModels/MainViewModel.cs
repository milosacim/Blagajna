using MivexBlagajna.DataAccess.Services;
using Syncfusion.Windows.Shared;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<ViewModelBase> _workspaces;
        private ViewModelBase? _selectedViewModel;
        private ViewModelBase _activeDocument;
        //private DelegateCommand<object>? _openNewTabCommand;

        public MainViewModel(KomitentiViewModel komitentiViewModel)
        {
            Workspaces = new ObservableCollection<ViewModelBase>();
            KomitentiViewModel = komitentiViewModel;
            SelectViewModelCommand = new Commands.DelegateCommand(SelectViewModel);
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

        public ViewModelBase? SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged();
            }
        }

        //public void OpenViewinWorkspaceTab()
        //{
        //    ViewModelBase viewModel = SelectedViewModel;
        //    Workspaces.Add(viewModel);
        //    ActiveViewModel = viewModel;
        //}

        public async void SelectViewModel(object parameter)
        {
            SelectedViewModel = parameter as ViewModelBase;

            if (SelectedViewModel != null)
            {
                await SelectedViewModel.LoadAsync();
            }
            Workspaces.Add(SelectedViewModel);
            ActiveViewModel = SelectedViewModel;
        }

        public KomitentiViewModel KomitentiViewModel { get; }
        public Commands.DelegateCommand SelectViewModelCommand { get; }


        //public DelegateCommand<object> OpenNewTabCommand
        //{
        //    get
        //    {
        //        if (_openNewTabCommand == null)
        //        {
        //            _openNewTabCommand = new DelegateCommand<object>(param => OpenViewinWorkspaceTab());
        //        }
        //        return _openNewTabCommand;
        //    }
        //}

    }
}
