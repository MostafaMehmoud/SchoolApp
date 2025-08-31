using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;

namespace SchoolApp.DAL.ViewModels
{
    public class VWReceipt
    {
        [Key]
        public int Id { get; set; }
        public int Code { get; set; }
        public DateOnly ReceiptDate { get; set; }
        [Required(ErrorMessage ="رجا اختر طالب")]
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public decimal Amount { get; set; }
        public decimal CLSCloth { get; set; } = 0;
        public decimal CLSAcpt { get; set; } = 0;
        public decimal CLSBakelite { get; set; } = 0;
        public decimal CLSRegs { get; set; } = 0;
        public int CashCheque { get; set; }
        public int? ChequeNumber { get; set; }
        public DateOnly? ChequeDate { get; set; }
        public string Purpose { get; set; }
        public decimal ReceiptBusFirstTremCost { get; set; }
        public decimal ReceiptBusSecondTremCost { get; set; }
        public decimal ReceiptTotalFees { get; set; } = 0;
        public decimal ReceiptTotalPayments { get; set; } = 0;
        public decimal RemainingFees { get; set; } = 0;
        public decimal LastBalance { get; set; } = 0;
        public string AmountName { get; set; }
        public string? BankName { get; set; }
        public decimal TaxRate { get; set; } // مثل 15
        public decimal AmountAfterTaxRate { get; set; } // مثل 15
        public string AmountNameAfterTaxRate { get; set; }
        public decimal TaxRateValue { get; set; }
        public List<InstallmentReceipt> installmentReceipts { get; set; }
    }
}
