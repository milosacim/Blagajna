using Microsoft.EntityFrameworkCore;
using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Services.Repositories
{
    public class MestoTroskaRepository : IMestoTroskaRepository
    {
        private MivexBlagajnaDbContext _context;

        public MestoTroskaRepository(MivexBlagajnaDbContext context)
        {
            _context = context;
        }
        public async Task<MestoTroska> GetByIdAsync(int id)
        {
            return await _context.MestaTroska.SingleAsync(m => m.MestoTroska_Id == id);
        }
        public async Task<IEnumerable<MestoTroska>> GetAll()
        {
            IEnumerable<MestoTroska> mesta = await _context.MestaTroska.ToListAsync();
            return mesta;
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

        public void Remove(MestoTroska mestoTroska)
        {
            _context.MestaTroska.Remove(mestoTroska);
        }

        public async Task<int> GetLastMestoIdAsync()
        {
            return await _context.MestaTroska.MaxAsync(m => m.MestoTroska_Id);
        }

        public async Task<int> GetLastIdAsync()
        {
            return await _context.MestaTroska.MaxAsync(m => m.MestoTroska_Id);
        }
    }
}
