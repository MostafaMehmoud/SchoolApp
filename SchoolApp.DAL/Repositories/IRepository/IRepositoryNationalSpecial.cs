using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;

namespace SchoolApp.DAL.Repositories.IRepository
{
    public interface IRepositoryNationalSpecial :IRepositoryBase<National>
    {
        Task<National> GetMin();
        Task<National> GetMax();
        Task<National> GetNextOrPreviousNationalByCode(int id, string direction);
        int GetMaxIdOfNational();
        Task<int> GetNewCode();
    }
}
