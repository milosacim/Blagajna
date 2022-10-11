using MivexBlagajna.Data.Models.Lookups;

namespace MivexBlagajna.DataAccess.Services.Lookups
{
    public interface ILookupKomitentDataService
    {
        Task<IEnumerable<LookupKomitent>> GetLookupKomitentAsync();
    }
}