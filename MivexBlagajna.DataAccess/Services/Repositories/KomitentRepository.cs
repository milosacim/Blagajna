using Microsoft.EntityFrameworkCore;
using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Services.Repositories
{
    public class KomitentRepository : IKomitentRepository
    {
        private readonly MivexBlagajnaDbContext _context;

        public KomitentRepository(MivexBlagajnaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Komitent>> GetAllAsync()
        {
            return await _context.Komitenti
                .Select(k => new Komitent
                {
                    Id = k.Id,
                    Sifra = k.Sifra,
                    Naziv = k.Naziv,
                    Naziv2 = k.Naziv2,
                    Ime = k.Ime,
                    Prezime = k.Prezime,
                    Jmbg = k.Jmbg,
                    PostanskiBroj = k.PostanskiBroj,
                    Pib = k.Pib,
                    MaticniBroj = k.MaticniBroj,
                    Mesto = k.Mesto,
                    Adresa = k.Adresa,
                    KontaktOsoba = k.KontaktOsoba,
                    Telefon = k.Telefon,
                    PravnoLice = k.PravnoLice,
                    FizickoLice = k.FizickoLice,
                    MestoTroska_id = k.MestoTroska_id,
                    MestoTroska = k.MestoTroska

                }).ToListAsync();
        }
        public async Task<Komitent> GetByIdAsync(int id)
        {
            return await _context.Komitenti.SingleAsync(k => k.Id == id);
        }
        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
        public void CancelChanges()
        {
            var changedEntries = _context.ChangeTracker.Entries()
                .Where(k => k.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Add(Komitent komitent)
        {
            _context.Komitenti.Add(komitent);
        }

        public async Task<int> GetLastKomitentIdAsync()
        {
            return await _context.Komitenti.MaxAsync(k => k.Id);
        }

        public async Task DeleteAsync(Komitent komitent)
        {
            if (komitent != null)
            {
                _context.Komitenti.Remove(komitent);
                await _context.SaveChangesAsync();
            }
        }
    }
}