using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Services.Repositories
{
    public interface ITransakcijeRepository
    {
        void Add(Transakcija transakcija);
        Task SaveAsync();
        Task<IEnumerable<Transakcija>> GetAllAsync();
        Task<int> GetLastBrojNalogaAsync();
    }
}