using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class editcolumnAddValueTax : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdvanceRepayment",
                table: "studentsClassTypes",
                newName: "ValueAddedTax");

            migrationBuilder.RenameColumn(
                name: "AdvanceRepayment",
                table: "students",
                newName: "ValueAddedTax");

            migrationBuilder.AlterColumn<string>(
                name: "TaxNumber",
                table: "companies",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "companies",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValueAddedTax",
                table: "studentsClassTypes",
                newName: "AdvanceRepayment");

            migrationBuilder.RenameColumn(
                name: "ValueAddedTax",
                table: "students",
                newName: "AdvanceRepayment");

            migrationBuilder.AlterColumn<string>(
                name: "TaxNumber",
                table: "companies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "companies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
