using MivexBlagajna.Data.Models.Lookups;

namespace MivexBlagajna.DataAccess.Services.Lookups
{
    public class LookupTransakcijaDataService
    {
        private readonly MivexBlagajnaDbContext _context;

        public LookupTransakcijaDataService(MivexBlagajnaDbContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<LookupTransakcija>> GetLookupTransakcija()
        //{
        //    return await _context.Transakcije.Where
        //}
    }
}
