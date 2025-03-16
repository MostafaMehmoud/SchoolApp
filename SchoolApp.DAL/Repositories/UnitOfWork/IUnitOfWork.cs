using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
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
        IRepositorySpecial<Installment> installments { get; }
        IRepositorySpecial<Department> departments { get; }
        IRepositorySpecial<Student>   students { get; } 
        IRepositorySpecial<Receipt> receipts { get; }
        IRepositorySpecial<Payment> payments { get; }   
        IDbContextTransaction BeginTransaction();
        void Save();
        int Complete();
    }

}
