using MivexBlagajna.UI.ViewModels.Mesta_Troska.Details;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Commands.Mesta_Troska
{
    public class CancelCommand : AsyncCommand
    {
        private readonly IMestaTroskaDetailsViewModel mestaTroskaDetailsViewModel;

        public CancelCommand(IMestaTroskaDetailsViewModel mestaTroskaDetailsViewModel)
        {
            this.mestaTroskaDetailsViewModel = mestaTroskaDetailsViewModel;
        }
        public override bool CanExecute()
        {
            return mestaTroskaDetailsViewModel.HasChanges;
        }

        public override async Task ExecuteAsync()
        {
            await mestaTroskaDetailsViewModel.CancelChange();
        }
    }
}
