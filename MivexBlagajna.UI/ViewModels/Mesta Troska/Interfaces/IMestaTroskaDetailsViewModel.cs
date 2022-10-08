using MivexBlagajna.UI.Wrappers;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Mesta_Troska.Details
{
    public interface IMestaTroskaDetailsViewModel
    {
        MestoTroskaWrapper MestoTroska { get; set; }
        Task LoadAsync(int? mestoTroskaId);
        Task DeleteAsync();
        Task LoadNadredjenoMestoTroskaAsync(int nadredjeni_Id);
        event MestaTroskaDetailsViewModel.SaveMestoTroskaHandler OnMestoSaved;
        event MestaTroskaDetailsViewModel.DeleteMestoTroskaHandler OnMestoDeleted;
        bool HasChanges { get; }
        void Dispose();
    }
}