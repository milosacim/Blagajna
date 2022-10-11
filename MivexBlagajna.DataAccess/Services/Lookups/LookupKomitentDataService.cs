using Microsoft.EntityFrameworkCore;
using MivexBlagajna.Data.Models.Lookups;

namespace MivexBlagajna.DataAccess.Services.Lookups
{
    public class LookupKomitentDataService : ILookupKomitentDataService
    {
        private readonly MivexBlagajnaDbContext _context;

        public LookupKomitentDataService(MivexBlagajnaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LookupKomitent>> GetLookupKomitentAsync()
        {
            return await _context.Komitenti
                .Select(k => new LookupKomitent
                {
                    Id = k.Id,
                    PunNaziv = k.PravnoLice == true ? $"{k.Sifra} - {k.Naziv}" : $"{k.Sifra} - {k.Ime} {k.Prezime}",
                    PravnoLice = k.PravnoLice,
                    FizickoLice = k.FizickoLice,
                    MestoTroska = $"{k.MestoTroska.Prefix} - {k.MestoTroska.Naziv}"

                }).ToListAsync();
        }
    }
}
