using MivexBlagajna.UI.ViewModels.Komitenti.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Commands.Komitenti
{
    public class CancelCommand : AsyncCommand
    {
        private IKomitentiDetailViewModel _komitentiDetailViewModel;

        public CancelCommand(IKomitentiDetailViewModel komitentiDetailViewModel)
        {
            _komitentiDetailViewModel = komitentiDetailViewModel;
        }
        public override bool CanExecute()
        {
            if (KomitentiDetailViewModel.Komitent != null)
            {
                return ( RunningTasks.Count() == 0 && KomitentiDetailViewModel.HasChanges ) || ( KomitentiDetailViewModel.Komitent.HasErrors || KomitentiDetailViewModel.Komitent.IsEditable || KomitentiDetailViewModel.Komitent == null );
            }
            else
            {
                return false;
            }
        }

        public override async Task ExecuteAsync()
        {
            await KomitentiDetailViewModel.CancelChange();
        }


        public IKomitentiDetailViewModel KomitentiDetailViewModel
        {
            get { return _komitentiDetailViewModel; }
            set { _komitentiDetailViewModel = value; }
        }

    }
}
