using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MivexBlagajna.DataAccess
{
    public class MivexBlagajnaDbContextFactory : IDesignTimeDbContextFactory<MivexBlagajnaDbContext>
    {
        public MivexBlagajnaDbContext CreateDbContext(string[]? args=null)
        {
            var context = MivexBlagajnaDbContext.CreateContext();
            context.Database.Migrate();
            return context;
        }
    }
}
