using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MivexBlagajna.DataAccess.Migrations
{
    public partial class Modified_Transakcija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Neopravndan",
                table: "Transakcije");

            migrationBuilder.DropColumn(
                name: "Opravdan",
                table: "Transakcije");

            migrationBuilder.AddColumn<string>(
                name: "VrstaNaloga",
                table: "Transakcije",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VrstaNaloga",
                table: "Transakcije");

            migrationBuilder.AddColumn<bool>(
                name: "Neopravndan",
                table: "Transakcije",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Opravdan",
                table: "Transakcije",
                type: "bit",
                nullable: true);
        }
    }
}
