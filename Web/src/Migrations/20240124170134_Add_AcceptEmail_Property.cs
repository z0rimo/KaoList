using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeRabbits.KaoList.Web.Migrations
{
    /// <inheritdoc />
    public partial class Add_AcceptEmail_Property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AcceptEmail",
                table: "KaoListUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptEmail",
                table: "KaoListUsers");
        }
    }
}
