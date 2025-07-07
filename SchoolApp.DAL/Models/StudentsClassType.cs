using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.Models
{
    public class StudentsClassType
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int StudentNumber { get; set; }
        public string StudentName { get; set; }
        public int DepartmentId { get; set; }
        public int StageId { get; set; }
        public DateOnly BrithDate { get; set; }
        public string BrithPlace { get; set; }
        public int ClassTypeId { get; set; }
        public decimal LastBalance { get; set; } = 0;
        public decimal ValueAddedTax { get; set; } = 0;
        public decimal MonthlyInstallment { get; set; } = 0;
        public decimal CLSCloth { get; set; } = 0;
        public decimal CLSAcpt { get; set; } = 0;
        public decimal CLSBakelite { get; set; } = 0;
        public decimal CLSRegs { get; set; } = 0;
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
        public decimal TotalCost { get; set; }
        public decimal TotalCostAfterDiscount { get; set; }
        public decimal ReceiptTotalFees { get; set; } = 0;
        public decimal ReceiptTotalPayments { get; set; } = 0;
        public decimal RemainingFees { get; set; } = 0;
        public DateOnly RegistrationDate { get; set; }
        public DateOnly? TransferringDate { get; set; }








    }
}
