using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnapSaves.Data.Migrations.Identity
{
    /// <inheritdoc />
    public partial class AddOrganizationTypeAndCreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "organizations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 15, 1, 33, 33, 569, DateTimeKind.Utc).AddTicks(6762));

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "organizations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "organizations");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "organizations");
        }
    }
}
