using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnapSaves.Data.Migrations.Identity
{
    /// <inheritdoc />
    public partial class FixLearningItemColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "organizations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 17, 19, 44, 13, 783, DateTimeKind.Utc).AddTicks(2474),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 7, 17, 19, 13, 4, 943, DateTimeKind.Utc).AddTicks(3457));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "learningitems",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "learningitems");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "organizations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 17, 19, 13, 4, 943, DateTimeKind.Utc).AddTicks(3457),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 7, 17, 19, 44, 13, 783, DateTimeKind.Utc).AddTicks(2474));
        }
    }
}
