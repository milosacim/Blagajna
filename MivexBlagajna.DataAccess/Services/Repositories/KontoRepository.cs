using Microsoft.EntityFrameworkCore;
using MivexBlagajna.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.DataAccess.Services.Repositories
{
    public class KontoRepository : IKontoRepository
    {
        private readonly MivexBlagajnaDbContext _context;

        public KontoRepository(MivexBlagajnaDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Konto>> GetAllAsync()
        {
            IEnumerable<Konto> konta = await _context.Konta.ToListAsync();
            return konta;
        }
    }
}
