using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationUserPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CanAccessBusesFile", "CanAccessGrades", "CanAccessPayments", "CanAccessPrintReports", "CanAccessReceipts", "CanAccessSearch", "CanAccessStudentsFile", "CanAccessTypesEncoming", "CanAccessUsersFile", "ConcurrencyStamp", "Email", "EmailConfirmed", "Level", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserNumber" },
                values: new object[] { "c7c062f0-1784-4245-8630-1fc72bae7b26", 0, true, true, true, true, true, true, true, true, true, "29acb43e-2f18-4c77-85eb-b9445de3e38f", "admin@school.com", true, "Admin", false, null, "ADMIN@SCHOOL.COM", "ADMIN", "AQAAAAIAAYagAAAAEMaWRNIfksxPoDv+RanvDHvPXtM9LmbFzVPrlQbi1ynTAyYt+o1avSQDXP1mzEiuvQ==", null, false, "EJBUZX7KWK3DCZVSOHHPYWIA7HBFVGGL", false, "الحسابات", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c7c062f0-1784-4245-8630-1fc72bae7b26");
        }
    }
}
