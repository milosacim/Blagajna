using MivexBlagajna.UI.ViewModels.Mesta_Troska.Details;
using System.Linq;
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
            if (mestaTroskaDetailsViewModel.MestoTroska != null)
            {
                return (RunningTasks.Count() == 0 && mestaTroskaDetailsViewModel.HasChanges) || (mestaTroskaDetailsViewModel.MestoTroska.HasErrors || mestaTroskaDetailsViewModel.MestoTroska.IsEditable || mestaTroskaDetailsViewModel.MestoTroska == null);
            } else
            {
                return false;
            }
        }

        public override async Task ExecuteAsync()
        {
            await mestaTroskaDetailsViewModel.CancelChange();
        }
    }
}
