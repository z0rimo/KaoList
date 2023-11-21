using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeRabbits.KaoList.Web.Migrations
{
    /// <inheritdoc />
    public partial class Modify_properties_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Query",
                table: "SongSearchLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "SongSearchLogs",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
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

            migrationBuilder.AlterColumn<string>(
                name: "Query",
                table: "SongSearchLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
