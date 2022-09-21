using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MivexBlagajna.DataAccess.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MestaTroska",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sifra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Nivo = table.Column<int>(type: "int", nullable: false),
                    NadredjenoMesto_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MestaTroska", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Komitent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sifra = table.Column<int>(type: "int", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Naziv2 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Jmbg = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    PostanskiBroj = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Pib = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaticniBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mesto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KontaktOsoba = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PravnoLice = table.Column<bool>(type: "bit", nullable: false),
                    FizickoLice = table.Column<bool>(type: "bit", nullable: false),
                    MestoTroska_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komitent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Komitent_MestaTroska_MestoTroska_id",
                        column: x => x.MestoTroska_id,
                        principalTable: "MestaTroska",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Komitent_MestoTroska_id",
                table: "Komitent",
                column: "MestoTroska_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Komitent");

            migrationBuilder.DropTable(
                name: "MestaTroska");
        }
    }
}
