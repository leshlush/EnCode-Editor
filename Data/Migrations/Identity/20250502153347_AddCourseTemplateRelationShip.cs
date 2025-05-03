using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnapSaves.Data.Migrations.Identity
{
    /// <inheritdoc />
    public partial class AddCourseTemplateRelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LtiUsers_AspNetUsers_UserId",
                table: "LtiUsers");

            migrationBuilder.DropIndex(
                name: "IX_LtiUsers_UserId",
                table: "LtiUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LtiUsers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "LtiUsers",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CourseTemplate",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TemplateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTemplate", x => new { x.CourseId, x.TemplateId });
                    table.ForeignKey(
                        name: "FK_CourseTemplate_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTemplate_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LtiUsers_AppUserId",
                table: "LtiUsers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTemplate_TemplateId",
                table: "CourseTemplate",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_LtiUsers_AspNetUsers_AppUserId",
                table: "LtiUsers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LtiUsers_AspNetUsers_AppUserId",
                table: "LtiUsers");

            migrationBuilder.DropTable(
                name: "CourseTemplate");

            migrationBuilder.DropTable(
                name: "Template");

            migrationBuilder.DropIndex(
                name: "IX_LtiUsers_AppUserId",
                table: "LtiUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "LtiUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LtiUsers",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LtiUsers_UserId",
                table: "LtiUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LtiUsers_AspNetUsers_UserId",
                table: "LtiUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
