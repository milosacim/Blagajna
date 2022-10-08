using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.Commands.Interfaces;
using MivexBlagajna.UI.ViewModels.Komitenti;
using MivexBlagajna.UI.ViewModels.MestaTroska;
using MivexBlagajna.UI.ViewModels.Uplate_Isplate;
using Syncfusion.Windows.Shared;
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
        #endregion

        #region Konstruktor

        public MainViewModel(
            KomitentiViewModel komitentiViewModel,
            MestaTroskaViewModel mestaTroskaViewModel,
            UplateIsplateViewModel uplateIsplateViewModel)
        {
            // Initializations
            Workspaces = new ObservableCollection<ViewModelBase>();
            KomitentiViewModel = komitentiViewModel;
            MestaTroskaViewModel = mestaTroskaViewModel;
            UplateIsplateViewModel = uplateIsplateViewModel;

            SelectViewModelCommand = new SelectViewModelCommand<ViewModelBase>(this);
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

        public KomitentiViewModel KomitentiViewModel { get; }
        public MestaTroskaViewModel MestaTroskaViewModel { get; }
        public UplateIsplateViewModel UplateIsplateViewModel { get; }

        #endregion

        #region Methods
        public async Task SelectViewModel(object parameter)
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
        public AsyncCommandGeneric<ViewModelBase> SelectViewModelCommand { get; }

        #endregion
    }
}