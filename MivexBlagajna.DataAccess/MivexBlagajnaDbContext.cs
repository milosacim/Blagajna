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
            modelBuilder.HasSequence<int>("Sifre").StartsAt(1).IncrementsBy(1);

            modelBuilder.Entity<Komitent>().Property(k => k.Sifra).HasDefaultValueSql("NEXT VALUE FOR Sifre");
            modelBuilder.Entity<Komitent>().HasIndex(k => k.Sifra).IsUnique(true);

            modelBuilder.ApplyConfiguration(new KomitentiConfiguration());
            modelBuilder.ApplyConfiguration(new MestoTroskaConfiguration());
            modelBuilder.ApplyConfiguration(new KontoConfiguration());
            modelBuilder.ApplyConfiguration(new TransakcijaConfiguration());
            modelBuilder.ApplyConfiguration(new VrsteNalogaConfiguration());
        }

        public DbSet<Komitent> Komitenti { get; set; }
        public DbSet<MestoTroska> MestaTroska { get; set; }
        public DbSet<Konto> Konta { get; set; }
        public DbSet<Transakcija> Transakcije { get; set; }
        public DbSet<VrsteNaloga> VrsteNalogas { get; set; }

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
