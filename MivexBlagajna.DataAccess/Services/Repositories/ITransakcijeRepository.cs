using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Services.Repositories
{
    public interface ITransakcijeRepository
    {
        void Add(Transakcija transakcija);
        Task SaveAsync();
        Task<IEnumerable<Transakcija>> GetAllAsync();
        Task<List<Komitent>> GetAllKomitenti();
        Task<List<MestoTroska>> GetAllMestaTroska();
        Task<List<Konto>> GetAllKonta();
        Task<List<VrsteNaloga>> GetAllVrsteNaloga();
        bool HasChanges();
        void CancelChanges();
        Task DeleteAsync(Transakcija transakcija);
    }
}