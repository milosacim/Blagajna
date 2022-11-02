using Microsoft.EntityFrameworkCore;
using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Services.Repositories
{
    public class MestoTroskaRepository : IMestoTroskaRepository
    {
        private readonly MivexBlagajnaDbContext _context;
        public MestoTroskaRepository(MivexBlagajnaDbContext context)
        {
            _context = context;
        }
        public async Task<MestoTroska> GetByIdAsync(int id)
        {
            return await _context.MestaTroska.Include(m => m.RoditeljMestoTroska).Include(m => m.DecaMestoTroska).SingleAsync(m => m.Id == id);
        }
        public async Task<IEnumerable<MestoTroska>> GetAll()
        {
            return await _context.MestaTroska.Where(m => m.Obrisano == false)
                .Include(m => m.DecaMestoTroska)
                .Include(m => m.RoditeljMestoTroska )
                .Select(m => new MestoTroska()
                {
                    Id = m.Id,
                    NadredjenoMesto_Id = m.NadredjenoMesto_Id,
                    Prefix = m.Prefix,
                    Naziv = m.Naziv,
                    Obrisano = m.Obrisano,
                    DecaMestoTroska = m.DecaMestoTroska,
                    RoditeljMestoTroska = m.RoditeljMestoTroska

                }).ToListAsync();
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
        public void Add(MestoTroska mestoTroska)
        {
            _context.MestaTroska.Add(mestoTroska);
        }
        public async Task RemoveAsync(MestoTroska mestoTroska)
        {
            if (mestoTroska != null)
            {
                _context.MestaTroska.Remove(mestoTroska);
                await _context.SaveChangesAsync();
            }
        }
    }
}
