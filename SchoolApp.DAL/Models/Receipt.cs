using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.Models
{
    public class Receipt
    {
        [Key]
        public int Id { get; set; }
        public int Code {  get; set; }
        public DateOnly ReceiptDate { get; set; }               
        public int StudentId {  get; set; }
        public string StudentName { get; set; } 
        public decimal Amount {  get; set; }
        public decimal CLSCloth { get; set; } = 0;
        public decimal CLSAcpt { get; set; } = 0;
        public decimal CLSBakelite { get; set; } = 0;
        public decimal CLSRegs { get; set; } = 0;
        public int CashCheque {  get; set; }
        public int? ChequeNumber {  get; set; }
        public DateOnly? ChequeDate { get; set; }
        public string Purpose {  get; set; }
        public string? BankName {  get; set; }
        public decimal ReceiptBusFirstTremCost { get; set; } = 0;
        public decimal ReceiptBusSecondTremCost { get; set; } = 0;
        public string AmountName { get; set; }

        public List<InstallmentReceipt> installmentReceipts { get; set; }
    }
    public class InstallmentReceipt
    {
        [Key]
        public int Id { get; set; }
        public int ReceiptId { get; set; }
        public decimal CostInstallment { get; set; } = 0;
        public string InstallmentName { get; set; }
    }
}
