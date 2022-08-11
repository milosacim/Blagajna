using Microsoft.EntityFrameworkCore;
using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess.Services
{
    public class KomitentiDataService : IKomitentDataService
    {
        private MivexBlagajnaDbContext _context;
        public KomitentiDataService(MivexBlagajnaDbContext context)
        {
            _context = context;
        }
        public async Task<List<Komitent>> GetAllAsync()
        {
           return await _context.Komitenti.ToListAsync();
        }

        public async Task<Komitent> GetByIdAsync(int id)
        {
            return await _context.Komitenti.SingleAsync(k => k.Id == id);
        }

        public async Task SaveAsync(Komitent komitent)
        {
            _context.Komitenti.Add(komitent);
            _context.Entry(komitent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}