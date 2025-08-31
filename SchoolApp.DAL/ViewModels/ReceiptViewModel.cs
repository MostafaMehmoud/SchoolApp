using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;

namespace SchoolApp.DAL.ViewModels
{
    public class ReceiptViewModel
    {
        public int Code { get; set; }
        public DateOnly ReceiptDate { get; set; }
        public string StudentName { get; set; }
        public decimal Amount { get; set; }
        public string AmountName { get; set; }
        public int CashCheque { get; set; } // 1 = نقدًا، 2 = شيك
        public int? ChequeNumber { get; set; }
        public DateOnly? ChequeDate { get; set; }
        public string Purpose { get; set; }
        public string? BankName { get; set; }
        public decimal TaxRate { get; set; } // مثل 15
        public decimal AmountAfterTaxRate { get; set; } // مثل 15
        public string AmountNameAfterTaxRate { get; set; }
        public decimal TaxRateValue { get; set; }

        public List<InstallmentReceipt> Installments { get; set; }
    }
}
