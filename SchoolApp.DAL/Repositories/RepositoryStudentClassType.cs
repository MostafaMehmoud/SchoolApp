using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.Repository;

namespace SchoolApp.DAL.Repositories
{
    public class RepositoryStudentClassType : Repository<StudentsClassType>
    {
        public RepositoryStudentClassType(ApplicationDBContext context) : base(context)
        {
        }
    }
}
