using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login_and_register.Migrations
{
    /// <inheritdoc />
    public partial class Editmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Options",
                table: "Questions",
                type: "VARCHAR(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Tittle",
                table: "Exams",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Instructions",
                table: "Exams",
                type: "VARCHAR(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Options",
                table: "Questions",
                type: "VARCHAR(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Tittle",
                table: "Exams",
                type: "VARCHAR(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Instructions",
                table: "Exams",
                type: "VARCHAR(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(500)",
                oldMaxLength: 500);
        }
    }
}
