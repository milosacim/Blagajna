using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MivexBlagajna.DataAccess.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Komitenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sifra = table.Column<int>(type: "int", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Naziv2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostanskiBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mesto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KontaktOsoba = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PravnoLice = table.Column<bool>(type: "bit", nullable: false),
                    FizickoLice = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komitenti", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Komitenti",
                columns: new[] { "Id", "Adresa", "FizickoLice", "Ime", "KontaktOsoba", "Mesto", "Naziv", "Naziv2", "PostanskiBroj", "PravnoLice", "Prezime", "Sifra", "Telefon" },
                values: new object[,]
                {
                    { 1, "Bulevar Oslobodilaca Cacka 105b", false, null, null, "Cacak", "Mivex", "Mivex DOO", "32000", true, null, 1, null },
                    { 2, "Trbusani bb", true, "Milos", null, "Trbusani", null, null, "32205", false, "Acimovic", 2, "064/040-8769" },
                    { 3, "Bulevar Oslobodilaca Cacka 105b", true, "Ivan", null, "Cacak", null, null, "32000", false, "Cvorovic", 3, "064/828-1500" },
                    { 4, "Ljubic", false, null, null, "Cacak", "Maloprodaja Ljubic", "Mivex Maloprodaja Ljubic", "32000", true, null, 4, "032/352-468" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Komitenti");
        }
    }
}
