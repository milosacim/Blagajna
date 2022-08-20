using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels
{
    public interface IKomitentiDetailViewModel
    {
        Task LoadAsync(int komitentId);
        bool HasChanges { get; }
    }
}