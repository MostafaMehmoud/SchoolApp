using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.Repositories;

namespace SchoolApp.BL.Services.IServices
{
    public interface IServiceNational
    {
       public string Add(int code,string name);
       public string Edit(int id,int code,string name);
        public string Delete(int id);
        public National GetbyId(int id);
        public IEnumerable<National> GetAll();
        public Task<int> GetNewCode();
        Task<National> GetNextNational(int id);
        Task<National> GetPreviousNational(int id);
        Task<National> GetMinNational();
        Task<National> GetMaxNational();
        int GetMaxNationalId();
    }
}
