using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login_and_register.Migrations
{
    /// <inheritdoc />
    public partial class AddCoursePhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Courses");

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "Courses",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Courses",
                type: "VARCHAR(1000)",
                maxLength: 1000,
                nullable: true);
        }
    }
}
