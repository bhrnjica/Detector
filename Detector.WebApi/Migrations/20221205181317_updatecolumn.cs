using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Detector.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class updatecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DetectorId",
                table: "DetectorDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DetectorId",
                table: "DetectorDetails");
        }
    }
}
