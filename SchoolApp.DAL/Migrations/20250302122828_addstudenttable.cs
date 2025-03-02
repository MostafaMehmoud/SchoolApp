using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addstudenttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentNumber = table.Column<int>(type: "int", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrandFatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    BrithDate = table.Column<DateOnly>(type: "date", nullable: false),
                    BrithPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalId = table.Column<int>(type: "int", nullable: false),
                    GanderType = table.Column<short>(type: "smallint", nullable: false),
                    ClassTypeId = table.Column<int>(type: "int", nullable: false),
                    ClassTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastSchoolName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdvanceRepayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyInstallment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PersonalStatusStudentId = table.Column<short>(type: "smallint", nullable: false),
                    PersonalStatusStudentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalStatusStudentStartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PersonalStatusStudentEndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PersonalStatusStudentPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentTypeId = table.Column<short>(type: "smallint", nullable: false),
                    EnrollmentPeriodId = table.Column<short>(type: "smallint", nullable: false),
                    GuardianName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kinship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuardianNationalId = table.Column<int>(type: "int", nullable: false),
                    GuardianWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuardianJob = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuardianMobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuardianEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuardianZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuardianWorkMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameOfClosestPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseLocationGuardian = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HouseMobileGuardian = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuardianFaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuardianWorkingWithUs = table.Column<bool>(type: "bit", nullable: false),
                    PersonalStatusGuardianId = table.Column<short>(type: "smallint", nullable: false),
                    PersonalStatusGuardianNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalStatusGuardianStartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PersonalStatusGuardianEndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PersonalStatusGuardianPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherStudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KinshipMother = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherNationalId = table.Column<int>(type: "int", nullable: false),
                    MotherWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherFaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherWorkMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherJob = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherMobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CLSCloth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CLSAcpt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CLSBakelite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CLSRegs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AreYouWantGoWithBusSchool = table.Column<bool>(type: "bit", nullable: false),
                    DirectionBus = table.Column<short>(type: "smallint", nullable: true),
                    BusId = table.Column<int>(type: "int", nullable: true),
                    CostFirstTerm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostSecondTerm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "installmentCostAfterDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    ClassTypeId = table.Column<int>(type: "int", nullable: false),
                    InstallmentId = table.Column<int>(type: "int", nullable: false),
                    CostInstallment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_installmentCostAfterDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_installmentCostAfterDiscounts_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "installmentCostBeforeDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StageId = table.Column<int>(type: "int", nullable: false),
                    ClassTypeId = table.Column<int>(type: "int", nullable: false),
                    InstallmentId = table.Column<int>(type: "int", nullable: false),
                    CostInstallment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_installmentCostBeforeDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_installmentCostBeforeDiscounts_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_installmentCostAfterDiscounts_StudentId",
                table: "installmentCostAfterDiscounts",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_installmentCostBeforeDiscounts_StudentId",
                table: "installmentCostBeforeDiscounts",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_students_StudentNumber",
                table: "students",
                column: "StudentNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "installmentCostAfterDiscounts");

            migrationBuilder.DropTable(
                name: "installmentCostBeforeDiscounts");

            migrationBuilder.DropTable(
                name: "students");
        }
    }
}
