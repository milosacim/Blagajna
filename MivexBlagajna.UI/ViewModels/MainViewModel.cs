using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.ViewModels.Komitenti;
using MivexBlagajna.UI.ViewModels.MestaTroska;
using MivexBlagajna.UI.ViewModels.Uplate_Isplate;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        #region Fields
        private ObservableCollection<ViewModelBase>? _workspaces;
        private ViewModelBase? _selectedViewModel;
        private ViewModelBase? _activeDocument;

        private Func<KomitentiViewModel> _komitentiViewModelCreator;
        private Func<MestaTroskaViewModel> _mestaTroskaViewModelCreator;
        private Func<UplateIsplateViewModel> _uplateIsplateViewModelCreator;
        #endregion

        #region Konstruktor

        public MainViewModel(
            Func<KomitentiViewModel> komitentiViewModelCreator
            , Func<MestaTroskaViewModel> mestaTroskaViewModelCreator
            , Func<UplateIsplateViewModel> uplateIsplateViewModelCreator
            )
        {
            // Initializations
            Workspaces = new ObservableCollection<ViewModelBase>();
            _komitentiViewModelCreator = komitentiViewModelCreator;
            _mestaTroskaViewModelCreator = mestaTroskaViewModelCreator;
            _uplateIsplateViewModelCreator = uplateIsplateViewModelCreator;

            SelectViewModelCommand = new SelectViewModelCommand<ViewModelType>(this);
        }

        #endregion

        #region Properties

        public ObservableCollection<ViewModelBase> Workspaces
        {
            get { return _workspaces; }
            set { _workspaces = value; }
        }


        public ViewModelBase? ActiveViewModel
        {
            get { return _activeDocument; }
            set {
                var oldValue = _activeDocument;
                _activeDocument = value; 
                OnModelPropertyChanged(oldValue, value); 
            }
        }
        public ViewModelBase? SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                var oldValue = _selectedViewModel;
                _selectedViewModel = value;
                OnModelPropertyChanged(oldValue, value);
            }
        }

        #endregion

        #region Methods
        public async Task SelectViewModel(object parameter)
        {
            switch (parameter)
            {
                case ViewModelType.Komitenti:
                    SelectedViewModel = _komitentiViewModelCreator();
                    break;
                case ViewModelType.MestaTroska:
                    SelectedViewModel = _mestaTroskaViewModelCreator();
                    break;
                case ViewModelType.UplateIsplate:
                    SelectedViewModel = _uplateIsplateViewModelCreator();
                    break;

                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel", "viewType");

            }
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
                if (Workspaces.Contains(SelectedViewModel))
                {
                    ActiveViewModel = SelectedViewModel;
                }
                else
                {
                    Workspaces.Add(SelectedViewModel);
                    ActiveViewModel = Workspaces.LastOrDefault();
                }
            }
        }

        #endregion

        #region Commands
        public AsyncCommandGeneric<ViewModelType> SelectViewModelCommand { get; }

        #endregion
    }
}