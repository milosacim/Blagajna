using Microsoft.EntityFrameworkCore;
using MivexBlagajna.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.DataAccess.Services.Lookups
{
    public class LookupMestoTroskaDataService : ILookupMestoTroskaDataService
    {
        private readonly MivexBlagajnaDbContext _context;
        public LookupMestoTroskaDataService(MivexBlagajnaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LookupMestoTroska>> GetLookupMestoTroskaAsync()
        {
            return await _context.MestaTroska
                .Select(m => new LookupMestoTroska
                {
                    Id = m.Id,
                    Sifra = m.Sifra,
                    Naziv = m.Naziv

                }).ToListAsync();
        }
    }
}
