using MivexBlagajna.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MivexBlagajna.DataAccess.Services
{
    public interface IKomitentDataService
    {
        Task<List<Komitent>> GetAllAsync();
        Task<Komitent> GetByIdAsync(int id);

        Task SaveAsync(Komitent komitent);
    }
}
