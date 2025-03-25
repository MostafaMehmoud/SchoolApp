using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.ViewModels
{
    public class VWFeesReport
    {
        public int Id { get; set; }
       
       
        public string StudentName { get; set; }
        public string ClassTypeName { get; set; }
        public string StageName { get; set; }
       
       
        public decimal CLSAcpt { get; set; } = 0;
        public decimal CLSRegs { get; set; } = 0;
        public decimal CLSCloth { get; set; } = 0;
        public decimal CLSBakelite { get; set; } = 0;
        public decimal CostBus { get; set; } = 0;
        public decimal TotalAmountClassType { get; set; } = 0;
        public decimal TotalAmountFees { get; set; } = 0;
        
    }
}
