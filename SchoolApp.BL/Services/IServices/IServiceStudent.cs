using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.BL.Services.IServices
{
    public interface IServiceStudent
    {
        public string Add(VWStudent Student);
        public string Edit(VWStudent Student);
        public string Delete(int id);
        public Student GetbyId(int id);
        public IEnumerable<Student> GetAll();
        public Task<int> GetNewCode();
        Task<VWStudent> GetNextStudent(int id);
        Task<VWStudent> GetPreviousStudent(int id);
        Task<VWStudent> GetMinStudent();
        Task<VWStudent> GetMaxStudent();
        int GetMaxStudentId();
        VWStudentCostReceipt GetCostReceipt(int StudentId);
        PrintStudentDetails GetPrintStudentDetails(int StudentId);  
        
      
    }
}
