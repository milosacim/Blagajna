﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<NosilacTroska> NosiociTroska { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Komitent>().HasData(
            //    new Komitent { Id = 1, Sifra = 1, Naziv = "MIVEX D.O.O. za trgovinu transport i usluge", Naziv2 = "Mivex DOO", PostanskiBroj = "32102", Pib= "100898155", MaticniBroj= "17123629", Mesto = "Cacak", Adresa = "Bulevar Oslobodilaca Cacka 105b", KontaktOsoba= "Ivan Čvorović", Telefon= "032-310-180", PravnoLice = true, FizickoLice = false },
            //    new Komitent { Id = 2, Sifra = 2, Naziv = "MIVEX LOGISTICS D.O.O. za trgovinu transport i usluge ", Naziv2 = "Mivex DOO", PostanskiBroj = "32102", Pib= "105441444", MaticniBroj= "20368578", Mesto = "Cacak", Adresa = "Bulevar Oslobodilaca Cacka 105b", KontaktOsoba= "Dejan Čvorović", Telefon= "032-310-180", PravnoLice = true, FizickoLice = false },
            //    new Komitent { Id = 3, Sifra = 3, Naziv = "ČAČANKA D.O.O. za proizvodnju i promet alk. i bezal. pića", Naziv2 = "Mivex DOO", PostanskiBroj = "32102", Pib= "104792570", MaticniBroj= "07167237", Mesto = "Cacak", Adresa = "Bulevar Oslobodilaca Cacka 105b", KontaktOsoba= "Ivan Čvorović", Telefon= "032-310-180", PravnoLice = true, FizickoLice = false },
            //    new Komitent { Id = 4, Sifra = 4, Ime = "Ivan", Prezime = "Čvorović", Jmbg = "12345678910123", Mesto = "Cacak", Adresa = "Bulevar Oslobodilaca Cacka 105b", Telefon= "064/8281-500", PravnoLice =false, FizickoLice = true },
            //    new Komitent { Id = 5, Sifra = 5, Naziv = "MP 18 - Maloprodaja Ljubić", Naziv2 = "Maloprodaja Ljubić", PostanskiBroj = "32000", Mesto = "Cacak", Adresa = "Milenka Nikšića 50", KontaktOsoba= "Dragan Mladenović", PravnoLice = true, FizickoLice = false }
            //    );
            //modelBuilder.Entity<MestoTroska>().HasData(
            //    new MestoTroska { Id = 1, Sifra = "01", Naziv = "Veleprodaja" },
            //    new MestoTroska { Id = 2, Sifra = "02", Naziv = "Maloprodaja" },
            //    new MestoTroska { Id = 3, Sifra = "03", Naziv = "Usluge" },
            //    new MestoTroska { Id = 4, Sifra = "04", Naziv = "Osnivači" },
            //    new MestoTroska { Id = 5, Sifra = "05", Naziv = "Cer" },
            //    new MestoTroska { Id = 6, Sifra = "06", Naziv = "Objekti" },
            //    new MestoTroska { Id = 7, Sifra = "07", Naziv = "Restoran" },
            //    new MestoTroska { Id = 8, Sifra = "08", Naziv = "Čačanka" }
            //    );

            base.OnModelCreating(modelBuilder);
        }
    }
}
