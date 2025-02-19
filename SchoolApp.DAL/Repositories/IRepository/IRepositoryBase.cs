using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;

namespace SchoolApp.DAL.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(int id);

        void AddRange(IEnumerable<T> entities);

        void UpdateRange(IEnumerable<T> entities);
        void DeleteRange(IEnumerable<T> entities);
        void Save();
        IEnumerable<T> GetAllWithInclude(params Expression<Func<T, object>>[] includeProperties);


    }

}
