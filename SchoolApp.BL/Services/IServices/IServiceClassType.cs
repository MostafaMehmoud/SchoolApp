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
        Task<ClassType> GetNextClassType(int id);
        Task<ClassType> GetPreviousClassType(int id);
        Task<ClassType> GetMinClassType();
        Task<ClassType> GetMaxClassType();
        int GetMaxClassTypeId();
        
    }
}
