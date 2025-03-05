using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.Models
{
    public class Student
    {
        [Key]
       public int Id { get; set; }
        public int StudentNumber { get; set; } 
        public string StudentName { get; set; }
        public string FatherName {  get; set; }
        public string GrandFatherName {  get; set; }
        public string FamilyName {  get; set; }
        public int DepartmentId {  get; set; }
        public int StageId {  get; set; }
        public DateOnly BrithDate { get; set; }
        public string BrithPlace {  get; set; }
        public int NationalId {  get; set; }
        public short GanderType {  get; set; }
        public int ClassTypeId {  get; set; }
        public string ClassTypeName {  get; set; }
        public decimal LastBalance { get; set; } = 0;
        public string? LastSchoolName {  get; set; }
        public decimal AdvanceRepayment {  get; set; } = 0;
        public decimal MonthlyInstallment {  get; set; } = 0;
        public short PersonalStatusStudentId { get; set; }
        public string PersonalStatusStudentNumber { get;set; }
        public DateOnly PersonalStatusStudentStartDate {  get; set; }
        public DateOnly PersonalStatusStudentEndDate {  get; set; }
        public string PersonalStatusStudentPlace {  get; set; }
        public short PaymentTypeId {  get; set; }
        public short EnrollmentPeriodId {  get; set; }//فترة الالتحاق
        public string GuardianName {  get; set; }
        public string Kinship { get;set; }
        public int GuardianNationalId {  get; set; }
        public string? GuardianWork {  get; set; }
        public string GuardianJob {  get; set; }
        public string GuardianMobile {  get; set; }
        public string? GuardianEmail {  get; set; }  
        public string? GuardianZipCode {  get; set; }
        public string? GuardianWorkMobile { get; set; }
        public string? NameOfClosestPerson { get; set; }
        public string? HouseLocationGuardian { get; set; }
        public string? HouseMobileGuardian { get; set; }
        public string? GuardianFaxNumber {  get; set; }
        public short GuardianWorkingWithUs {  get; set; }
        public short PersonalStatusGuardianId { get; set; }
        public string PersonalStatusGuardianNumber { get; set; }
        public DateOnly PersonalStatusGuardianStartDate { get; set; }
        public DateOnly PersonalStatusGuardianEndDate { get; set; }
        public string PersonalStatusGuardianPlace { get; set; }
        public string MotherStudentName { get; set; }

        public string KinshipMother { get; set; }
        public int MotherNationalId { get; set; }
        public string? MotherWork { get; set; }
        public string? MotherFaxNumber { get; set; }
        public string? MotherWorkMobile { get; set; }
        public string MotherJob { get; set; }
        public string MotherMobilePhone { get; set; }
        public string? Notes { get; set; }
        public decimal CLSCloth { get;set; } = 0;
        public decimal CLSAcpt { get;set; } = 0;
        public decimal CLSBakelite { get;set; } = 0;
        public decimal CLSRegs { get;set; } = 0;
        public bool AreYouWantGoWithBusSchool { get; set; }
        public short? DirectionBus { get;set; }
        public int? BusId { get; set; }
        public decimal CostFirstTermBeforeDiscount { get; set; } = 0;
        public decimal CostSecondTermBeforeDiscount { get; set; } = 0;
        public decimal GeneralDiscount { get; set; } = 0;
        public decimal EmployeeDiscount { get; set; } = 0;
        public decimal EarlyPaymentDiscount { get; set; } = 0;
        public decimal SiblingsDiscount { get; set; } = 0;
        public decimal CommunityFundDiscount { get; set; } = 0;
        public decimal SpecialDiscount { get; set; } = 0;
        public decimal TotalDiscount { get; set; } = 0;
        public decimal BusDiscount { get; set; } = 0;
        public decimal CostFirstTermAfterDiscount { get; set; } = 0;
        public decimal CostSecondTermAfterDiscount { get; set; } = 0;
        public decimal TotalCost { get; set;  }
        public decimal TotalCostAfterDiscount { get; set;  }
        public List<InstallmentCostBeforeDiscount> installmentCostBeforeDiscounts { get; set; } = new List<InstallmentCostBeforeDiscount>();
        public List<InstallmentCostAfterDiscount> installmentCostAfterDiscounts { get; set; } = new List<InstallmentCostAfterDiscount>();
    }
}
