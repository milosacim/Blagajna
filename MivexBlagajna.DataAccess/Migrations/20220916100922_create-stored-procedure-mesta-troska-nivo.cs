using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MivexBlagajna.DataAccess.Migrations
{
    public partial class createstoredproceduremestatroskanivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo.Vrati_nivoe_mesta_troska]
                        AS
                        BEGIN
                            SET NOCOUNT ON
                            SELECT DISTINCT mt.Nivo FROM MestaTroska mt
                        END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
