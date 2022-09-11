using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Mesta_Troska.Details
{
    public interface IMestaTroskaDetailsViewModel
    {
        Task LoadAsync(int? mestoTroskaId);
        bool HasChanges { get; }
        void Dispose();
    }
}