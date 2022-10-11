using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Services.Repositories
{
    public interface IKontoRepository
    {
        Task<IEnumerable<Konto>> GetAllAsync();
    }
}