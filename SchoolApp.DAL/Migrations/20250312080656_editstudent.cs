using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class editstudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ReceiptTotalFees",
                table: "students",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ReceiptTotalPayments",
                table: "students",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RemainingFees",
                table: "students",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiptTotalFees",
                table: "students");

            migrationBuilder.DropColumn(
                name: "ReceiptTotalPayments",
                table: "students");

            migrationBuilder.DropColumn(
                name: "RemainingFees",
                table: "students");
        }
    }
}
