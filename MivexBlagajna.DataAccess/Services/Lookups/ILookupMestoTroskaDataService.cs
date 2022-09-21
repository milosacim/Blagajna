using MivexBlagajna.Data.Models.Lookups;

namespace MivexBlagajna.DataAccess.Services.Lookups
{
    public interface ILookupMestoTroskaDataService
    {
        Task<IEnumerable<LookupMestoTroska>> GetLookupMestoTroskaAsync();
    }
}