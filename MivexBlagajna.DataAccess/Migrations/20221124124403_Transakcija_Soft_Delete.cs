using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MivexBlagajna.DataAccess.Migrations
{
    public partial class Transakcija_Soft_Delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Uplata",
                table: "Transakcije",
                type: "decimal(20,5)",
                nullable: false,
                defaultValue: 0.0000m,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)",
                oldDefaultValue: 0.0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Isplata",
                table: "Transakcije",
                type: "decimal(20,5)",
                nullable: false,
                defaultValue: 0.0000m,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,2)",
                oldDefaultValue: 0.0m);

            migrationBuilder.AddColumn<bool>(
                name: "Obrisano",
                table: "Transakcije",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Obrisano",
                table: "Transakcije");

            migrationBuilder.AlterColumn<decimal>(
                name: "Uplata",
                table: "Transakcije",
                type: "decimal(20,2)",
                nullable: false,
                defaultValue: 0.0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,5)",
                oldDefaultValue: 0.0000m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Isplata",
                table: "Transakcije",
                type: "decimal(20,2)",
                nullable: false,
                defaultValue: 0.0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,5)",
                oldDefaultValue: 0.0000m);
        }
    }
}
