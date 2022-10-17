using MivexBlagajna.UI.ViewModels.Komitenti.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Commands.Komitenti
{
    public class DeleteCommand : AsyncCommand
    {
        private readonly IKomitentiDetailViewModel komitentiDetailViewModel;

        public DeleteCommand(IKomitentiDetailViewModel komitentiDetailViewModel)
        {
            this.komitentiDetailViewModel = komitentiDetailViewModel;
        }
        public override bool CanExecute()
        {
            return RunningTasks.Count() == 0 && komitentiDetailViewModel.Komitent != null && !komitentiDetailViewModel.Komitent.HasErrors && !komitentiDetailViewModel.HasChanges;
        }

        public override async Task ExecuteAsync()
        {
            await komitentiDetailViewModel.DeleteKomitentAsync();
        }
    }
}
