using Microsoft.EntityFrameworkCore;
using MivexBlagajna.Data.Models.Lookups;

namespace MivexBlagajna.DataAccess.Services.Lookups
{
    public class LookupMestoTroskaDataService : ILookupMestoTroskaDataService
    {
        private readonly MivexBlagajnaDbContext _context;
        public LookupMestoTroskaDataService(MivexBlagajnaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LookupMestoTroska>> GetLookupMestoTroskaAsync()
        {
            return await _context.MestaTroska.Where(m => m.Obrisano == false)
                .Select(m => new LookupMestoTroska
                {
                    Id = m.Id,
                    Sifra = m.Prefix,
                    Naziv = m.Naziv,
                    Nivo = m.Nivo,
                    NadredjenoMesto_Id = m.NadredjenoMesto_Id,

                }).ToListAsync();
        }
    }
}
