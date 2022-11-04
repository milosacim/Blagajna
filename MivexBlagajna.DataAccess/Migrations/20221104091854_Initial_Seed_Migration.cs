using Microsoft.EntityFrameworkCore.Migrations;
using MivexBlagajna.Data.Models;

#nullable disable

namespace MivexBlagajna.DataAccess.Migrations
{
    public partial class Initial_Seed_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using (var context = MivexBlagajnaDbContext.CreateContext())
            {
                context.MestaTroska.AddRange(
                    new List<MestoTroska>()
                    {
                        new MestoTroska { Prefix = "01", Naziv = "Veleprodaja", Komitenti = new List<Komitent>() {
                            new Komitent { Naziv = "MIVEX D.O.O. za trgovinu transport i usluge", Naziv2 = "Mivex DOO", PostanskiBroj = "32102", Pib= "100898155", MaticniBroj= "17123629", Mesto = "Cacak", Adresa = "Bulevar Oslobodilaca Cacka 105b", KontaktOsoba= "Ivan Čvorović", Telefon= "032-310-180", PravnoLice = true, FizickoLice = false },
                            new Komitent { Naziv = "MIVEX LOGISTICS D.O.O. za trgovinu transport i usluge ", Naziv2 = "Mivex DOO", PostanskiBroj = "32102", Pib= "105441444", MaticniBroj= "20368578", Mesto = "Cacak", Adresa = "Bulevar Oslobodilaca Cacka 105b", KontaktOsoba= "Dejan Čvorović", Telefon= "032-310-180", PravnoLice = true, FizickoLice = false }
                            }
                        },

                        new MestoTroska { Prefix = "02", Naziv = "Maloprodaja", Komitenti = new List<Komitent>() { new Komitent { Naziv = "MP 18 - Maloprodaja Ljubić", Naziv2 = "Maloprodaja Ljubić", PostanskiBroj = "32000", Mesto = "Cacak", Adresa = "Milenka Nikšića 50", KontaktOsoba = "Dragan Mladenović", PravnoLice = true, FizickoLice = false } } },
                        new MestoTroska { Prefix = "03", Naziv = "Usluge" },
                        new MestoTroska { Prefix = "04", Naziv = "Osnivači", Komitenti = new List<Komitent>() { new Komitent { Ime = "Ivan", Prezime = "Čvorović", Jmbg = "1234567891012", Mesto = "Cacak", Adresa = "Bulevar Oslobodilaca Cacka 105b", Telefon= "064/8281-500", PravnoLice =false, FizickoLice = true } } },
                        new MestoTroska { Prefix = "05", Naziv = "Cer" },
                        new MestoTroska { Prefix = "06", Naziv = "Objekti" },
                        new MestoTroska { Prefix = "07", Naziv = "Restoran" },
                        new MestoTroska { Prefix = "08", Naziv = "Čačanka", Komitenti = new List<Komitent>() { new Komitent { Naziv = "ČAČANKA D.O.O. za proizvodnju i promet alk. i bezal. pića", Naziv2 = "Mivex DOO", PostanskiBroj = "32102", Pib = "104792570", MaticniBroj = "07167237", Mesto = "Cacak", Adresa = "Bulevar Oslobodilaca Cacka 105b", KontaktOsoba = "Ivan Čvorović", Telefon = "032-310-180", PravnoLice = true, FizickoLice = false } } }
                    });

                context.Konta.AddRange(
                    new List<Konto>()
                    {
                        new Konto() {Naziv = "Dinarski"},
                        new Konto() {Naziv = "Devizni"},
                        new Konto() {Naziv = "Cekovi"}
                    });

                context.VrsteNalogas.AddRange(
                   new List<VrsteNaloga>()
                   {
                        new VrsteNaloga() {VrstaNaloga = "Dnevnica"},
                        new VrsteNaloga() {VrstaNaloga = "Pazar"},
                        new VrsteNaloga() {VrstaNaloga = "Cekovi"},
                        new VrsteNaloga() {VrstaNaloga = "Plata"}
                   });

                context.SaveChanges();
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
