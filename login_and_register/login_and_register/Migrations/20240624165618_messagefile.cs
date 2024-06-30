using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login_and_register.Migrations
{
    /// <inheritdoc />
    public partial class messagefile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "File",
                table: "ChatMessages",
                newName: "FileData");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "ChatMessages");

            migrationBuilder.RenameColumn(
                name: "FileData",
                table: "ChatMessages",
                newName: "File");
        }
    }
}
