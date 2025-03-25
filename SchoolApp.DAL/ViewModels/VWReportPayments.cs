using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.ViewModels
{
    public class VWReportPayments
    {
        public string studentNumber {  get; set; }
        public string StudentName {  get; set; }
        public DateOnly PaymentDate { get; set; }
        public int PaymentId {  get; set; }
        public decimal AmountPayment {  get; set; }
    }
}
