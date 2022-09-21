using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MivexBlagajna.Data.Models;
using MivexBlagajna.DataAccess.Configurations;

namespace MivexBlagajna.DataAccess
{
    public class MivexBlagajnaDbContext : DbContext
    {
        public MivexBlagajnaDbContext(DbContextOptions options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new KomitentiConfiguration());
            modelBuilder.ApplyConfiguration(new MestoTroskaConfiguration());
        }
        public DbSet<Komitent> Komitenti { get; set; }
        public DbSet<MestoTroska> MestaTroska { get; set; }

        internal static MivexBlagajnaDbContext CreateContext()
        {
            return new MivexBlagajnaDbContext(new DbContextOptionsBuilder<MivexBlagajnaDbContext>().UseSqlServer(
                new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), $"appsettings.json"))
                .AddEnvironmentVariables()
                .Build()
                .GetConnectionString("TestDatabase")
                ).Options);
        }
    }
}
