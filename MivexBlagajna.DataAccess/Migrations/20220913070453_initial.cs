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
                    Komitent_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sifra = table.Column<int>(type: "int", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Naziv2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Jmbg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostanskiBroj = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Pib = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaticniBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mesto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KontaktOsoba = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PravnoLice = table.Column<bool>(type: "bit", nullable: false),
                    FizickoLice = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komitenti", x => x.Komitent_Id);
                });

            migrationBuilder.CreateTable(
                name: "MestaTroska",
                columns: table => new
                {
                    MestoTroska_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sifra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Nivo = table.Column<int>(type: "int", nullable: false),
                    NadredjenoMesto_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MestaTroska", x => x.MestoTroska_Id);
                });

            migrationBuilder.InsertData(
                table: "Komitenti",
                columns: new[] { "Komitent_Id", "Adresa", "FizickoLice", "Ime", "Jmbg", "KontaktOsoba", "MaticniBroj", "Mesto", "Naziv", "Naziv2", "Pib", "PostanskiBroj", "PravnoLice", "Prezime", "Sifra", "Telefon" },
                values: new object[,]
                {
                    { 1, "Bulevar Oslobodilaca Cacka 105b", false, null, null, "Ivan Čvorović", "17123629", "Cacak", "MIVEX D.O.O. za trgovinu transport i usluge", "Mivex DOO", "100898155", "32102", true, null, 1, "032-310-180" },
                    { 2, "Bulevar Oslobodilaca Cacka 105b", false, null, null, "Dejan Čvorović", "20368578", "Cacak", "MIVEX LOGISTICS D.O.O. za trgovinu transport i usluge ", "Mivex DOO", "105441444", "32102", true, null, 2, "032-310-180" },
                    { 3, "Bulevar Oslobodilaca Cacka 105b", false, null, null, "Ivan Čvorović", "07167237", "Cacak", "ČAČANKA D.O.O. za proizvodnju i promet alk. i bezal. pića", "Mivex DOO", "104792570", "32102", true, null, 3, "032-310-180" },
                    { 4, "Bulevar Oslobodilaca Cacka 105b", true, "Ivan", "12345678910123", null, null, "Cacak", null, null, null, null, false, "Čvorović", 4, "064/8281-500" },
                    { 5, "Milenka Nikšića 50", false, null, null, "Dragan Mladenović", null, "Cacak", "MP 18 - Maloprodaja Ljubić", "Maloprodaja Ljubić", null, "32000", true, null, 5, null }
                });

            migrationBuilder.InsertData(
                table: "MestaTroska",
                columns: new[] { "MestoTroska_Id", "NadredjenoMesto_Id", "Naziv", "Nivo", "Sifra" },
                values: new object[,]
                {
                    { 1, 0, "Veleprodaja", 1, "01" },
                    { 2, 0, "Maloprodaja", 1, "02" },
                    { 3, 0, "Usluge", 1, "03" },
                    { 4, 0, "Osnivači", 1, "04" },
                    { 5, 0, "Cer", 1, "05" },
                    { 6, 0, "Objekti", 1, "06" },
                    { 7, 0, "Restoran", 1, "07" },
                    { 8, 1, "Komercijala", 1, "01.01" },
                    { 9, 8, "Kancelarija", 1, "01.01.01" },
                    { 10, 8, "Teren", 1, "01.01.02" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Komitenti");

            migrationBuilder.DropTable(
                name: "MestaTroska");
        }
    }
}
