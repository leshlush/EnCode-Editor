using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnapSaves.Data.Migrations.Identity
{
    /// <inheritdoc />
    public partial class SortPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_learningitems_learningitems_NextItemId",
                table: "learningitems");

            migrationBuilder.DropForeignKey(
                name: "FK_learningitems_learningitems_PreviousItemId",
                table: "learningitems");

            migrationBuilder.DropIndex(
                name: "IX_learningitems_NextItemId",
                table: "learningitems");

            migrationBuilder.DropIndex(
                name: "IX_learningitems_PreviousItemId",
                table: "learningitems");

            migrationBuilder.DropColumn(
                name: "NextItemId",
                table: "learningitems");

            migrationBuilder.DropColumn(
                name: "PreviousItemId",
                table: "learningitems");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "organizations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 18, 0, 42, 16, 223, DateTimeKind.Utc).AddTicks(6537),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 7, 17, 19, 44, 13, 783, DateTimeKind.Utc).AddTicks(2474));

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "learningitems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "learningitems");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "organizations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 7, 17, 19, 44, 13, 783, DateTimeKind.Utc).AddTicks(2474),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 7, 18, 0, 42, 16, 223, DateTimeKind.Utc).AddTicks(6537));

            migrationBuilder.AddColumn<int>(
                name: "NextItemId",
                table: "learningitems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreviousItemId",
                table: "learningitems",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_learningitems_learningitems_NextItemId",
                table: "learningitems",
                column: "NextItemId",
                principalTable: "learningitems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_learningitems_learningitems_PreviousItemId",
                table: "learningitems",
                column: "PreviousItemId",
                principalTable: "learningitems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
