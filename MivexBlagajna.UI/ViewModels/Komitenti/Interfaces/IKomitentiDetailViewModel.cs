using MivexBlagajna.UI.Wrappers;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Komitenti.Interfaces
{
    public interface IKomitentiDetailViewModel
    {
        KomitentWrapper Komitent { get; set; }
        bool HasChanges { get; }
        Task LoadAsync(int? komitentId);
        Task SaveKomitentAsync();
        Task DeleteKomitentAsync();
        Task CancelChange();
        void Dispose();
    }
}