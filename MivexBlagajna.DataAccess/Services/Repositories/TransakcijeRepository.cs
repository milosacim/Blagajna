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
    }
}
