using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;

namespace SchoolApp.DAL.Repositories.IRepository
{
    public interface ICompanyRepository
    {
        Task<Company> GetCompanyAsync();
        Task AddCompanyAsync(Company company);
        Task UpdateCompanyAsync(Company company);
    }

}
