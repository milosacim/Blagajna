using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Services.Repositories
{
    public interface IMestoTroskaRepository
    {
        void Add(MestoTroska mestoTroska);
        Task<IEnumerable<MestoTroska>> GetAll();
        Task<MestoTroska> GetByIdAsync(int id);
        void Remove(MestoTroska mestoTroska);
        void CancelChanges();
        Task SaveAsync();
        bool HasChanges();
        Task<int> GetLastIdAsync();
    }
}