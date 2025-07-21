using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnapSaves.Data.Migrations.Identity
{
    /// <inheritdoc />
    public partial class AddLessonEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_learningitems_templates_TemplateId",
                table: "learningitems");

            migrationBuilder.DropIndex(
                name: "IX_learningitems_TemplateId",
                table: "learningitems");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "organizations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 20, 0, 24, 47, 36, DateTimeKind.Utc).AddTicks(3982),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 7, 18, 13, 52, 24, 360, DateTimeKind.Utc).AddTicks(6673));

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "learningitems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Location = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lessons", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lessons");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "learningitems");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "organizations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 18, 13, 52, 24, 360, DateTimeKind.Utc).AddTicks(6673),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 7, 20, 0, 24, 47, 36, DateTimeKind.Utc).AddTicks(3982));

            migrationBuilder.CreateIndex(
                name: "IX_learningitems_TemplateId",
                table: "learningitems",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_learningitems_templates_TemplateId",
                table: "learningitems",
                column: "TemplateId",
                principalTable: "templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
