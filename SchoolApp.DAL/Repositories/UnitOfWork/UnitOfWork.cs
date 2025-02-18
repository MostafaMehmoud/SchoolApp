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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        public IRepositoryBase<National> Nationals { get; }
        public IRepositoryNationalSpecial repositoryNationalSpecial { get; }
        public IRepositorySpecial<FileBus> fileBus { get; }
        public IRepositorySpecial<Stage> stages { get; }

        public IRepositorySpecial<ClassTypeName> classTypes { get; }

        public IRepositorySpecial<ClassType> classTypesSpecial {  get; }

        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
            Nationals = new Repository<National>(_context);
            repositoryNationalSpecial = new RepositoryNationalSpecial(_context);
            fileBus = new RepositoryFileBusSpecial(_context);
            stages=new RepositoryStage(_context);
            classTypes=new RepositoryClassTypeName(_context);
            classTypesSpecial = new RepositoryClassType(_context);
        }

        public int Complete() => _context.SaveChanges();
        public void Dispose() => _context.Dispose();
    }

}
