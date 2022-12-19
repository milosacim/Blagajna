using MivexBlagajna.UI.ViewModels.Uplate_Isplate;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Commands.Transakcije
{
    public class SaveTransakcijaCommand : AsyncCommand
    {
        private readonly IUplateIsplateViewModel uplateIsplateViewModel;

        public SaveTransakcijaCommand(IUplateIsplateViewModel uplateIsplateViewModel)
        {
            this.uplateIsplateViewModel = uplateIsplateViewModel;
        }
        public override bool CanExecute()
        {
            return uplateIsplateViewModel.HasChanges && !uplateIsplateViewModel.Transakcija.HasErrors;
        }

        public override async Task ExecuteAsync()
        {
           await uplateIsplateViewModel.SaveTransakcijaAsync();
        }
    }
}
