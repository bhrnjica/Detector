using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Detector.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class DetectorData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetectorDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetectorDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetectorData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DetectorDetailsId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetectorData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetectorData_DetectorDetails_DetectorDetailsId",
                        column: x => x.DetectorDetailsId,
                        principalTable: "DetectorDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetectorData_DetectorDetailsId",
                table: "DetectorData",
                column: "DetectorDetailsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetectorData");

            migrationBuilder.DropTable(
                name: "DetectorDetails");
        }
    }
}
