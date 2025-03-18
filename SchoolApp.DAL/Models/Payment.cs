using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.Models
{
    public class Payment
    {
        [Key]
public int Id { get; set; } 
        public int Code { get; set; }
        public DateOnly PaymentDate { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public decimal Amount { get; set; }
        public int CashCheque { get; set; }
        public int? ChequeNumber { get; set; }
        public DateOnly? ChequeDate { get; set; }
        public string Purpose { get; set; }
        public string? BankName { get; set; }
        public string AmountName { get; set; }
    }
}
