using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login_and_register.Migrations
{
    /// <inheritdoc />
    public partial class AddAsssubs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
        name: "SubmissionAssignments",
        columns: table => new
        {
            Id = table.Column<int>(type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
            ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
            AssignmentId = table.Column<int>(type: "int", nullable: false),
            File = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
            Grade = table.Column<int>(type: "INT", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_SubmissionAssignments", x => x.Id);
            table.ForeignKey(
                name: "FK_SubmissionAssignments_AspNetUsers_ApplicationUserId",
                column: x => x.ApplicationUserId,
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                name: "FK_SubmissionAssignments_Assignments_AssignmentId",
                column: x => x.AssignmentId,
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        });

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionAssignments_ApplicationUserId",
                table: "SubmissionAssignments",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionAssignments_AssignmentId",
                table: "SubmissionAssignments",
                column: "AssignmentId",
                unique: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubmissionAssignments");
        }
    }
}
