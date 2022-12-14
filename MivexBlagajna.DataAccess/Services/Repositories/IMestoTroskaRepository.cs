using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Services.Repositories
{
    public interface IMestoTroskaRepository
    {
        void Add(MestoTroska mestoTroska);
        Task<IEnumerable<MestoTroska>> GetAllAsync();
        Task<MestoTroska> GetByIdAsync(int id);
        Task RemoveAsync(MestoTroska mestoTroska);
        void CancelChanges();
        Task SaveAsync();
        bool HasChanges();
    }
}