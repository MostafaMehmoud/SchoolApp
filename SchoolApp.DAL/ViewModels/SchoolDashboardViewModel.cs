using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.ViewModels
{
    public class SchoolDashboardViewModel
    {
        public int TotalStudents { get; set; }
        public decimal TotalFees { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal RemainingAmount => TotalFees - TotalPaid;
        public int TotalBuses { get; set; }
        public decimal TotalExpenses { get; set; }

        // يمكن إضافة عناصر أخرى لاحقًا
    }


}
