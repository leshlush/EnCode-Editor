using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnapSaves.Data.Migrations.Identity
{
    /// <inheritdoc />
    public partial class LearningPathAndItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "organizations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 17, 19, 13, 4, 943, DateTimeKind.Utc).AddTicks(3457),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 7, 15, 17, 57, 18, 93, DateTimeKind.Utc).AddTicks(4771));

            migrationBuilder.CreateTable(
                name: "learningitems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    TemplateId = table.Column<int>(type: "int", nullable: true),
                    PreviousItemId = table.Column<int>(type: "int", nullable: true),
                    NextItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_learningitems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_learningitems_learningitems_NextItemId",
                        column: x => x.NextItemId,
                        principalTable: "learningitems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_learningitems_learningitems_PreviousItemId",
                        column: x => x.PreviousItemId,
                        principalTable: "learningitems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_learningitems_templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "learningpaths",
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
                    table.PrimaryKey("PK_learningpaths", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "learningpathitems",
                columns: table => new
                {
                    LearningPathId = table.Column<int>(type: "int", nullable: false),
                    LearningItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_learningpathitems", x => new { x.LearningPathId, x.LearningItemId });
                    table.ForeignKey(
                        name: "FK_learningpathitems_learningitems_LearningItemId",
                        column: x => x.LearningItemId,
                        principalTable: "learningitems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_learningpathitems_learningpaths_LearningPathId",
                        column: x => x.LearningPathId,
                        principalTable: "learningpaths",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_learningitems_NextItemId",
                table: "learningitems",
                column: "NextItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_learningitems_PreviousItemId",
                table: "learningitems",
                column: "PreviousItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_learningitems_TemplateId",
                table: "learningitems",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_learningpathitems_LearningItemId",
                table: "learningpathitems",
                column: "LearningItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "learningpathitems");

            migrationBuilder.DropTable(
                name: "learningitems");

            migrationBuilder.DropTable(
                name: "learningpaths");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "organizations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 15, 17, 57, 18, 93, DateTimeKind.Utc).AddTicks(4771),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 7, 17, 19, 13, 4, 943, DateTimeKind.Utc).AddTicks(3457));
        }
    }
}
