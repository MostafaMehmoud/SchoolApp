using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;

namespace SchoolApp.DAL.ViewModels
{
    public class VWClassType
    {
        public VWClassType()
        {
            amounts = new List<VWAmount>();
        }
        public int Id { get; set; }
        public int Code { get; set; }

        public int StageId { get; set; }
       
        public decimal CLSAmountBus { get; set; }
        public decimal CLSAmountBook { get; set; }
        public decimal CLSAmountAdvs { get; set; }
        public decimal CLSAmountInst { get; set; }
        public List<VWAmount> amounts { get; set; }   
    }
    public class VWAmount
    {
        public int Id { get; set; }

        public int ClassTypeId { get; set; }

        public decimal AmountPrice { get; set; }
        public DateTime AmountDate { get; set; }
    }
}
