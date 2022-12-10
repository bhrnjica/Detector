using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Detector.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class updatecolumn1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "DetectorData",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "DetectorData");
        }
    }
}
