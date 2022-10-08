using MivexBlagajna.UI.ViewModels.Komitenti.Interfaces;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Commands
{
    public class SaveKomitentCommand : AsyncCommand
    {
        private readonly IKomitentiDetailViewModel komitentiDetailViewModel;

        public SaveKomitentCommand(
            IKomitentiDetailViewModel komitentiDetailViewModel)
        {
            this.komitentiDetailViewModel = komitentiDetailViewModel;
        }

        public override bool CanExecute()
        {
            return komitentiDetailViewModel.Komitent != null && !komitentiDetailViewModel.Komitent.HasErrors && komitentiDetailViewModel.HasChanges;
        }

        public override async Task ExecuteAsync()
        {
            await komitentiDetailViewModel.SaveKomitentAsync();
        }
    }
}
