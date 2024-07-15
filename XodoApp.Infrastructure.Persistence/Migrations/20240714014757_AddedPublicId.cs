using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XodoApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedPublicId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "VehicleImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "VehicleImages");
        }
    }
}
