using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.ViewModels
{
    public class VWCostBus
    {
        public int BusId {  get; set; }
        public int BusCostTypeId {  get; set; }
        public decimal FristTremCost {  get; set; }
        public decimal SecondTremCost { get; set; }
    }
}
