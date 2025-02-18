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
            Stage=new Stage();
            Amounts = new List<Amount>();
        }
        public int Id { get; set; }
        public int Code { get; set; }
        
        public int StageId { get; set; }
        public Stage Stage { get; set; }    
        public decimal CLSAmountBus {  get; set; }
        public decimal CLSAmountBook {  get; set; }
        public decimal CLSAmountAdvs {  get; set; }
        public decimal CLSAmountInst { get; set; }
        public List<Amount> Amounts { get; set; }    
        
    }
}
