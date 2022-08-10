using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Services
{
    public interface ILookupKomitentDataService
    {
        Task<IEnumerable<LookupKomitent>> GetLookupKomitentAsync();
    }
}