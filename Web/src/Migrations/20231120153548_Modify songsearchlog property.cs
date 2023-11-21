using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeRabbits.KaoList.Web.Migrations
{
    /// <inheritdoc />
    public partial class Modifysongsearchlogproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "SongSearchLogs",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "SingId",
                table: "SongSearchLogs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateTable(
                name: "SongDetailLogs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    SingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdentityToken = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongDetailLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SongDetailLogs_KaoListUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "KaoListUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SongDetailLogs_Sings_SingId",
                        column: x => x.SingId,
                        principalTable: "Sings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongSearchLogs_SingId",
                table: "SongSearchLogs",
                column: "SingId");

            migrationBuilder.CreateIndex(
                name: "IX_PopularDailySings_SingId",
                table: "PopularDailySings",
                column: "SingId");

            migrationBuilder.CreateIndex(
                name: "IX_SongDetailLogs_SingId",
                table: "SongDetailLogs",
                column: "SingId");

            migrationBuilder.CreateIndex(
                name: "IX_SongDetailLogs_UserId",
                table: "SongDetailLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SongSearchLogs_Sings_SingId",
                table: "SongSearchLogs",
                column: "SingId",
                principalTable: "Sings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongSearchLogs_Sings_SingId",
                table: "SongSearchLogs");

            migrationBuilder.DropTable(
                name: "PopularDailySings");

            migrationBuilder.DropTable(
                name: "SongDetailLogs");

            migrationBuilder.DropIndex(
                name: "IX_SongSearchLogs_SingId",
                table: "SongSearchLogs");

            migrationBuilder.DropColumn(
                name: "SingId",
                table: "SongSearchLogs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "SongSearchLogs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");
        }
    }
}
