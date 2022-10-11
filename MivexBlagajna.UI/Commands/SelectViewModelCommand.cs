using MivexBlagajna.UI.ViewModels;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Commands
{
    public class SelectViewModelCommand<ViewModelBase> : AsyncCommandGeneric<ViewModelBase>
    {
        private readonly IMainViewModel mainViewModel;
        public SelectViewModelCommand(IMainViewModel mainViewModel) => this.mainViewModel = mainViewModel;
        public override bool CanExecute(ViewModelBase parameter) => parameter != null;
        public override async Task ExecuteAsync(ViewModelBase parameter) => await mainViewModel.SelectViewModel(parameter);
    }
}