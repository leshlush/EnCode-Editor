using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnapSaves.Data.Migrations.Identity
{
    /// <inheritdoc />
    public partial class AddCompositeKeyToUserCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MongoId",
                table: "Template",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MongoId",
                table: "Template");
        }
    }
}
