using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Services.Lookups
{
    public interface ILookupMestoTroskaDataService
    {
        Task<IEnumerable<LookupMestoTroska>> GetLookupMestoTroskaAsync();
    }
}