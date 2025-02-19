using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class editTableClassType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CLSAmountAdvs",
                table: "classTypes");

            migrationBuilder.RenameColumn(
                name: "CLSAmountInst",
                table: "classTypes",
                newName: "CLSRegs");

            migrationBuilder.RenameColumn(
                name: "CLSAmountBus",
                table: "classTypes",
                newName: "CLSCloth");

            migrationBuilder.RenameColumn(
                name: "CLSAmountBook",
                table: "classTypes",
                newName: "CLSAcpt");

            migrationBuilder.AddColumn<int>(
                name: "ClassTypeNameId",
                table: "amounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassTypeNameId",
                table: "amounts");

            migrationBuilder.RenameColumn(
                name: "CLSRegs",
                table: "classTypes",
                newName: "CLSAmountInst");

            migrationBuilder.RenameColumn(
                name: "CLSCloth",
                table: "classTypes",
                newName: "CLSAmountBus");

            migrationBuilder.RenameColumn(
                name: "CLSAcpt",
                table: "classTypes",
                newName: "CLSAmountBook");

            migrationBuilder.AddColumn<decimal>(
                name: "CLSAmountAdvs",
                table: "classTypes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
