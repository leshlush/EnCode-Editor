using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnapSaves.Data.Migrations.Identity
{
    /// <inheritdoc />
    public partial class AddLtiUserForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LtiUsers",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            // Directly create the index
            migrationBuilder.Sql("CREATE INDEX IX_LtiUsers_UserId ON LtiUsers (UserId);");

            migrationBuilder.AddForeignKey(
                name: "FK_LtiUsers_AspNetUsers_UserId",
                table: "LtiUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }





        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LtiUsers_AspNetUsers_UserId",
                table: "LtiUsers");

            migrationBuilder.Sql("DROP INDEX IX_LtiUsers_UserId ON LtiUsers;");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LtiUsers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

    }
}
