using MivexBlagajna.UI.ViewModels.Mesta_Troska.Details;
using System.Linq;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Commands.Mesta_Troska
{
    public class DeleteCommand : AsyncCommand
    {
        private readonly IMestaTroskaDetailsViewModel mestaTroskaDetailsViewModel;

        public DeleteCommand(IMestaTroskaDetailsViewModel mestaTroskaDetailsViewModel)
        {
            this.mestaTroskaDetailsViewModel = mestaTroskaDetailsViewModel;
        }
        public override bool CanExecute()
        {
            return RunningTasks.Count() == 0;
        }

        public override async Task ExecuteAsync()
        {
            await mestaTroskaDetailsViewModel.DeleteAsync();
        }
    }
}
