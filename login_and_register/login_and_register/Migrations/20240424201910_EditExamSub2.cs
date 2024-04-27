using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login_and_register.Migrations
{
    /// <inheritdoc />
    public partial class EditExamSub2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Submissions_ApplicationUserId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_QuestionId",
                table: "Submissions");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ApplicationUserId",
                table: "Submissions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_QuestionId",
                table: "Submissions",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Submissions_ApplicationUserId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_QuestionId",
                table: "Submissions");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ApplicationUserId",
                table: "Submissions",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_QuestionId",
                table: "Submissions",
                column: "QuestionId",
                unique: true);
        }
    }
}
