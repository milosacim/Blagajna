using MivexBlagajna.UI.ViewModels.Mesta_Troska.Details;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Commands.Mesta_Troska
{
    public class SaveMestoTroskaCommand : AsyncCommand
    {
        private readonly IMestaTroskaDetailsViewModel mestaTroskaDetailsViewModel;

        public SaveMestoTroskaCommand(IMestaTroskaDetailsViewModel mestaTroskaDetailsViewModel)
        {
            this.mestaTroskaDetailsViewModel = mestaTroskaDetailsViewModel;
        }

        public override bool CanExecute()
        {
            return mestaTroskaDetailsViewModel.MestoTroska != null && !mestaTroskaDetailsViewModel.MestoTroska.HasErrors && mestaTroskaDetailsViewModel.HasChanges;
        }

        public override async Task ExecuteAsync()
        {
            await mestaTroskaDetailsViewModel.SaveMestoAsync();
        }
    }
}
