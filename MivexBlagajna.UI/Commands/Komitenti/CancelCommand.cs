using MivexBlagajna.UI.ViewModels.Komitenti.Interfaces;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Commands.Komitenti
{
    public class CancelCommand : AsyncCommand
    {
        private readonly IKomitentiDetailViewModel komitentiDetailViewModel;

        public CancelCommand(IKomitentiDetailViewModel komitentiDetailViewModel)
        {
            this.komitentiDetailViewModel = komitentiDetailViewModel;
        }
        public override bool CanExecute()
        {
            return komitentiDetailViewModel.HasChanges;
        }

        public override async Task ExecuteAsync()
        {
            await komitentiDetailViewModel.CancelChange();
        }
    }
}
