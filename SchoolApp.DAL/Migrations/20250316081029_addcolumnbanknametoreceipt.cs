using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addcolumnbanknametoreceipt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "receipts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankName",
                table: "receipts");
        }
    }
}
