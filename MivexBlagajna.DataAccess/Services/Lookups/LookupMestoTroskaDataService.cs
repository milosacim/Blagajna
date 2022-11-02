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
            return await _context.MestaTroska.Where(m => m.Obrisano == false).Include(m => m.Komitenti)
                .Select(m => new LookupMestoTroska
                {
                    Id = m.Id,
                    Sifra = m.Prefix,
                    Naziv = m.Naziv,
                    NadredjenoMesto_Id = m.NadredjenoMesto_Id,

                }).ToListAsync();
        }
    }
}
