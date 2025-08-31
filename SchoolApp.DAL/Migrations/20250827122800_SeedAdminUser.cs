using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using SchoolApp.DAL.Models;

#nullable disable

namespace SchoolApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ثانياً: نضيف مستخدم Admin افتراضي
            var hasher = new PasswordHasher<ApplicationUser>();
            var adminId = Guid.NewGuid().ToString();

            var admin = new ApplicationUser
            {
                Id = adminId,
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@school.com",
                NormalizedEmail = "ADMIN@SCHOOL.COM",
                EmailConfirmed = true,
                UserNumber = 1,
                Level = "Admin",
                CanAccessGrades = true,
                CanAccessStudentsFile = true,
                CanAccessBusesFile = true,
                CanAccessTypesEncoming = true,
                CanAccessReceipts = true,
                CanAccessPayments = true,
                CanAccessPrintReports = true,
                CanAccessSearch = true,
                CanAccessUsersFile = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                LockoutEnabled = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                AccessFailedCount = 0
            };

            // الباسورد: 300100
            admin.PasswordHash = hasher.HashPassword(admin, "300100");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[]
                {
                    "Id","UserName","NormalizedUserName","Email","NormalizedEmail",
                    "EmailConfirmed","PasswordHash","SecurityStamp","ConcurrencyStamp",
                    "UserNumber","Level",
                    "CanAccessGrades","CanAccessStudentsFile","CanAccessBusesFile","CanAccessTypesEncoming",
                    "CanAccessReceipts","CanAccessPayments","CanAccessPrintReports",
                    "CanAccessSearch","CanAccessUsersFile",
                    "LockoutEnabled","PhoneNumberConfirmed","TwoFactorEnabled","AccessFailedCount"
                },
                values: new object[]
                {
                    admin.Id, admin.UserName, admin.NormalizedUserName, admin.Email, admin.NormalizedEmail,
                    admin.EmailConfirmed, admin.PasswordHash, admin.SecurityStamp, admin.ConcurrencyStamp,
                    admin.UserNumber, admin.Level,
                    admin.CanAccessGrades, admin.CanAccessStudentsFile, admin.CanAccessBusesFile, admin.CanAccessTypesEncoming,
                    admin.CanAccessReceipts, admin.CanAccessPayments, admin.CanAccessPrintReports,
                    admin.CanAccessSearch, admin.CanAccessUsersFile,
                    admin.LockoutEnabled, admin.PhoneNumberConfirmed, admin.TwoFactorEnabled, admin.AccessFailedCount
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
