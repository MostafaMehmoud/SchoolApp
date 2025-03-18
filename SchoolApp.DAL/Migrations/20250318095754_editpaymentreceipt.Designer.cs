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
    [Migration("20250318095754_editpaymentreceipt")]
    partial class editpaymentreceipt
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

                    b.Property<int>("ClassTypeNameId")
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

                    b.Property<decimal>("CLSAcpt")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CLSBakelite")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CLSCloth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CLSRegs")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<DateTime>("CurrentDateClassType")
                        .HasColumnType("datetime2");

                    b.Property<int>("StageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

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

            modelBuilder.Entity("SchoolApp.DAL.Models.Department", b =>
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

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("departments");
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

            modelBuilder.Entity("SchoolApp.DAL.Models.Installment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("InstallName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("StageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("installments");
                });

            modelBuilder.Entity("SchoolApp.DAL.Models.InstallmentCostAfterDiscount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("CostInstallment")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("InstallmentId")
                        .HasColumnType("int");

                    b.Property<string>("InstallmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StageId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("installmentCostAfterDiscounts");
                });

            modelBuilder.Entity("SchoolApp.DAL.Models.InstallmentCostBeforeDiscount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("CostInstallment")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("InstallmentId")
                        .HasColumnType("int");

                    b.Property<string>("InstallmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StageId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("installmentCostBeforeDiscounts");
                });

            modelBuilder.Entity("SchoolApp.DAL.Models.InstallmentReceipt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("CostInstallment")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("InstallmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReceiptId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReceiptId");

                    b.ToTable("installmentReceipts");
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

            modelBuilder.Entity("SchoolApp.DAL.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("AmountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BankName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CashCheque")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("ChequeDate")
                        .HasColumnType("date");

                    b.Property<int?>("ChequeNumber")
                        .HasColumnType("int");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<DateOnly>("PaymentDate")
                        .HasColumnType("date");

                    b.Property<string>("Purpose")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("payments");
                });

            modelBuilder.Entity("SchoolApp.DAL.Models.Receipt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("AmountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BankName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CLSAcpt")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CLSBakelite")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CLSCloth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CLSRegs")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CashCheque")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("ChequeDate")
                        .HasColumnType("date");

                    b.Property<int?>("ChequeNumber")
                        .HasColumnType("int");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Purpose")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ReceiptBusFirstTremCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ReceiptBusSecondTremCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateOnly>("ReceiptDate")
                        .HasColumnType("date");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("receipts");
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

            modelBuilder.Entity("SchoolApp.DAL.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AdvanceRepayment")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("AreYouWantGoWithBusSchool")
                        .HasColumnType("bit");

                    b.Property<DateOnly>("BrithDate")
                        .HasColumnType("date");

                    b.Property<string>("BrithPlace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("BusDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("BusId")
                        .HasColumnType("int");

                    b.Property<decimal>("CLSAcpt")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CLSBakelite")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CLSCloth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CLSRegs")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ClassTypeId")
                        .HasColumnType("int");

                    b.Property<string>("ClassTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("CommunityFundDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CostFirstTermAfterDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CostFirstTermBeforeDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CostSecondTermAfterDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CostSecondTermBeforeDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<short?>("DirectionBus")
                        .HasColumnType("smallint");

                    b.Property<decimal>("EarlyPaymentDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("EmployeeDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<short>("EnrollmentPeriodId")
                        .HasColumnType("smallint");

                    b.Property<string>("FamilyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("GanderType")
                        .HasColumnType("smallint");

                    b.Property<decimal>("GeneralDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("GrandFatherName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuardianEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuardianFaxNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuardianJob")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuardianMobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuardianName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GuardianNationalId")
                        .HasColumnType("int");

                    b.Property<string>("GuardianWork")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuardianWorkMobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("GuardianWorkingWithUs")
                        .HasColumnType("smallint");

                    b.Property<string>("GuardianZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HouseLocationGuardian")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HouseMobileGuardian")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kinship")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KinshipMother")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("LastBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("LastSchoolName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("MonthlyInstallment")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MotherFaxNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherJob")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherMobilePhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MotherNationalId")
                        .HasColumnType("int");

                    b.Property<string>("MotherStudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherWork")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherWorkMobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameOfClosestPerson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NationalId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("PaymentTypeId")
                        .HasColumnType("smallint");

                    b.Property<DateOnly>("PersonalStatusGuardianEndDate")
                        .HasColumnType("date");

                    b.Property<short>("PersonalStatusGuardianId")
                        .HasColumnType("smallint");

                    b.Property<string>("PersonalStatusGuardianNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalStatusGuardianPlace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("PersonalStatusGuardianStartDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("PersonalStatusStudentEndDate")
                        .HasColumnType("date");

                    b.Property<short>("PersonalStatusStudentId")
                        .HasColumnType("smallint");

                    b.Property<string>("PersonalStatusStudentNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalStatusStudentPlace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("PersonalStatusStudentStartDate")
                        .HasColumnType("date");

                    b.Property<decimal>("ReceiptTotalFees")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ReceiptTotalPayments")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RemainingFees")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SiblingsDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SpecialDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StageId")
                        .HasColumnType("int");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentNumber")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalCostAfterDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalDiscount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("StudentNumber")
                        .IsUnique();

                    b.ToTable("students");
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

            modelBuilder.Entity("SchoolApp.DAL.Models.InstallmentCostAfterDiscount", b =>
                {
                    b.HasOne("SchoolApp.DAL.Models.Student", null)
                        .WithMany("installmentCostAfterDiscounts")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SchoolApp.DAL.Models.InstallmentCostBeforeDiscount", b =>
                {
                    b.HasOne("SchoolApp.DAL.Models.Student", null)
                        .WithMany("installmentCostBeforeDiscounts")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SchoolApp.DAL.Models.InstallmentReceipt", b =>
                {
                    b.HasOne("SchoolApp.DAL.Models.Receipt", null)
                        .WithMany("installmentReceipts")
                        .HasForeignKey("ReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SchoolApp.DAL.Models.ClassType", b =>
                {
                    b.Navigation("Amounts");
                });

            modelBuilder.Entity("SchoolApp.DAL.Models.Receipt", b =>
                {
                    b.Navigation("installmentReceipts");
                });

            modelBuilder.Entity("SchoolApp.DAL.Models.Student", b =>
                {
                    b.Navigation("installmentCostAfterDiscounts");

                    b.Navigation("installmentCostBeforeDiscounts");
                });
#pragma warning restore 612, 618
        }
    }
}
