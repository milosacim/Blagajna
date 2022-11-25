using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MivexBlagajna.DataAccess.Migrations
{
    public partial class Modified_Configuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Komitent_MestaTroska_MestoTroska_Id",
                table: "Komitent");

            migrationBuilder.DropForeignKey(
                name: "FK_Transakcije_Komitent_Komitent_Id",
                table: "Transakcije");

            migrationBuilder.DropForeignKey(
                name: "FK_Transakcije_MestaTroska_MestoTroska_Id",
                table: "Transakcije");

            migrationBuilder.AddForeignKey(
                name: "FK_Komitent_MestaTroska_MestoTroska_Id",
                table: "Komitent",
                column: "MestoTroska_Id",
                principalTable: "MestaTroska",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transakcije_Komitent_Komitent_Id",
                table: "Transakcije",
                column: "Komitent_Id",
                principalTable: "Komitent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transakcije_MestaTroska_MestoTroska_Id",
                table: "Transakcije",
                column: "MestoTroska_Id",
                principalTable: "MestaTroska",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Komitent_MestaTroska_MestoTroska_Id",
                table: "Komitent");

            migrationBuilder.DropForeignKey(
                name: "FK_Transakcije_Komitent_Komitent_Id",
                table: "Transakcije");

            migrationBuilder.DropForeignKey(
                name: "FK_Transakcije_MestaTroska_MestoTroska_Id",
                table: "Transakcije");

            migrationBuilder.AddForeignKey(
                name: "FK_Komitent_MestaTroska_MestoTroska_Id",
                table: "Komitent",
                column: "MestoTroska_Id",
                principalTable: "MestaTroska",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transakcije_Komitent_Komitent_Id",
                table: "Transakcije",
                column: "Komitent_Id",
                principalTable: "Komitent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transakcije_MestaTroska_MestoTroska_Id",
                table: "Transakcije",
                column: "MestoTroska_Id",
                principalTable: "MestaTroska",
                principalColumn: "Id");
        }
    }
}
