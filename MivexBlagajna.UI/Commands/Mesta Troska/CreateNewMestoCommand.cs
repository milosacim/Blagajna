using MivexBlagajna.UI.ViewModels.Mesta_Troska.Details;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Commands.Mesta_Troska
{
    public class CreateNewMestoCommand : AsyncCommand
    {
        private readonly IMestaTroskaDetailsViewModel mestaTroskaDetailsViewModel;

        public CreateNewMestoCommand(IMestaTroskaDetailsViewModel mestaTroskaDetailsViewModel)
        {
            this.mestaTroskaDetailsViewModel = mestaTroskaDetailsViewModel;
        }
        public override bool CanExecute()
        {
            return mestaTroskaDetailsViewModel.MestoTroska != null;
        }

        public override async Task ExecuteAsync()
        {
            await mestaTroskaDetailsViewModel.LoadAsync(null);
        }
    }
}
