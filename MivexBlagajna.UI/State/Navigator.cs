using MivexBlagajna.UI.ViewModels;

namespace MivexBlagajna.UI.State
{
    public class Navigator : INavigator
    {

        private ViewModelBase? _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { _currentViewModel = value; }
        }

    }
}
