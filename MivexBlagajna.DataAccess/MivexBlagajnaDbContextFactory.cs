using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MivexBlagajna.DataAccess
{
    public class MivexBlagajnaDbContextFactory : IDesignTimeDbContextFactory<MivexBlagajnaDbContext>
    {
        public MivexBlagajnaDbContext CreateDbContext(string[]? args=null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MivexBlagajnaDbContext>();
            optionsBuilder.UseSqlServer("Server=192.168.0.144;Database=MivexBlagajnaDb;User Id=retail01;Password=mivex***032;");

            return new MivexBlagajnaDbContext(optionsBuilder.Options);
        }
    }
}
