using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class edittablestudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostFirstTerm",
                table: "students");

            migrationBuilder.DropColumn(
                name: "CostSecondTerm",
                table: "students");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CostFirstTerm",
                table: "students",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CostSecondTerm",
                table: "students",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
