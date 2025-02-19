using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.Models
{
    public class ClassType
    {
        public ClassType()
        {
           
            Amounts = new List<Amount>();
        }
        public int Id { get; set; }
        public int Code { get; set; }
        
        public int StageId { get; set; }
        
        public DateTime CurrentDateClassType { get; set; }
        public decimal CLSAcpt {  get; set; }
        public decimal CLSRegs {  get; set; }
        public decimal CLSCloth { get; set; }
        public List<Amount> Amounts { get; set; }    
        
    }
}
