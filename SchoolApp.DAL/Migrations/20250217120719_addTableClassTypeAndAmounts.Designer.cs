﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolApp.DAL;

#nullable disable

namespace SchoolApp.DAL.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20250217120719_addTableClassTypeAndAmounts")]
    partial class addTableClassTypeAndAmounts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SchoolApp.DAL.Models.Amount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AmountDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("AmountPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ClassTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClassTypeId");

                    b.ToTable("amounts");
                });

            modelBuilder.Entity("SchoolApp.DAL.Models.ClassType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("CLSAmountAdvs")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CLSAmountBook")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CLSAmountBus")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CLSAmountInst")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<int>("StageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StageId");

                    b.ToTable("classTypes");
                });

            modelBuilder.Entity("SchoolApp.DAL.Models.ClassTypeName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("classTypeName");
                });

            modelBuilder.Entity("SchoolApp.DAL.Models.FileBus", b =>
                {
                    b.Property<int>("BusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BusId"));

                    b.Property<decimal>("BusAll")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("BusCode")
                        .HasColumnType("int");

                    b.Property<string>("BusDrive")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<decimal>("BusGo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("BusNumber")
                        .HasColumnType("int");

                    b.Property<string>("BusPlate")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<decimal>("BusReturn")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("BusRoute")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("BusState")
                        .HasColumnType("int");

                    b.Property<int?>("BusStud")
                        .HasColumnType("int");

                    b.Property<string>("BusSup")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("MobilPhone1")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("MobilPhone2")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("BusId");

                    b.HasIndex("BusCode")
                        .IsUnique();

                    b.ToTable("FileBuses");
                });

            modelBuilder.Entity("SchoolApp.DAL.Models.National", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("code")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("code")
                        .IsUnique();

                    b.ToTable("nationals");
                });

            modelBuilder.Entity("SchoolApp.DAL.Models.Stage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("StageCode")
                        .HasColumnType("int");

                    b.Property<string>("StageName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("StageCode")
                        .IsUnique();

                    b.ToTable("stages");
                });

            modelBuilder.Entity("SchoolApp.DAL.Models.Amount", b =>
                {
                    b.HasOne("SchoolApp.DAL.Models.ClassType", "ClassType")
                        .WithMany("Amounts")
                        .HasForeignKey("ClassTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassType");
                });

            modelBuilder.Entity("SchoolApp.DAL.Models.ClassType", b =>
                {
                    b.HasOne("SchoolApp.DAL.Models.Stage", "Stage")
                        .WithMany()
                        .HasForeignKey("StageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stage");
                });

            modelBuilder.Entity("SchoolApp.DAL.Models.ClassType", b =>
                {
                    b.Navigation("Amounts");
                });
#pragma warning restore 612, 618
        }
    }
}
