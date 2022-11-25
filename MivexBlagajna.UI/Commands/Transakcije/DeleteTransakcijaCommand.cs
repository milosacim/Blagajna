using MivexBlagajna.UI.ViewModels.Uplate_Isplate;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Commands.Transakcije
{
    class DeleteTransakcijaCommand : AsyncCommand
    {
        private readonly IUplateIsplateViewModel _uplateIsplateViewModel;

        public DeleteTransakcijaCommand(IUplateIsplateViewModel uplateIsplateViewModel)
        {
            _uplateIsplateViewModel = uplateIsplateViewModel;
        }
        public override bool CanExecute()
        {
            return _uplateIsplateViewModel.Transakcija != null;
        }

        public override async Task ExecuteAsync()
        {
            await _uplateIsplateViewModel.DeleteTransakcijaAsync(_uplateIsplateViewModel.Transakcija);
        }
    }
}
