using Microsoft.EntityFrameworkCore;
using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Services.Repositories
{
    public class TransakcijeRepository : ITransakcijeRepository
    {
        private readonly MivexBlagajnaDbContext _context;
        public TransakcijeRepository(MivexBlagajnaDbContext context)
        {
            _context = context;
        }
        public void Add(Transakcija transakcija) => _context.Add(transakcija);
        public async Task SaveAsync() => await _context.SaveChangesAsync();
        public async Task<IEnumerable<Transakcija>> GetAllAsync() => await _context.Transakcije.FromSqlRaw($"exec sp_vrati_sve_transakcije").ToListAsync();
        public async Task<List<Konto>> GetAllKonta() => await _context.Konta.ToListAsync();
        public async Task<List<VrsteNaloga>> GetAllVrsteNaloga() => await _context.VrsteNalogas.ToListAsync();
        public bool HasChanges() => _context.ChangeTracker.HasChanges();
        public void CancelChanges()
        {
            var changedEntries = _context.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged).ToList();

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
        public async Task<IEnumerable<StavkaKartice>> GetFinansijskaKarticaAsync(string? uslov) => 
            await _context.StavkeKartice.FromSqlRaw($"exec sp_vrati_finansijsku_karticu {uslov}").ToListAsync();
        public async Task<IEnumerable<Komitent>> GetKomitentiAsync() => 
            await _context.Komitenti.FromSqlRaw($"exec sp_vrati_komitente").ToListAsync();
        public async Task<IEnumerable<MestoTroska>> GetMestaTroskaAsync() => 
            await _context.MestaTroska.FromSqlRaw($"exec sp_vrati_mestaTroska").ToListAsync();

        public async Task DeleteAsync(Transakcija transakcija)
        {
            if (transakcija != null)
            {
                _context.Transakcije.Remove(transakcija);
                await _context.SaveChangesAsync();
            }
        }
    }
}
