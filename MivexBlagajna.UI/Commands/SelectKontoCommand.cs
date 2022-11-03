using MivexBlagajna.UI.ViewModels.Uplate_Isplate;
using System;
using System.Windows.Input;

namespace MivexBlagajna.UI.Commands
{
    public class SelectKontoCommand : ICommand
    {
        private readonly IUplateIsplateViewModel _uplateIsplateViewModel;

        public event EventHandler? CanExecuteChanged;

        public SelectKontoCommand(IUplateIsplateViewModel uplateIsplateViewModel)
        {
            _uplateIsplateViewModel = uplateIsplateViewModel;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is VrsteKontaEnum)
            {
                VrsteKontaEnum vrstaKonta = (VrsteKontaEnum)parameter;

                _uplateIsplateViewModel.SelectedKonto = _uplateIsplateViewModel.SelectKonto(vrstaKonta);
            }
        }
    }
}
