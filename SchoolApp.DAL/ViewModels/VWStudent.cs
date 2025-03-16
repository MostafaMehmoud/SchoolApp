using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;

namespace SchoolApp.DAL.ViewModels
{
    public class VWStudent
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "كود الطالب مطلوب")]
       
        public int StudentNumber { get; set; }
        [Required(ErrorMessage ="اسم الطالب مطلوب")]
        [MaxLength(30,ErrorMessage ="الحد الاقصي لاسم الطالب  هو 30")]
        public string StudentName { get; set; }
        [Required(ErrorMessage = "اسم الاب مطلوب")]
        [MaxLength(30, ErrorMessage = "الحد الاقصي لاسم الاب  هو 30")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "اسم الجد مطلوب")]
        [MaxLength(30, ErrorMessage = "الحد الاقصي لاسم الجد  هو 30")]
        public string GrandFatherName { get; set; }
        [Required(ErrorMessage = "اسم العائلة مطلوب")]
        [MaxLength(30, ErrorMessage = "الحد الاقصي لاسم العائلة  هو 30")]
        public string FamilyName { get; set; }
        [Required(ErrorMessage = "رجا اختر القسم")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "رجا اختر المرحلة")]
        public int StageId { get; set; }
        [Required(ErrorMessage = "تاريخ الميلاد مطلوب")]
        public DateOnly? BrithDate { get; set; }
        [Required(ErrorMessage = "   مكان الميلاد مطلوب")]
        [MaxLength(100, ErrorMessage = "الحد الاقصي لمكان الميلاد  هو 100")]

        public string BrithPlace { get; set; }
        [Required(ErrorMessage = "رجا اختر جنسية الطالب")]
        public int NationalId { get; set; }
        [Required(ErrorMessage = "رجا اختر نوع الطالب")]
        public short GanderType { get; set; }
        [Required(ErrorMessage = "رجا اختر الصف")]
        public int ClassTypeId { get; set; }
        [Required(ErrorMessage = "    اسم الصف مطلوب")]
        [MaxLength(30, ErrorMessage = "الحد الاقصي لاسم الصف   هو 30")]

        public string ClassTypeName { get; set; }
        public decimal LastBalance { get; set; } = 0;
        public string? LastSchoolName { get; set; }
        public decimal AdvanceRepayment { get; set; } = 0;
        public decimal MonthlyInstallment { get; set; } = 0;
        [Required(ErrorMessage = "رجا الحالة الشخصية لطالب ")]
        public short PersonalStatusStudentId { get; set; }
        [Required(ErrorMessage = " رقم الحالة الشخصية لطالب مطلوب")]
        [MaxLength(20, ErrorMessage = "الحد الاقصي لرقم الحالة الشخصية لطالب هو 20")]

        public string PersonalStatusStudentNumber { get; set; }
        [Required(ErrorMessage = "تاريخ اصدار الحالة الشخصية لطالب مطلوب")]
        public DateOnly? PersonalStatusStudentStartDate { get; set; }
        [Required(ErrorMessage = "تاريخ انتهاء الحالة الشخصية لطالب مطلوب")]
        public DateOnly? PersonalStatusStudentEndDate { get; set; }
        [Required(ErrorMessage = " مكان اصدار الحالة الشخصية  لطالب مطلوب")]
        [MaxLength(30, ErrorMessage = "الحد الاقصي لاسم مكان اصدار الحالة الشخصية لطالب هو 30")]

        public string PersonalStatusStudentPlace { get; set; }
        [Required(ErrorMessage = " رجا اختر طريقة الدفع")]
        public short PaymentTypeId { get; set; }
        [Required(ErrorMessage = "رجا اختر فترة الالتحاق")]
        public short EnrollmentPeriodId { get; set; }//فترة الالتحاق
        [Required(ErrorMessage = "اسم ولي امر الطالب مطلوب")]
        [MaxLength(30, ErrorMessage = "الحد الاقصي لاسم ولي امر الطالب  هو 30")]

        public string GuardianName { get; set; }
        [Required(ErrorMessage = "صلة القرابة ولي امر  بالطالب مطلوب")]
        [MaxLength(30, ErrorMessage = "الحد الاقصي لاسم صلة القرابة ولي امر  بالطالب  هو 30")]

        public string Kinship { get; set; }
        [Required(ErrorMessage = "رجا اختر الحالة الشخصية لولي الامر ")]
        public int GuardianNationalId { get; set; }
        public string? GuardianSideWork { get; set; }
        [Required(ErrorMessage = " وظيفة عمل ولي الامر مطلوب")]
        [MaxLength(50, ErrorMessage = "الحد الاقصي لاسم وظيفة عمل ولي الامر  هو 50")]

        public string GuardianJob { get; set; }
        [Required(ErrorMessage = "رقم الجوال ولي امر  مطلوب")]
        [MaxLength(15, ErrorMessage = "الحد الاقصي لرقم الجوال ولي امر  هو 15")]

        public string GuardianMobile { get; set; }
        public string? GuardianEmail { get; set; }
        public string? GuardianZipCode { get; set; }
        public string? GuardianWorkMobile { get; set; }
        public string? NameOfClosestPerson { get; set; }
        public string? HouseLocationGuardian { get; set; }
        public string? HouseMobileGuardian { get; set; }
        public string? GuardianFaxNumber { get; set; }
       
      

        public short GuardianWorkingWithUs { get; set; }
        
        [Required(ErrorMessage = " رجا الحالة الشخصية لولي الامر")]
        public short PersonalStatusGuardianId { get; set; }
        [Required(ErrorMessage = " رقم الحالة الشخصية لولي الامر مطلوب")]
        [MaxLength(20, ErrorMessage = "الحد الاقصي لرقم الحالة الشخصية لولي الامر هو 20")]

        public string PersonalStatusGuardianNumber { get; set; }
        [Required(ErrorMessage = "تاريخ اصدار الحالة الشخصية لولي الامر مطلوب")]
        public DateOnly? PersonalStatusGuardianStartDate { get; set; }
        [Required(ErrorMessage = "تاريخ انتهاء الحالة الشخصية لولي الامر مطلوب")]
        public DateOnly? PersonalStatusGuardianEndDate { get; set; }
        [Required(ErrorMessage = " مكان اصدار الحالة الشخصية  لولي الامر مطلوب")]
        [MaxLength(30, ErrorMessage = "الحد الاقصي لاسم مكان اصدار الحالة الشخصية لولي الامر هو 30")]

        public string PersonalStatusGuardianPlace { get; set; }
        [Required(ErrorMessage = "اسم ام الطالب مطلوب")]
        [MaxLength(30, ErrorMessage = "الحد الاقصي لاسم ام الطالب  هو 30")]

        public string MotherStudentName { get; set; }
        [Required(ErrorMessage = "علاقة الطالب بالام مطلوب")]
        [MaxLength(30, ErrorMessage = "الحد الاقصي لاسم علاقة الطالب بالام  هو 30")]

        public string KinshipMother { get; set; }
        [Required(ErrorMessage = "رجا اختر الجنسية الام")]
        public int MotherNationalId { get; set; }
        public string? MotherWork { get; set; }
        public string? MotherFaxNumber { get; set; }
        public string? MotherWorkMobile { get; set; }
        [Required(ErrorMessage = "وظيفة ام الطالب مطلوب")]
        [MaxLength(30, ErrorMessage = "الحد الاقصي لاسم ام الطالب هو 30")]

        public string MotherJob { get; set; }
        [Required(ErrorMessage = "   رقم جوال الام مطلوب")]
        [MaxLength(30, ErrorMessage = "الحد الاقصي لرقم جوال الام  هو 30")]

        public string MotherMobilePhone { get; set; }
        public string? Notes { get; set; }
        public decimal CLSCloth { get; set; } = 0;
        public decimal CLSAcpt { get; set; } = 0;
        public decimal CLSBakelite { get; set; } = 0;
        public decimal CLSRegs { get; set; } = 0;
        [Required(ErrorMessage = "هل ولي الامر من العاملين  ")]
        public bool AreYouWantGoWithBusSchool { get; set; }
        public short? DirectionBus { get; set; }
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
        public decimal TotalCost { get; set; } = 0;
        public decimal TotalCostAfterDiscount { get; set; } = 0;
        public decimal ReceiptTotalFees { get; set; } = 0;
        public decimal ReceiptTotalPayments { get; set; } = 0;
        public decimal RemainingFees { get; set; } = 0;
        public List<VWInstallmentCostBeforeDiscount> installmentCostBeforeDiscounts { get; set; } = new List<VWInstallmentCostBeforeDiscount>();
        public List<VWInstallmentCostAfterDiscount> installmentCostAfterDiscounts { get; set; } = new List<VWInstallmentCostAfterDiscount>();

    }

    public class VWInstallmentCostAfterDiscount
    {
        public int Id { get; set; }
        public int InstallmentId { get; set; }
        public decimal CostInstallment { get; set; } = 0;
        public string InstallmentName { get; set; }
    }

    public class VWInstallmentCostBeforeDiscount
    {
        public int Id { get; set; }
        public int InstallmentId { get; set; }
        public decimal CostInstallment { get; set; } = 0;
        public string InstallmentName { get; set; }
    }
}
