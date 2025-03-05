using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addcolumntotalcostdiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "GuardianWorkingWithUs",
                table: "students",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCostAfterDiscount",
                table: "students",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCostAfterDiscount",
                table: "students");

            migrationBuilder.AlterColumn<bool>(
                name: "GuardianWorkingWithUs",
                table: "students",
                type: "bit",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }
    }
}
