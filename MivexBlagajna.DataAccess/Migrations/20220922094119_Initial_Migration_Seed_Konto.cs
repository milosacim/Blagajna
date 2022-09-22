using Microsoft.EntityFrameworkCore.Migrations;
using MivexBlagajna.Data.Models;

#nullable disable

namespace MivexBlagajna.DataAccess.Migrations
{
    public partial class Initial_Migration_Seed_Konto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using (var context = MivexBlagajnaDbContext.CreateContext())
            {
                context.Konta.AddRange(
                    new List<Konto>()
                    {
                        new Konto() {Naziv = "Dinarski"},
                        new Konto() {Naziv = "Devizni"},
                        new Konto() {Naziv = "Cekovi"}
                    });
                context.SaveChanges();
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
