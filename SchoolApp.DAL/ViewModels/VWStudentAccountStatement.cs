using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.ViewModels
{
  
    public class AccountStatementDetails
    {
        public string StudentName { get; set; }
        public int StudentId { get; set; }
        public string AcountName { get; set; }
        public DateOnly AccountDate { get; set; }
        public string ReceiptIdOrName {  get; set; }
        public decimal LastBalance {  get; set; }
        public decimal Fees {  get; set; }
        public decimal Payment { get; set; }
        public decimal RamaingPayment {  get; set; }
        public decimal AmountReturn {  get; set; }
    }
}
