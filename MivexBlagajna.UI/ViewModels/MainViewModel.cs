using Syncfusion.Windows.Shared;
using System.Collections.ObjectModel;

namespace MivexBlagajna.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<ViewModelBase> _workspaces;
        private ViewModelBase _selectedViewModel;
        private ViewModelBase _activeDocument;



        public MainViewModel()
        {
            Workspaces = new ObservableCollection<ViewModelBase>();
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
            KomitentiViewModel viewModel = new KomitentiViewModel("Komitenti", DockState.Document);
            Workspaces.Add(viewModel);
            ActiveViewModel = viewModel;
        }

        public async void SelectViewModel(object parameter)
        {
            SelectedViewModel = parameter as ViewModelBase;
            //if (SelectedViewModel != null)
            //{
            //    await SelectedViewModel.LoadAsync();
            //}
        }

        public ViewModelBase SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
            }
        }

        private DelegateCommand<object> _openNewTabCommand;

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
