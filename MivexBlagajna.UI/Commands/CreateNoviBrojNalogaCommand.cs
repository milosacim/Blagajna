using MivexBlagajna.UI.ViewModels.Uplate_Isplate;
using System;
using System.Windows.Input;

namespace MivexBlagajna.UI.Commands
{
    public class CreateNoviBrojNalogaCommand : ICommand
    {
        private readonly IUplateIsplateViewModel _uplateIsplateViewModel;

        public CreateNoviBrojNalogaCommand(IUplateIsplateViewModel uplateIsplateViewModel)
        {
            _uplateIsplateViewModel = uplateIsplateViewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
           
        }
    }
}
