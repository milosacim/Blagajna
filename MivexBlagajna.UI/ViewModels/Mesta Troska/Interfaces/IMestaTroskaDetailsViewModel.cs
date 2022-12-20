using MivexBlagajna.Data.Models;
using MivexBlagajna.UI.EventArgs;
using MivexBlagajna.UI.Wrappers;
using System;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Mesta_Troska.Details
{
    public interface IMestaTroskaDetailsViewModel : IDisposable
    {
        MestoTroskaWrapper MestoTroska { get; set; }
        Task LoadAsync(int? mestoTroskaId);
        Task LoadAllMestaTroska();
        Task SaveMestoAsync();
        Task CancelChange();
        Task DeleteAsync();

        event EventHandler<SavedMestoTroskaArgs> OnMestoSaved;
        event EventHandler<MestoTroskaDeletedArgs> OnMestoDeleted;
        bool HasChanges { get; }
        void SetPrefix(object? parameter);
    }
}