using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.Repositories.IRepository;
using SchoolApp.DAL.Repository;

namespace SchoolApp.DAL.Repositories
{
    public class RepositoryCompany : ICompanyRepository
    {
        private readonly ApplicationDBContext _context;
        public RepositoryCompany(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Company> GetCompanyAsync()
        {
            return await _context.companies.FirstOrDefaultAsync();
        }

        public async Task AddCompanyAsync(Company company)
        {
            await _context.companies.AddAsync(company);
        }

        public async Task UpdateCompanyAsync(Company company)
        {
            _context.companies.Update(company);
        }
    }
}
