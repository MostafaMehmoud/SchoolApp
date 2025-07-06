using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.Repositories.UnitOfWork;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.BL.Services
{
    public class ServiceCompany : IServiceCompany
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceCompany(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
       

        public async Task<Company> GetCompanyAsync()
        {
            return await _unitOfWork.company.GetCompanyAsync();
        }

        public async Task SaveCompanyAsync(Company company)
        {
            var existing = await _unitOfWork.company.GetCompanyAsync();
            if (existing == null)
            {
                await _unitOfWork.company.AddCompanyAsync(company);
            }
            else
            {
                company.Id = existing.Id; // احفظ الـ Id الحالي
                await _unitOfWork.company.UpdateCompanyAsync(company);
            }

            await _unitOfWork.CompleteAsync();
        }
    }
}
