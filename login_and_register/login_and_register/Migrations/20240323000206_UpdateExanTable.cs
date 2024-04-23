using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login_and_register.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExanTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Exams");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Exams",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumOfQuestions",
                table: "Exams",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "NumOfQuestions",
                table: "Exams");

            migrationBuilder.AddColumn<string>(
                name: "EndDate",
                table: "Exams",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
