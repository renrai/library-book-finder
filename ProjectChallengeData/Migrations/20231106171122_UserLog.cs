using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectLibraryData.Migrations
{
    /// <inheritdoc />
    public partial class UserLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserCreation",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserLastUpdate",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserCreation",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserLastUpdate",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCreation",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserLastUpdate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserCreation",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UserLastUpdate",
                table: "Books");
        }
    }
}
