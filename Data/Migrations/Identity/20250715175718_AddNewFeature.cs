using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnapSaves.Data.Migrations.Identity
{
    /// <inheritdoc />
    public partial class AddNewFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAnonymous",
                table: "projectsharelinks",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastAccessedAt",
                table: "projectsharelinks",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TemplateId",
                table: "projectsharelinks",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "organizations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 15, 17, 57, 18, 93, DateTimeKind.Utc).AddTicks(4771),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 7, 15, 1, 33, 33, 569, DateTimeKind.Utc).AddTicks(6762));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAnonymous",
                table: "projectsharelinks");

            migrationBuilder.DropColumn(
                name: "LastAccessedAt",
                table: "projectsharelinks");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "projectsharelinks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "organizations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 15, 1, 33, 33, 569, DateTimeKind.Utc).AddTicks(6762),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 7, 15, 17, 57, 18, 93, DateTimeKind.Utc).AddTicks(4771));
        }
    }
}
