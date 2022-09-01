using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Komitenti.Interfaces
{
    public interface IKomitentiDetailViewModel
    {
        Task LoadAsync(int? komitentId);
        bool HasChanges { get; }
        void Dispose();
    }
}