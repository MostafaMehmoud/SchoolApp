using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.ViewModels
{
    public class VWUser
    {
        public string Id { get; set; } 
        public int UserNumber { get; set; }
       
        public string Password { get; set; }
        public string Level { get; set; }
        public string UserName {  get; set; }
        public string? Email {  get; set; }
        public List<SelectListItemLevel> Levels { get; set; }
        // صلاحيات
        public bool CanAccessGrades { get; set; }         // مراحل دراسية
        public bool CanAccessStudentsFile { get; set; }   // ملف الطلبة
        public bool CanAccessBusesFile { get; set; }   // ملف الباص
        public bool CanAccessTypesEncoming { get; set; }   // انواع الايردات 
        public bool CanAccessReceipts { get; set; }   //  سندات قبض 
        public bool CanAccessPayments { get; set; }   // سندات صرف 
        public bool CanAccessPrintReports { get; set; }   // طباعة التقارير  
        public bool CanAccessSearch { get; set; }   // استعلامات  
        public bool CanAccessUsersFile { get; set; }   // ملف المستخدمين  
    }
   public class SelectListItemLevel
    {
       public string Text {  get; set; }
        public string Value {  get; set; }
    }
}
