using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolApp.DAL.Models;

namespace SchoolApp.DAL
{
    public class ApplicationDBContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) 
        { 
        }
        public DbSet<FileBus> FileBuses { get; set; }
        public DbSet<National> nationals { get; set; }
        public DbSet<Stage> stages { get; set; }    
        public DbSet<ClassTypeName> classTypeName {  get; set; }   
        public DbSet<Amount> amounts { get; set; }  
        public DbSet<ClassType> classTypes { get; set; }
        public DbSet<Installment> installments { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<InstallmentCostBeforeDiscount> installmentCostBeforeDiscounts { get; set; }
        public DbSet<InstallmentCostAfterDiscount> installmentCostAfterDiscounts { get; set; }
        public DbSet<Receipt> receipts { get; set; }    
        public DbSet<InstallmentReceipt> installmentReceipts { get; set; }
        public DbSet<Payment> payments { get; set; }   
        public DbSet<StudentsClassType> studentsClassTypes { get; set; }
        public DbSet<Company> companies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //var adminId = "c7c062f0-1784-4245-8630-1fc72bae7b26";
            //var hasher = new PasswordHasher<ApplicationUser>();

            //var adminUser = new ApplicationUser
            //{
            //    Id = adminId,
            //    UserName = "الحسابات",
            //    NormalizedUserName = "ADMIN",
            //    Email = "admin@school.com",
            //    NormalizedEmail = "ADMIN@SCHOOL.COM",
            //    EmailConfirmed = true,
            //    PhoneNumberConfirmed = false,
            //    UserNumber = 1,
            //    Level = "Admin",

            //    // صلاحيات
            //    CanAccessGrades = true,
            //    CanAccessStudentsFile = true,
            //    CanAccessBusesFile = true,
            //    CanAccessTypesEncoming = true,
            //    CanAccessReceipts = true,
            //    CanAccessPayments = true,
            //    CanAccessPrintReports = true,
            //    CanAccessSearch = true,
            //    CanAccessUsersFile = true,

            //    SecurityStamp = "EJBUZX7KWK3DCZVSOHHPYWIA7HBFVGGL",
            //    ConcurrencyStamp = "29acb43e-2f18-4c77-85eb-b9445de3e38f"
            //};

            //adminUser.PasswordHash = hasher.HashPassword(adminUser, "300100"); // بدل الباسوورد حسب رغبتك

            //modelBuilder.Entity<ApplicationUser>().HasData(adminUser);
            modelBuilder.Entity<National>()
            .HasIndex(n => n.code)
            .IsUnique();
            modelBuilder.Entity<FileBus>()
           .HasIndex(n => n.BusCode)
           .IsUnique();
           modelBuilder.Entity<Stage>()
           .HasIndex(n => n.StageCode)
           .IsUnique();
            modelBuilder.Entity<ClassTypeName>()
          .HasIndex(n => n.Code)
          .IsUnique();
            modelBuilder.Entity<Installment>()
         .HasIndex(n => n.Code)
         .IsUnique();
            modelBuilder.Entity<Department>()
        .HasIndex(n => n.Code)
        .IsUnique();
            modelBuilder.Entity<Student>()
       .HasIndex(n => n.StudentNumber)
       .IsUnique();
            modelBuilder.Entity<Receipt>()
      .HasIndex(n => n.Code)
      .IsUnique();
            modelBuilder.Entity<Payment>()
      .HasIndex(n => n.Code)
      .IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}
