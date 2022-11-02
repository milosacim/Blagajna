using MivexBlagajna.Data.Models;
using MivexBlagajna.UI.ViewModels.Mesta_Troska.Details;
using System;
using System.Windows.Input;

namespace MivexBlagajna.UI.Commands
{
    internal class CreatePrefixCommand : ICommand
    {
        private readonly IMestaTroskaDetailsViewModel mestaTroskaDetailsViewModel;

        public event EventHandler? CanExecuteChanged;

        public CreatePrefixCommand(IMestaTroskaDetailsViewModel mestaTroskaDetailsViewModel)
        {
            this.mestaTroskaDetailsViewModel = mestaTroskaDetailsViewModel;
        }
        public bool CanExecute(object? parameter)
        {
            return false;
        }

        public void Execute(object? parameter)
        {
            mestaTroskaDetailsViewModel.CreatePrefix(parameter);
        }
    }
}
