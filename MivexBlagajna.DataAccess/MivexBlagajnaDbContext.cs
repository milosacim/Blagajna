using Microsoft.EntityFrameworkCore;
using MivexBlagajna.Data.Models;

namespace MivexBlagajna.DataAccess
{
    public class MivexBlagajnaDbContext : DbContext
    {
        public MivexBlagajnaDbContext(DbContextOptions options) : base(options) 
        {

        }

        public DbSet<Komitent> Komitenti { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Komitent>().HasData(
                new Komitent { Id = 1, Sifra = 1, Naziv = "Mivex", Naziv2 = "Mivex DOO", PostanskiBroj = "32000", Mesto = "Cacak", Adresa = "Bulevar Oslobodilaca Cacka 105b", PravnoLice = true, FizickoLice = false },
                new Komitent { Id = 2, Sifra = 2, Ime = "Milos", Prezime = "Acimovic", PostanskiBroj = "32205", Mesto = "Trbusani", Adresa = "Trbusani bb", Telefon = "064/040-8769", PravnoLice = false, FizickoLice = true },
                new Komitent { Id = 3, Sifra = 3, Ime = "Ivan", Prezime = "Cvorovic", PostanskiBroj = "32000", Mesto = "Cacak", Adresa = "Bulevar Oslobodilaca Cacka 105b", Telefon = "064/828-1500", PravnoLice = false, FizickoLice = true },
                new Komitent { Id = 4, Sifra = 4, Naziv = "Maloprodaja Ljubic", Naziv2 = "Mivex Maloprodaja Ljubic", PostanskiBroj = "32000", Mesto = "Cacak", Adresa = "Ljubic", Telefon = "032/352-468", PravnoLice = true, FizickoLice = false }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
