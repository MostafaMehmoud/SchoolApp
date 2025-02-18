using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;

namespace SchoolApp.BL.Services.IServices
{
    public interface IServiceStage
    {
        public string Add(int code, string name);
        public string Edit(int id, int code, string name);
        public string Delete(int id);
        public Stage GetbyId(int id);
        public IEnumerable<Stage> GetAll();
        public Task<int> GetNewCode();
        Task<Stage> GetNextStage(int id);
        Task<Stage> GetPreviousStage(int id);
        Task<Stage> GetMinStage();
        Task<Stage> GetMaxStage();
        int GetMaxStageId();
    }
}
