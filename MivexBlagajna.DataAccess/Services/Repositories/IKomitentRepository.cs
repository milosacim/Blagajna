using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Services.Repositories
{
    public interface IKomitentRepository
    {
        Task<Komitent> GetByIdAsync(int id);
        Task SaveAsync();
        Task CancelChanges(int id);
        bool HasChanges();
    }
}
