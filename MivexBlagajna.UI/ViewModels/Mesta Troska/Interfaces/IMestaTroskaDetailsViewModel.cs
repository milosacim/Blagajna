using MivexBlagajna.UI.Wrappers;
using System;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Mesta_Troska.Details
{
    public interface IMestaTroskaDetailsViewModel
    {
        MestoTroskaWrapper MestoTroska { get; set; }
        Task LoadAsync(int? mestoTroskaId);
        Task SaveMestoAsync();
        Task CancelChange();
        Task DeleteAsync();
        Task LoadNadredjenoMestoTroskaAsync(int nadredjeni_Id);

        event EventHandler<SavedMestoTroskaArgs> OnMestoSaved;
        event EventHandler OnMestoDeleted;
        bool HasChanges { get; }
        void Dispose();
    }

    public class SavedMestoTroskaArgs
    {
        public readonly int id;
        public readonly string prefix;
        public readonly string naziv;
        public readonly int nivo;
        public readonly int nadId;

        public SavedMestoTroskaArgs(int id, string prefix, string naziv, int nivo, int nadId)
        {
            this.id = id;
            this.prefix = prefix;
            this.naziv = naziv;
            this.nivo = nivo;
            this.nadId = nadId;
        }
    }
}