using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login_and_register.Migrations
{
    /// <inheritdoc />
    public partial class GropMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "ChatMessages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChatGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatGroups_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatGroupMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatGroupMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatGroupMembers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatGroupMembers_ChatGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "ChatGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_GroupId",
                table: "ChatMessages",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatGroupMembers_GroupId",
                table: "ChatGroupMembers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatGroupMembers_UserId",
                table: "ChatGroupMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatGroups_OwnerId",
                table: "ChatGroups",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_ChatGroups_GroupId",
                table: "ChatMessages",
                column: "GroupId",
                principalTable: "ChatGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_ChatGroups_GroupId",
                table: "ChatMessages");

            migrationBuilder.DropTable(
                name: "ChatGroupMembers");

            migrationBuilder.DropTable(
                name: "ChatGroups");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_GroupId",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "ChatMessages");
        }
    }
}
