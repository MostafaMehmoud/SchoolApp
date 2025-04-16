using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SchoolApp.DAL.Models
{
    public class ApplicationUser:IdentityUser
    {
        public int UserNumber { get; set; } // رقم المستخدم
        public string Level { get; set; }   // المستوى (مشرف - إضافة - تعديل وإضافة - اظهار)

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
}
