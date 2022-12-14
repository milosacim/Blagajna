using MivexBlagajna.UI.Commands;
using MivexBlagajna.UI.Commands.Interfaces;
using MivexBlagajna.UI.ViewModels.Kartica;
using MivexBlagajna.UI.ViewModels.Komitenti;
using MivexBlagajna.UI.ViewModels.MestaTroska;
using MivexBlagajna.UI.ViewModels.Uplate_Isplate;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels
{
    public class MainViewModel : ViewModelBase, IClosing, IDisposable
    {
        #region Fields
        private ViewModelBase? _selectedViewModel;

        private Func<KomitentiViewModel> _komitentiViewModelCreator;
        private Func<MestaTroskaViewModel> _mestaTroskaViewModelCreator;
        private Func<UplateIsplateViewModel> _uplateIsplateViewModelCreator;
        private Func<FinansijskaKarticaViewModel> _finansijskaKarticaViewModelCreator;
        private bool disposedValue;
        #endregion

        #region Konstruktor

        public MainViewModel(
            Func<KomitentiViewModel> komitentiViewModelCreator
            , Func<MestaTroskaViewModel> mestaTroskaViewModelCreator
            , Func<UplateIsplateViewModel> uplateIsplateViewModelCreator
            , Func<FinansijskaKarticaViewModel> finansijskaKarticaViewModelCreator
            )
        {
            // Initializations
            Workspaces = new ObservableCollection<ViewModelBase>();
            _komitentiViewModelCreator = komitentiViewModelCreator;
            _mestaTroskaViewModelCreator = mestaTroskaViewModelCreator;
            _uplateIsplateViewModelCreator = uplateIsplateViewModelCreator;
            _finansijskaKarticaViewModelCreator = finansijskaKarticaViewModelCreator;

            SelectViewModelCommand = new SelectViewModelCommand<ViewModelType>(this);
        }


        #endregion

        #region Properties

        public ObservableCollection<ViewModelBase> Workspaces
        {
            get;
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
            CreateViewModel(parameter);

            if (Workspaces.Any())
            {
                foreach (var model in Workspaces)
                {
                    if (model?.GetType().GetProperty("Header").GetValue(model, null) == SelectedViewModel?.GetType().GetProperty("Header").GetValue(SelectedViewModel, null))
                    {
                        return;
                    }
                }
            }
            Workspaces.Add(SelectedViewModel);

            await SelectedViewModel.LoadAsync();
        }

        private void CreateViewModel(object parameter)
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
                case ViewModelType.Kartica:
                    SelectedViewModel = _finansijskaKarticaViewModelCreator();
                    break;

                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel", "viewType");
            }
        }

        public bool OnClosing()
        {
            if (Workspaces.Contains(SelectedViewModel))
            {
                Workspaces.Remove(SelectedViewModel);

                SelectedViewModel.Dispose();

                if (Workspaces.Count >= 1)
                {
                    SelectedViewModel = Workspaces.Last();
                }
            }

            return true;
        }

        #endregion

        #region Commands
        public IAsyncCommandGeneric<ViewModelType> SelectViewModelCommand { get; }

        #endregion
    }
}