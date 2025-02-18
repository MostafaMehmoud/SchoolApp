using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;

namespace SchoolApp.DAL.Repositories.IRepository
{
    public interface IRepositorySpecial<T> : IRepositoryBase<T> where T : class
    {
        Task<T> GetMin();
        Task<T> GetMax();
        Task<T> GetNextOrPreviousItemByCode(int id, string direction);
        int GetMaxIdOfItem();
        Task<int> GetNewCode();
    }


}
