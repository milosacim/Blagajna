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

        public void Add(Transakcija transakcija)
        {
            _context.Add(transakcija);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transakcija>> GetAllAsync()
        {
            IEnumerable<Transakcija> transakcije = await _context.Transakcije.Include(t => t.Konto).Include(t => t.VrstaNaloga).Include(t => t.Komitent).Include(t => t.MestoTroska).ToListAsync();
            return transakcije;
        }

        public async Task<int> GetLastBrojNalogaAsync()
        {
            return await _context.Transakcije.MaxAsync(t => t.Broj);
        }
        
        public List<VrsteNaloga> GetAllVrsteNaloga()
        {
            return _context.VrsteNalogas.ToList();
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
