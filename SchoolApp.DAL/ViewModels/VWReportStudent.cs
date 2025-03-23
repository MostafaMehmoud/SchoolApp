using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.ViewModels
{
    public class VWReportStudent
    {
        public int Id { get; set; }
        public int StudentNumber { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string FamilyName { get; set; }
        public string HouseLocationGuardian { get; set; }
        public string? NameOfClosestPerson { get; set; }
        public string StageName { get; set; }
        public string ClassTypeName { get; set; }
        public string ClassName { get; set; }
        public string DepartmentName { get; set; }
        public string? HouseMobileGuardian { get; set; }
        public string GuardianMobile { get; set; }
        public string? GuardianWork { get; set; }
        public string GuardianJob { get; set; }
        public string MotherStudentName { get; set; }
        public string? MotherWorkMobile { get; set; }
        public string MotherJob { get; set; }
        public string MotherMobilePhone { get; set; }
        public decimal ReceiptTotalFees { get; set; } = 0;
        public decimal ReceiptTotalPayments { get; set; } = 0;
        public decimal RemainingFees { get; set; } = 0;
        public decimal BusFees { get; set; } = 0;
        public decimal LastBalance { get; set; } = 0;
    }
}
