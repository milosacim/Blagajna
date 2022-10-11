using MivexBlagajna.UI.ViewModels.Komitenti.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Commands.Komitenti
{
    public class CreateNewKomitentCommand : AsyncCommand
    {
        private readonly IKomitentiDetailViewModel komitentiDetailViewModel;

        public CreateNewKomitentCommand(IKomitentiDetailViewModel komitentiDetailViewModel)
        {
            this.komitentiDetailViewModel = komitentiDetailViewModel;
        }
        public override bool CanExecute()
        {
            return komitentiDetailViewModel.Komitent != null;
        }

        public override async Task ExecuteAsync()
        {
            await komitentiDetailViewModel.LoadAsync(null);
        }
    }
}
