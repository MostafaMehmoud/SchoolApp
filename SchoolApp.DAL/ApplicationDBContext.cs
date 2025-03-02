using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolApp.DAL.Models;

namespace SchoolApp.DAL
{
    public class ApplicationDBContext:DbContext
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            base.OnModelCreating(modelBuilder);
        }
    }
}
