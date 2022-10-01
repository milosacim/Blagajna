using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MivexBlagajna.DataAccess.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "Sifra");

            migrationBuilder.CreateTable(
                name: "Konto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MestaTroska",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Sifra = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR Sifra"),
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
                    Obrisano = table.Column<bool>(type: "bit", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Transakcije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Broj = table.Column<int>(type: "int", nullable: false),
                    Nalog = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opravdan = table.Column<bool>(type: "bit", nullable: true),
                    Neopravndan = table.Column<bool>(type: "bit", nullable: true),
                    Uplata = table.Column<decimal>(type: "decimal(20,5)", nullable: false, defaultValue: 0.0000m),
                    Isplata = table.Column<decimal>(type: "decimal(20,5)", nullable: false, defaultValue: 0.0000m),
                    Komitent_Id = table.Column<int>(type: "int", nullable: false),
                    MestoTroska_Id = table.Column<int>(type: "int", nullable: false),
                    Konto_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transakcije", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transakcije_Komitent_Komitent_Id",
                        column: x => x.Komitent_Id,
                        principalTable: "Komitent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transakcije_Konto_Konto_Id",
                        column: x => x.Konto_Id,
                        principalTable: "Konto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transakcije_MestaTroska_MestoTroska_Id",
                        column: x => x.MestoTroska_Id,
                        principalTable: "MestaTroska",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Komitent_MestoTroska_id",
                table: "Komitent",
                column: "MestoTroska_id");

            migrationBuilder.CreateIndex(
                name: "IX_Komitent_Sifra",
                table: "Komitent",
                column: "Sifra",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transakcije_Komitent_Id",
                table: "Transakcije",
                column: "Komitent_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transakcije_Konto_Id",
                table: "Transakcije",
                column: "Konto_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transakcije_MestoTroska_Id",
                table: "Transakcije",
                column: "MestoTroska_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transakcije");

            migrationBuilder.DropTable(
                name: "Komitent");

            migrationBuilder.DropTable(
                name: "Konto");

            migrationBuilder.DropTable(
                name: "MestaTroska");

            migrationBuilder.DropSequence(
                name: "Sifra");
        }
    }
}
