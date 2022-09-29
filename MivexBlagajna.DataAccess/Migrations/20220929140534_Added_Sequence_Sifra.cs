using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MivexBlagajna.DataAccess.Migrations
{
    public partial class Added_Sequence_Sifra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "Sifra");

            migrationBuilder.AlterColumn<int>(
                name: "Sifra",
                table: "Komitent",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR Sifra",
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "Sifra");

            migrationBuilder.AlterColumn<int>(
                name: "Sifra",
                table: "Komitent",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "NEXT VALUE FOR Sifra");
        }
    }
}
