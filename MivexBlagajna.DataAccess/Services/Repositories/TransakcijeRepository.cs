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
        public async Task<IEnumerable<Transakcija>> GetAllAsync()
        {
            IEnumerable<Transakcija> transakcije = await _context.Transakcije.Where(t => t.Obrisano == false).Include(t => t.Konto).Include(t => t.VrstaNaloga).Include(t => t.Komitent).Include(t => t.MestoTroska).ToListAsync();
            return transakcije;
        }
        public async Task<List<Komitent>> GetAllKomitenti() => await _context.Komitenti.Where(m => m.Obrisano == false).ToListAsync();
        public async Task<List<MestoTroska>> GetAllMestaTroska() => await _context.MestaTroska.Where(m => m.Obrisano == false).ToListAsync();
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
        public async Task<IEnumerable<Transakcija>> GetFinansijskaKarticaAsync(DateTime datumDo)
        {
            var datum = datumDo;
            return await _context.Transakcije.FromSqlRaw($"exec Vrati_Finansijsku_Karicu @{0}", datumDo).ToListAsync();
        }
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
