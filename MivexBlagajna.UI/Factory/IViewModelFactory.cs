using MivexBlagajna.UI.State;
using MivexBlagajna.UI.ViewModels;

namespace MivexBlagajna.UI.Factory
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewModelType viewType);
    }
}