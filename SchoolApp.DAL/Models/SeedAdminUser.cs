using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace SchoolApp.DAL.Models
{
    public static class SeedAdminUser
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // إذا ما فيه مستخدم Admin، نضيفه
            if (await userManager.FindByNameAsync("Admin") == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@example.com",
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
                    CanAccessUsersFile = true
                };

                var result = await userManager.CreateAsync(adminUser, "300100");
                if (!result.Succeeded)
                {
                    throw new Exception("فشل إنشاء حساب Admin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}