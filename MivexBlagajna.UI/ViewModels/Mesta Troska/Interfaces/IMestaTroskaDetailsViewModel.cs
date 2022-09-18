using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Mesta_Troska.Details
{
    public interface IMestaTroskaDetailsViewModel
    {
        Task LoadAsync(int? mestoTroskaId);
        Task LoadNadredjenoMestoTroskaAsync(int nadredjeni_Id);
        bool HasChanges { get; }
        void Dispose();
    }
}