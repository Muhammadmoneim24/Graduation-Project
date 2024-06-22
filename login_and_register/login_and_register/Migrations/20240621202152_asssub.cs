using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login_and_register.Migrations
{
    /// <inheritdoc />
    public partial class asssub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubmissionAssignments_ApplicationUserId",
                table: "SubmissionAssignments");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionAssignments_ApplicationUserId_AssignmentId",
                table: "SubmissionAssignments",
                columns: new[] { "ApplicationUserId", "AssignmentId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubmissionAssignments_ApplicationUserId_AssignmentId",
                table: "SubmissionAssignments");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionAssignments_ApplicationUserId",
                table: "SubmissionAssignments",
                column: "ApplicationUserId");
        }
    }
}
