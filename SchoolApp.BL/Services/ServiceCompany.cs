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

        public async Task SaveCompanyAsync(Company newData)
        {
            var existing = await _unitOfWork.company.GetCompanyAsync();

            if (existing == null)
            {
                await _unitOfWork.company.AddCompanyAsync(newData);
            }
            else
            {
                // تحديث خصائص الكيان الموجود بدلاً من استبداله
               
                existing.CompanyName = newData.CompanyName;
                existing.TaxNumber = newData.TaxNumber;
                existing.TaxRate = newData.TaxRate;
                existing.Logo = newData.Logo;
                existing.Description = newData.Description;
                existing.BulidingAddressNumber = newData.BulidingAddressNumber;
                existing.BulidingAddressAddtionalNumber = newData.BulidingAddressAddtionalNumber;
                existing.NeighborhoodAddress = newData.NeighborhoodAddress;
                existing.StreetAddress = newData.StreetAddress;
                existing.CityAddress = newData.CityAddress;
                existing.AreaAddress = newData.AreaAddress;
                existing.CommercialRegister = newData.CommercialRegister;
                existing.CompanyPhoneNumber = newData.CompanyPhoneNumber;
                existing.CompanyEmail = newData.CompanyEmail;
                existing.EstablishedDate = newData.EstablishedDate;

                await _unitOfWork.company.UpdateCompanyAsync(existing);
            }

            await _unitOfWork.CompleteAsync();
        }

    }
}
