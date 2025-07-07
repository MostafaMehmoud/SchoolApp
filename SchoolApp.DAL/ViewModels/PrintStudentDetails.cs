using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.ViewModels
{
    public class PrintStudentDetails
    {
        public string StudentName { get; set; }
        public string Stage { get; set; }
        public string Level { get; set; }
        
        public string Term { get; set; }
        public string BusType { get; set; }
        public string Guardian { get; set; }
        public string Phone { get; set; }
        public string Relation { get; set; }
        public string HomeAddress { get; set; }
        public string WorkAddress { get; set; }
        public DateOnly date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public decimal Fees {  get; set; }
        public decimal Payment {  get; set; }

    }
}
