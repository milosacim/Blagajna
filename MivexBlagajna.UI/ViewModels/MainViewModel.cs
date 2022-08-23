using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.ViewModels.Komitenti;
using MivexBlagajna.UI.ViewModels.MestaTroska;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MivexBlagajna.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields
        private ObservableCollection<ViewModelBase> _workspaces;
        private ViewModelBase? _selectedViewModel;
        private ViewModelBase _activeDocument;
        #endregion

        #region Konstruktor
        public MainViewModel(KomitentiViewModel komitentiViewModel, MestaTroskaViewModel mestaTroskaViewModel)
        {
            Workspaces = new ObservableCollection<ViewModelBase>();
            KomitentiViewModel = komitentiViewModel;
            MestaTroskaViewModel = mestaTroskaViewModel;
            SelectViewModelCommand = new DelegateCommand(SelectViewModel);
        }
        #endregion

        #region Properties
        public ObservableCollection<ViewModelBase> Workspaces
        {
            get { return _workspaces; }
            set { _workspaces = value; }
        }
        public ViewModelBase ActiveViewModel
        {
            get { return _activeDocument; }
            set { _activeDocument = value; OnModelPropertyChanged(); }
        }
        public ViewModelBase? SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnModelPropertyChanged();
            }
        }

        public async void SelectViewModel(object parameter)
        {
            SelectedViewModel = parameter as ViewModelBase;

            if (SelectedViewModel != null)
            {
                await SelectedViewModel.LoadAsync();
            }
            if (SelectedViewModel != null && ActiveViewModel == SelectedViewModel)
            {
                return;
            } 
            else
            {
                Workspaces.Add(SelectedViewModel);
                ActiveViewModel = SelectedViewModel;
            }
        }
        public KomitentiViewModel KomitentiViewModel { get; }
        public MestaTroskaViewModel MestaTroskaViewModel { get; }
        #endregion

        #region Commands
        public DelegateCommand SelectViewModelCommand { get; }
        #endregion
    }
}