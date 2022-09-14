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
        public DbSet<MestoTroska> MestaTroska { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }

    public static class ModelbuildeExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Komitent>().HasData(
                new Komitent { Komitent_Id = 1, Sifra = 1, Naziv = "MIVEX D.O.O. za trgovinu transport i usluge", Naziv2 = "Mivex DOO", PostanskiBroj = "32102", Pib = "100898155", MaticniBroj = "17123629", Mesto = "Cacak", Adresa = "Bulevar Oslobodilaca Cacka 105b", KontaktOsoba = "Ivan Čvorović", Telefon = "032-310-180", PravnoLice = true, FizickoLice = false },
                new Komitent { Komitent_Id = 2, Sifra = 2, Naziv = "MIVEX LOGISTICS D.O.O. za trgovinu transport i usluge ", Naziv2 = "Mivex DOO", PostanskiBroj = "32102", Pib = "105441444", MaticniBroj = "20368578", Mesto = "Cacak", Adresa = "Bulevar Oslobodilaca Cacka 105b", KontaktOsoba = "Dejan Čvorović", Telefon = "032-310-180", PravnoLice = true, FizickoLice = false },
                new Komitent { Komitent_Id = 3, Sifra = 3, Naziv = "ČAČANKA D.O.O. za proizvodnju i promet alk. i bezal. pića", Naziv2 = "Mivex DOO", PostanskiBroj = "32102", Pib = "104792570", MaticniBroj = "07167237", Mesto = "Cacak", Adresa = "Bulevar Oslobodilaca Cacka 105b", KontaktOsoba = "Ivan Čvorović", Telefon = "032-310-180", PravnoLice = true, FizickoLice = false },
                new Komitent { Komitent_Id = 4, Sifra = 4, Ime = "Ivan", Prezime = "Čvorović", Jmbg = "12345678910123", Mesto = "Cacak", Adresa = "Bulevar Oslobodilaca Cacka 105b", Telefon = "064/8281-500", PravnoLice = false, FizickoLice = true },
                new Komitent { Komitent_Id = 5, Sifra = 5, Naziv = "MP 18 - Maloprodaja Ljubić", Naziv2 = "Maloprodaja Ljubić", PostanskiBroj = "32000", Mesto = "Cacak", Adresa = "Milenka Nikšića 50", KontaktOsoba = "Dragan Mladenović", PravnoLice = true, FizickoLice = false }
                );

            modelBuilder.Entity<MestoTroska>().HasData(
                new MestoTroska { MestoTroska_Id = 1, Sifra = "01", Naziv = "Veleprodaja", Nivo = 1, NadredjenoMesto_Id = 0 },
                new MestoTroska { MestoTroska_Id = 2, Sifra = "02", Naziv = "Maloprodaja", Nivo = 1, NadredjenoMesto_Id = 0 },
                new MestoTroska { MestoTroska_Id = 3, Sifra = "03", Naziv = "Usluge", Nivo = 1, NadredjenoMesto_Id = 0 },
                new MestoTroska { MestoTroska_Id = 4, Sifra = "04", Naziv = "Osnivači", Nivo = 1, NadredjenoMesto_Id = 0 },
                new MestoTroska { MestoTroska_Id = 5, Sifra = "05", Naziv = "Cer", Nivo = 1, NadredjenoMesto_Id = 0 },
                new MestoTroska { MestoTroska_Id = 6, Sifra = "06", Naziv = "Objekti", Nivo = 1, NadredjenoMesto_Id = 0 },
                new MestoTroska { MestoTroska_Id = 7, Sifra = "07", Naziv = "Restoran", Nivo = 1, NadredjenoMesto_Id = 0 },
                new MestoTroska { MestoTroska_Id = 8, Sifra = "01.01", Naziv = "Komercijala", Nivo = 1, NadredjenoMesto_Id = 1 },
                new MestoTroska { MestoTroska_Id = 9, Sifra = "01.01.01", Naziv = "Kancelarija", Nivo = 1, NadredjenoMesto_Id = 8 },
                new MestoTroska { MestoTroska_Id = 10, Sifra = "01.01.02", Naziv = "Teren", Nivo = 1, NadredjenoMesto_Id = 8 }

                );
        }
    }
}
