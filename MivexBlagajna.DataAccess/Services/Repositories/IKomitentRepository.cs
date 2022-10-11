using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Services.Repositories
{
    public interface IKomitentRepository
    {
        Task<IEnumerable<Komitent>> GetAllAsync();
        Task<Komitent> GetByIdAsync(int id);
        Task<int> GetLastKomitentIdAsync();
        Task SaveAsync();
        void CancelChanges();
        bool HasChanges();
        void Add(Komitent komitent);
        Task DeleteAsync(Komitent model);
    }
}
