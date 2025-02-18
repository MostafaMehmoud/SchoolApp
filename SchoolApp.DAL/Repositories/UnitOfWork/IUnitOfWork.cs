using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.Repositories.IRepository;
using SchoolApp.DAL.Repository;

namespace SchoolApp.DAL.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
       IRepositoryBase<National> Nationals { get; }
        IRepositoryNationalSpecial repositoryNationalSpecial { get; }
        IRepositorySpecial<FileBus> fileBus { get; }
        IRepositorySpecial<Stage> stages { get; }   
        IRepositorySpecial<ClassTypeName> classTypes { get; }   
        IRepositorySpecial<ClassType> classTypesSpecial { get; }    
        int Complete();
    }

}
