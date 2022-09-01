﻿using Microsoft.EntityFrameworkCore;
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
            return await _context.MestaTroska.SingleAsync(k => k.Id == id);
        }

        public async Task<IEnumerable<MestoTroska>> GetAll()
        {
            IEnumerable<MestoTroska> mesta = await _context.MestaTroska.ToListAsync();
            return mesta;
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
    }
}
