using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.Models
{
    public class Amount
    {
        public Amount()
        {
            ClassType=new ClassType();
        }
        public int Id {  get; set; }

        public int ClassTypeId {  get; set; }
        public ClassType ClassType { get; set; }    
        public decimal AmountPrice { get; set; }
        public DateTime AmountDate { get; set; }
    }
}
