using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Services.Repositories;
using MivexBlagajna.UI.Events.Komitenti;
using MivexBlagajna.UI.ViewModels.Komitenti.Interfaces;
using MivexBlagajna.UI.Views.Services;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.Commands
{
    public class DeleteCommand : AsyncCommandBase
    {
        private readonly Func<Task> _callback;

        public DeleteCommand(Func<Task> callback)
        {
            _callback = callback;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            await _callback();
        }
    }
}
