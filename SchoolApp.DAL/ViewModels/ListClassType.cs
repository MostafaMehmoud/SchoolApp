using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.ViewModels
{
    public class ListClassType
    {
        public int Id { get; set; }
        public int Code { get; set; }

        public int StageId { get; set; }
        public string StageName { get; set; }    

        public DateTime CurrentDateClassType { get; set; }
        public decimal CLSAcpt { get; set; }
        public decimal CLSRegs { get; set; }
        public decimal CLSCloth { get; set; }
        public decimal CLSBakelite { get; set; }
        public decimal TotalCostClassType {  get; set; }
        public List<VWAmoumt> amounts { get; set; }
    }
}
