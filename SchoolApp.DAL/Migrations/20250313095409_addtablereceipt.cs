using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addtablereceipt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "receipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    ReceiptDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CLSCloth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CLSAcpt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CLSBakelite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CLSRegs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashCheque = table.Column<int>(type: "int", nullable: false),
                    ChequeNumber = table.Column<int>(type: "int", nullable: false),
                    ChequeDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiptBusFirstTremCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReceiptBusSecondTremCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receipts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "installmentReceipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiptId = table.Column<int>(type: "int", nullable: false),
                    CostInstallment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InstallmentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_installmentReceipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_installmentReceipts_receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "receipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_installmentReceipts_ReceiptId",
                table: "installmentReceipts",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_receipts_Code",
                table: "receipts",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "installmentReceipts");

            migrationBuilder.DropTable(
                name: "receipts");
        }
    }
}
