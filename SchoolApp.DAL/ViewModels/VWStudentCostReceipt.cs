using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.ViewModels
{
    public class VWStudentCostReceipt
    {
        public int StudentId { get; set; }
        public List<InstallmentForStudent> installmentForStudents {  get; set; }=new List<InstallmentForStudent>();
        public decimal ReceiptTotalFees { get; set; } = 0;
        public decimal ReceiptTotalPayments { get; set; } = 0;
        public decimal RemainingFees { get; set; } = 0;
        public decimal CostFirstTermAfterDiscount { get; set; } = 0;
        public decimal CostSecondTermAfterDiscount { get; set; } = 0;
        public string StudentName { get; set; }
        public decimal CLSCloth { get; set; } = 0;
        public decimal CLSAcpt { get; set; } = 0;
        public decimal CLSBakelite { get; set; } = 0;
        public decimal CLSRegs { get; set; } = 0;
        public decimal TotalCost { get; set; } = 0;
        public decimal LastBalance {  get; set; } = 0;
        public bool isSuadi  { get; set; } 
        public decimal TaxRate { get; set; }
    }
    public class InstallmentForStudent
    {
        public int Id { get; set; }
        public int InstallmentId { get; set; }
        public decimal CostInstallment { get; set; }
        public string InstallmentName { get; set; }
    }
}
