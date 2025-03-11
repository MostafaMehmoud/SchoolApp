using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.BL.Services.IServices
{
    public interface IServiceClassType
    {
        public string Add(VWClassType ClassType);
        public string Edit(VWClassType ClassType);
        public string Delete(int id);
        public ClassType GetbyId(int id);
        public IEnumerable<ClassType> GetAll();
        public Task<int> GetNewCode();
        Task<VWClassType> GetNextClassType(int id);
        Task<VWClassType> GetPreviousClassType(int id);
        Task<VWClassType> GetMinClassType();
        Task<VWClassType> GetMaxClassType();
        int GetMaxClassTypeId();
        public List<ListClassType> GetAllClassTypes();   
        public ListClassType GetClassTypesById(int id);
        public IEnumerable<ReportClassType> GetAllReportTypes();
        public ReportClassType GetReportClassTypeById(int id);  
        
    }
}
