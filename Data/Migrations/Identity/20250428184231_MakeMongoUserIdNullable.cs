using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnapSaves.Data.Migrations.Identity
{
    /// <inheritdoc />
    public partial class MakeMongoUserIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MongoUserId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
