using MivexBlagajna.UI.Commands.Interfaces;
using MivexBlagajna.UI.ViewModels.Uplate_Isplate;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Commands.Transakcije
{
    public class TransakcijaCancelChangeCommand : AsyncCommand
    {
        private readonly IUplateIsplateViewModel _uplateIsplateViewModel;

        public TransakcijaCancelChangeCommand(IUplateIsplateViewModel uplateIsplateViewModel)
        {
            _uplateIsplateViewModel = uplateIsplateViewModel;
        }

        public override bool CanExecute()
        {
            return _uplateIsplateViewModel.HasChanges;
        }

        public override async Task ExecuteAsync()
        {
            await _uplateIsplateViewModel.CancelChange();
        }
    }
}
