using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.BL.Services.IServices
{
    public interface IServiceDepartment
    {
        public string Add(VWDepartment Department);
        public string Edit(VWDepartment Department);
        public string Delete(int id);
        public Department GetbyId(int id);
        public IEnumerable<Department> GetAll();
        public Task<int> GetNewCode();
        Task<Department> GetNextDepartment(int id);
        Task<Department> GetPreviousDepartment(int id);
        Task<Department> GetMinDepartment();
        Task<Department> GetMaxDepartment();
        int GetMaxDepartmentId();
    }
}
