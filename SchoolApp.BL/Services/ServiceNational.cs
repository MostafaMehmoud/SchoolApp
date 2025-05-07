using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.Repositories.UnitOfWork;

namespace SchoolApp.BL.Services
{
    public class ServiceNational : IServiceNational
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceNational(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public string Add(int code, string name)
        {
            var National = new National
            {
                code = code,
                Name = name
            };
           if(_unitOfWork.Nationals.Add(National))
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
           var studentWithNational=_unitOfWork.students.GetAll().Where(i=>i.NationalId==id);
            if (studentWithNational.Count() > 0)
            {
                return "يجب تغير جنسية الطلابة";
            }
            if (_unitOfWork.Nationals.Delete(id))
            {
                return "تم الحذف بنجاح";
            }
            else
            {
                return "حدثت مشكلة اثناء الحذف";
            }
        }

        public string Edit(int id, int code, string name)
        {
            var National = new National
            {
                ID=id,
                code = code,
                Name = name
            };
            if (_unitOfWork.Nationals.Update(National))
            {
                return "تم التعديل بنجاح";
            }
            else
            {
                return "حدثت مشكلة اثناء التعديل";
            }
        }

        public IEnumerable<National> GetAll()
        {
            return _unitOfWork.Nationals.GetAll();
        }

        public National GetbyId(int id)
        {
            return _unitOfWork.Nationals.GetById(id);
        }

        public async Task<National> GetMaxNational()
        {
            return await _unitOfWork.repositoryNationalSpecial.GetMax();
        }

        public int GetMaxNationalId()
        {
            return _unitOfWork.repositoryNationalSpecial.GetMaxIdOfNational();
        }

        public async Task<National> GetMinNational()
        {
            return await _unitOfWork.repositoryNationalSpecial.GetMin();
        }

        public async Task<int> GetNewCode()
        {
           var code= await _unitOfWork.repositoryNationalSpecial.GetNewCode();
            return code;
        }
        public async Task<National> GetNextNational(int id)
        {
            // Get the next record relative to the record with the specified id.
            return await _unitOfWork.repositoryNationalSpecial.GetNextOrPreviousNationalByCode(id, "next");
        }

        public async Task<National> GetPreviousNational(int id)
        {
            // Get the previous record relative to the record with the specified id.
            return await _unitOfWork.repositoryNationalSpecial.GetNextOrPreviousNationalByCode(id, "previous");
        }
    }
}
