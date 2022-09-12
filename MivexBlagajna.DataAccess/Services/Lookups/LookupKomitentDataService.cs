using Microsoft.EntityFrameworkCore;
using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Services.Lookups
{
    public class LookupKomitentDataService : ILookupKomitentDataService
    {
        private MivexBlagajnaDbContext _context;

        public LookupKomitentDataService(MivexBlagajnaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LookupKomitent>> GetLookupKomitentAsync()
        {
            return await _context.Komitenti
                .Select(k => new LookupKomitent
                {
                    Id = k.Komitent_Id,
                    PunNaziv = k.PravnoLice == true ? $"{k.Sifra} - {k.Naziv}" : $"{k.Sifra} - {k.Ime} {k.Prezime}",
                    PravnoLice = k.PravnoLice,
                    FizickoLice = k.FizickoLice

                }).ToListAsync();

        }
    }
}
