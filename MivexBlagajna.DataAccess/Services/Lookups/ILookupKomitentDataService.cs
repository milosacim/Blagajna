using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Services.Lookups
{
    public interface ILookupKomitentDataService
    {
        Task<IEnumerable<LookupKomitent>> GetLookupKomitentAsync();
    }
}