using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeRabbits.KaoList.Web.Migrations
{
    /// <inheritdoc />
    public partial class ModifySetNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongSearchLogs_KaoListUsers_UserId",
                table: "SongSearchLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_SoundPlayLogs_KaoListUsers_UserId",
                table: "SoundPlayLogs");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SoundPlayLogs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SongSearchLogs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_SongSearchLogs_KaoListUsers_UserId",
                table: "SongSearchLogs",
                column: "UserId",
                principalTable: "KaoListUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SoundPlayLogs_KaoListUsers_UserId",
                table: "SoundPlayLogs",
                column: "UserId",
                principalTable: "KaoListUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongSearchLogs_KaoListUsers_UserId",
                table: "SongSearchLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_SoundPlayLogs_KaoListUsers_UserId",
                table: "SoundPlayLogs");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SoundPlayLogs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SongSearchLogs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SongSearchLogs_KaoListUsers_UserId",
                table: "SongSearchLogs",
                column: "UserId",
                principalTable: "KaoListUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoundPlayLogs_KaoListUsers_UserId",
                table: "SoundPlayLogs",
                column: "UserId",
                principalTable: "KaoListUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
