using MivexBlagajna.UI.ViewModels.Komitenti.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Commands
{
    public class SaveKomitentCommand : AsyncCommand
    {
        private readonly IKomitentiDetailViewModel _komitentiDetailViewModel;

        public SaveKomitentCommand(
            IKomitentiDetailViewModel komitentiDetailViewModel)
        {
            _komitentiDetailViewModel = komitentiDetailViewModel;
        }

        public override bool CanExecute()
        {
            return RunningTasks.Count() == 0 && _komitentiDetailViewModel.Komitent != null && !_komitentiDetailViewModel.Komitent.HasErrors && _komitentiDetailViewModel.HasChanges;
        }

        public override async Task ExecuteAsync()
        {
            await _komitentiDetailViewModel.SaveKomitentAsync();
        }
    }
}
