using Microsoft.EntityFrameworkCore;
using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Services.Repositories
{
    public class KomitentRepository : IKomitentRepository
    {
        private MivexBlagajnaDbContext _context;
        public KomitentRepository(MivexBlagajnaDbContext context)
        {
            _context = context;
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
        public void Remove(Komitent komitent)
        {
            _context.Komitenti.Remove(komitent);
        }
    }
}