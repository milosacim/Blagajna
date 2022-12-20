using MivexBlagajna.UI.EventArgs;
using MivexBlagajna.UI.Wrappers;
using System;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Komitenti.Interfaces
{
    public interface IKomitentiDetailViewModel
    {
        bool HasChanges { get; }
        KomitentWrapper? Komitent { get; set; }
        KomitentWrapper CreateNewKomitent();
        Task LoadAsync(int? komitentId);
        Task LoadMestaTroskaAsync();
        Task SaveKomitentAsync();
        Task DeleteKomitentAsync();
        Task CancelChange();

        event EventHandler<KomitentDeletedArgs> OnKomitentDeleted;
        event EventHandler<KomitentSavedArgs> OnKomitentSaved;
    }
}