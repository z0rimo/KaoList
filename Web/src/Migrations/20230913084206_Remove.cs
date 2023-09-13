using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeRabbits.KaoList.Web.Migrations
{
    /// <inheritdoc />
    public partial class Remove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongDetailLogs_KaoListUsers_UserId",
                table: "SongDetailLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_SongDetailLogs_Sings_SingId",
                table: "SongDetailLogs");

            migrationBuilder.DropIndex(
                name: "IX_SongDetailLogs_SingId",
                table: "SongDetailLogs");

            migrationBuilder.DropIndex(
                name: "IX_SongDetailLogs_UserId",
                table: "SongDetailLogs");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SongDetailLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SingId",
                table: "SongDetailLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityToken",
                table: "SongDetailLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "SongDetailLogs",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SongDetailLogs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SingId",
                table: "SongDetailLogs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityToken",
                table: "SongDetailLogs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "SongDetailLogs",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SongDetailLogs_SingId",
                table: "SongDetailLogs",
                column: "SingId");

            migrationBuilder.CreateIndex(
                name: "IX_SongDetailLogs_UserId",
                table: "SongDetailLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SongDetailLogs_KaoListUsers_UserId",
                table: "SongDetailLogs",
                column: "UserId",
                principalTable: "KaoListUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SongDetailLogs_Sings_SingId",
                table: "SongDetailLogs",
                column: "SingId",
                principalTable: "Sings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
