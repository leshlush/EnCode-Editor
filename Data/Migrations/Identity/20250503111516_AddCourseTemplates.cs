using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnapSaves.Data.Migrations.Identity
{
    /// <inheritdoc />
    public partial class AddCourseTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTemplate_Courses_CourseId",
                table: "CourseTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseTemplate_Template_TemplateId",
                table: "CourseTemplate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Template",
                table: "Template");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseTemplate",
                table: "CourseTemplate");

            migrationBuilder.RenameTable(
                name: "Template",
                newName: "Templates");

            migrationBuilder.RenameTable(
                name: "CourseTemplate",
                newName: "CourseTemplates");

            migrationBuilder.RenameIndex(
                name: "IX_CourseTemplate_TemplateId",
                table: "CourseTemplates",
                newName: "IX_CourseTemplates_TemplateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Templates",
                table: "Templates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseTemplates",
                table: "CourseTemplates",
                columns: new[] { "CourseId", "TemplateId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTemplates_Courses_CourseId",
                table: "CourseTemplates",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTemplates_Templates_TemplateId",
                table: "CourseTemplates",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTemplates_Courses_CourseId",
                table: "CourseTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseTemplates_Templates_TemplateId",
                table: "CourseTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Templates",
                table: "Templates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseTemplates",
                table: "CourseTemplates");

            migrationBuilder.RenameTable(
                name: "Templates",
                newName: "Template");

            migrationBuilder.RenameTable(
                name: "CourseTemplates",
                newName: "CourseTemplate");

            migrationBuilder.RenameIndex(
                name: "IX_CourseTemplates_TemplateId",
                table: "CourseTemplate",
                newName: "IX_CourseTemplate_TemplateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Template",
                table: "Template",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseTemplate",
                table: "CourseTemplate",
                columns: new[] { "CourseId", "TemplateId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTemplate_Courses_CourseId",
                table: "CourseTemplate",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTemplate_Template_TemplateId",
                table: "CourseTemplate",
                column: "TemplateId",
                principalTable: "Template",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
