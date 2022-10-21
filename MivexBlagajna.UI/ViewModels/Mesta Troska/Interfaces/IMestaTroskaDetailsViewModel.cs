using MivexBlagajna.Data.Models;
using MivexBlagajna.UI.Wrappers;
using System;
using System.Threading.Tasks;

namespace MivexBlagajna.UI.ViewModels.Mesta_Troska.Details
{
    public interface IMestaTroskaDetailsViewModel
    {
        MestoTroskaWrapper MestoTroska { get; set; }
        Task LoadAsync(int? mestoTroskaId, int? nadMestoTroskaId);
        Task LoadAllMestaTroska();
        Task SaveMestoAsync();
        Task CancelChange();
        Task DeleteAsync();

        event EventHandler<SavedMestoTroskaArgs> OnMestoSaved;
        event EventHandler<MestoTroskaDeletedArgs> OnMestoDeleted;
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

    public class MestoTroskaDeletedArgs
    {
        public int id;
        public int nadId;

        public MestoTroskaDeletedArgs(int id, int nadId)
        {
            this.id = id;
            this.nadId = nadId;
        }
    }
}