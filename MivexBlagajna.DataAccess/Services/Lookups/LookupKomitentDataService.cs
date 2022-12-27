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
            return await _context.Komitenti.Include(k => k.MestoTroska).Where(k => k.Obrisano == false)
                .Select(k => new LookupKomitent
                {
                    Id = k.Id,
                    PunNaziv = k.PravnoLice == true ? $"{k.Sifra} - {k.Naziv}" : $"{k.Sifra} - {k.Ime} {k.Prezime}",
                    PravnoLice = k.PravnoLice,
                    FizickoLice = k.FizickoLice,
                    Adresa = k.Adresa,
                    Mesto = k.Mesto,
                    PostanskiBroj = k.PostanskiBroj,
                    Kontakt = k.Telefon

                }).ToListAsync();
        }
    }
}
