using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class companytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c7c062f0-1784-4245-8630-1fc72bae7b26");

            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BulidingAddressNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BulidingAddressAddtionalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NeighborhoodAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommercialRegister = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstablishedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "companies");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CanAccessBusesFile", "CanAccessGrades", "CanAccessPayments", "CanAccessPrintReports", "CanAccessReceipts", "CanAccessSearch", "CanAccessStudentsFile", "CanAccessTypesEncoming", "CanAccessUsersFile", "ConcurrencyStamp", "Email", "EmailConfirmed", "Level", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserNumber" },
                values: new object[] { "c7c062f0-1784-4245-8630-1fc72bae7b26", 0, true, true, true, true, true, true, true, true, true, "29acb43e-2f18-4c77-85eb-b9445de3e38f", "admin@school.com", true, "Admin", false, null, "ADMIN@SCHOOL.COM", "ADMIN", "AQAAAAIAAYagAAAAEMaWRNIfksxPoDv+RanvDHvPXtM9LmbFzVPrlQbi1ynTAyYt+o1avSQDXP1mzEiuvQ==", null, false, "EJBUZX7KWK3DCZVSOHHPYWIA7HBFVGGL", false, "الحسابات", 1 });
        }
    }
}
