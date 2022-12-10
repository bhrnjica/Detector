using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Detector.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class removecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetectorData_DetectorDetails_DetectorDetailsId",
                table: "DetectorData");

            migrationBuilder.DropColumn(
                name: "DetectorId",
                table: "DetectorDetails");

            migrationBuilder.AlterColumn<int>(
                name: "DetectorDetailsId",
                table: "DetectorData",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DetectorData_DetectorDetails_DetectorDetailsId",
                table: "DetectorData",
                column: "DetectorDetailsId",
                principalTable: "DetectorDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetectorData_DetectorDetails_DetectorDetailsId",
                table: "DetectorData");

            migrationBuilder.AddColumn<int>(
                name: "DetectorId",
                table: "DetectorDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DetectorDetailsId",
                table: "DetectorData",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_DetectorData_DetectorDetails_DetectorDetailsId",
                table: "DetectorData",
                column: "DetectorDetailsId",
                principalTable: "DetectorDetails",
                principalColumn: "Id");
        }
    }
}
