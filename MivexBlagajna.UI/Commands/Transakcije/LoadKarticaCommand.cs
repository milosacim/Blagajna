using MivexBlagajna.UI.ViewModels.Kartica;
using System;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Commands.Transakcije
{
    public class LoadKarticaCommand : AsyncCommand
    {
        private readonly FinansijskaKarticaViewModel _model;

        public LoadKarticaCommand(FinansijskaKarticaViewModel model)
        {
            _model = model;
        }
        public override bool CanExecute()
        {
            return true;
        }

        public override async Task ExecuteAsync()
        {
            await _model.LoadData(_model.DatumOd);
        }
    }
}
