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
    public class ServiceDepartment : IServiceDepartment
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceDepartment(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string Add(VWDepartment Department)
        {
            var department = new Department
            {
                Id = Department.Id,
                Code = Department.Code,
                
                Name = Department.Name
            };
            if (_unitOfWork.departments.Add(department))
            {
                return "تم الحفظ بنجاح";
            }
            else
            {
                return "حدثت مشكلة اثناء الحفظ";
            }
        }

        public string Delete(int id)
        {
            if (_unitOfWork.departments.Delete(id))
            {
                return "تم الحذف بنجاح";
            }
            else
            {
                return "حدثت مشكلة اثناء الحذف";
            }
        }

        public string Edit(VWDepartment Department)
        {
            var department = new Department
            {
                Id = Department.Id,
                Code = Department.Code,
               
                Name = Department.Name

            };
            if (_unitOfWork.departments.Update(department))
            {
                return "تم التعديل بنجاح";
            }
            else
            {
                return "حدثت مشكلة اثناء التعديل";
            }
        }

        public IEnumerable<Department> GetAll()
        {
            return _unitOfWork.departments.GetAll();
        }

        public Department GetbyId(int id)
        {
            return _unitOfWork.departments.GetById(id);
        }

        public async Task<Department> GetMaxDepartment()
        {
            return await _unitOfWork.departments.GetMax();
        }

        public int GetMaxDepartmentId()
        {
            return _unitOfWork.departments.GetMaxIdOfItem();
        }

        public async Task<Department> GetMinDepartment()
        {
            return await _unitOfWork.departments.GetMin();
        }

        public async Task<int> GetNewCode()
        {
            var code = await _unitOfWork.departments.GetNewCode();
            return code;
        }

        public async Task<Department> GetNextDepartment(int id)
        {
            return await _unitOfWork.departments.GetNextOrPreviousItemByCode(id, "next");
        }

        public async Task<Department> GetPreviousDepartment(int id)
        {
            return await _unitOfWork.departments.GetNextOrPreviousItemByCode(id, "previous");
        }
    }
}
