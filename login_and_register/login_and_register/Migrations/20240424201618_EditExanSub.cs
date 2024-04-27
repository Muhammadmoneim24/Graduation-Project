using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login_and_register.Migrations
{
    /// <inheritdoc />
    public partial class EditExanSub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_AspNetUsers_ApplicationUserId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Exams_ExamId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Questions_QuestionId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_ApplicationUserId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_ExamId",
                table: "Submissions");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Submissions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ApplicationUserId",
                table: "Submissions",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ExamId",
                table: "Submissions",
                column: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_AspNetUsers_ApplicationUserId",
                table: "Submissions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Exams_ExamId",
                table: "Submissions",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Questions_QuestionId",
                table: "Submissions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_AspNetUsers_ApplicationUserId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Exams_ExamId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Questions_QuestionId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_ApplicationUserId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_ExamId",
                table: "Submissions");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Submissions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ApplicationUserId",
                table: "Submissions",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ExamId",
                table: "Submissions",
                column: "ExamId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_AspNetUsers_ApplicationUserId",
                table: "Submissions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Exams_ExamId",
                table: "Submissions",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Questions_QuestionId",
                table: "Submissions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");
        }
    }
}
