using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeRabbits.KaoList.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddDaillog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PopularDailySings",
                columns: table => new
                {
                    SingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopularDailySings", x => new { x.Created, x.SingId });
                    table.ForeignKey(
                        name: "FK_PopularDailySings_Sings_SingId",
                        column: x => x.SingId,
                        principalTable: "Sings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PopularDailySings_SingId",
                table: "PopularDailySings",
                column: "SingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PopularDailySings");
        }
    }
}
