using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Services.Repositories
{
    public interface ITransakcijeRepository
    {
        void Add(Transakcija transakcija);
        Task SaveAsync();
        Task<IEnumerable<Transakcija>> GetAllAsync();
        Task<IEnumerable<StavkaKartice>> GetFinansijskaKarticaAsync(string? uslov);
        Task<List<Konto>> GetAllKonta();
        Task<List<VrsteNaloga>> GetAllVrsteNaloga();
        Task<IEnumerable<Komitent>> GetKomitentiAsync();
        Task<IEnumerable<MestoTroska>> GetMestaTroskaAsync();

        bool HasChanges();
        void CancelChanges();
        Task DeleteAsync(Transakcija transakcija);
    }
}