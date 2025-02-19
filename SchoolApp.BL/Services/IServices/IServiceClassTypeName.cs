using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.BL.Services.IServices
{
    public interface IServiceClassTypeName
    {
        public string Add(VWClassTypeName ClassTypeName);
        public string Edit(VWClassTypeName ClassTypeName);
        public string Delete(int id);
        public ClassTypeName GetbyId(int id);
        public IEnumerable<ClassTypeName> GetAll();
        public Task<int> GetNewCode();
        Task<ClassTypeName> GetNextClassTypeName(int id);
        Task<ClassTypeName> GetPreviousClassTypeName(int id);
        Task<ClassTypeName> GetMinClassTypeName();
        Task<ClassTypeName> GetMaxClassTypeName();
        int GetMaxClassTypeNameId();
        public List<ClassTypeName> GetAllClassTypesByStageId(int StageId);  
    }
}
