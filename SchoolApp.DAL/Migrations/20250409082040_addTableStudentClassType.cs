using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addTableStudentClassType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "studentsClassTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    BrithDate = table.Column<DateOnly>(type: "date", nullable: false),
                    BrithPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassTypeId = table.Column<int>(type: "int", nullable: false),
                    LastBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdvanceRepayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyInstallment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CLSCloth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CLSAcpt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CLSBakelite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CLSRegs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AreYouWantGoWithBusSchool = table.Column<bool>(type: "bit", nullable: false),
                    DirectionBus = table.Column<short>(type: "smallint", nullable: true),
                    BusId = table.Column<int>(type: "int", nullable: true),
                    CostFirstTermBeforeDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostSecondTermBeforeDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GeneralDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployeeDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EarlyPaymentDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SiblingsDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CommunityFundDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SpecialDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BusDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostFirstTermAfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostSecondTermAfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCostAfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReceiptTotalFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReceiptTotalPayments = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RemainingFees = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RegistrationDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studentsClassTypes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "studentsClassTypes");
        }
    }
}
