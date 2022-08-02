using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeRabbits.KaoList.Web.Migrations
{
    public partial class Editkaraokessingidtoinstrumentalid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Karaokes_Sings_SingId",
                table: "Karaokes");

            migrationBuilder.RenameColumn(
                name: "SingId",
                table: "Karaokes",
                newName: "InstrumentalId");

            migrationBuilder.RenameIndex(
                name: "IX_Karaokes_SingId",
                table: "Karaokes",
                newName: "IX_Karaokes_InstrumentalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Karaokes_Instrumentals_InstrumentalId",
                table: "Karaokes",
                column: "InstrumentalId",
                principalTable: "Instrumentals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Karaokes_Instrumentals_InstrumentalId",
                table: "Karaokes");

            migrationBuilder.RenameColumn(
                name: "InstrumentalId",
                table: "Karaokes",
                newName: "SingId");

            migrationBuilder.RenameIndex(
                name: "IX_Karaokes_InstrumentalId",
                table: "Karaokes",
                newName: "IX_Karaokes_SingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Karaokes_Sings_SingId",
                table: "Karaokes",
                column: "SingId",
                principalTable: "Sings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
