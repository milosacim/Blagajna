using MivexBlagajna.UI.ViewModels;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Commands
{
    public class SelectViewModelCommand<ViewModelType> : AsyncCommandGeneric<ViewModelType>
    {
        private readonly IMainViewModel _mainViewModel;
        public SelectViewModelCommand(IMainViewModel mainViewModel) => _mainViewModel = mainViewModel;
        public override bool CanExecute(ViewModelType parameter) => true;
        public override async Task ExecuteAsync(ViewModelType parameter) => await _mainViewModel.SelectViewModel(parameter);
    }
}