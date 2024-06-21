using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login_and_register.Migrations
{
    /// <inheritdoc />
    public partial class deleteexam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsSubs_Questions_QuestionId",
                table: "QuestionsSubs");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsSubs_Questions_QuestionId",
                table: "QuestionsSubs",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsSubs_Questions_QuestionId",
                table: "QuestionsSubs");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsSubs_Questions_QuestionId",
                table: "QuestionsSubs",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");
        }
    }
}
