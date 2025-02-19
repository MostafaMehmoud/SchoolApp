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
    public class ServiceClassTypeName : IServiceClassTypeName
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceClassTypeName(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string Add(VWClassTypeName ClassTypeName)
        {
            var ClassType = new ClassTypeName
            {
                Id = ClassTypeName.Id,
                Code = ClassTypeName.Code,
                StageId= ClassTypeName.StageId,
                Name = ClassTypeName.Name
            };
            if (_unitOfWork.classTypes.Add(ClassType))
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
            if (_unitOfWork.classTypes.Delete(id))
            {
                return "تم الحذف بنجاح";
            }
            else
            {
                return "حدثت مشكلة اثناء الحذف";
            }
        }

        public string Edit(VWClassTypeName ClassTypeName)
        {
            var ClassType = new ClassTypeName
            {
                Id = ClassTypeName.Id,
                Code = ClassTypeName.Code,
                StageId = ClassTypeName.StageId,
                Name = ClassTypeName.Name

            };
            if (_unitOfWork.classTypes.Update(ClassType))
            {
                return "تم التعديل بنجاح";
            }
            else
            {
                return "حدثت مشكلة اثناء التعديل";
            }
        }

        public IEnumerable<ClassTypeName> GetAll()
        {

            return _unitOfWork.classTypes.GetAll();
        }

        public List<ClassTypeName> GetAllClassTypesByStageId(int StageId)
        {
            return _unitOfWork.classTypes.GetAll().Where(i=>i.StageId==StageId).ToList();
        }

        public ClassTypeName GetbyId(int id)
        {
            return _unitOfWork.classTypes.GetById(id);
        }

        public async Task<ClassTypeName> GetMaxClassTypeName()
        {
            return await _unitOfWork.classTypes.GetMax();
        }

        public int GetMaxClassTypeNameId()
        {
            return _unitOfWork.classTypes.GetMaxIdOfItem();
        }
        

        public async Task<ClassTypeName> GetMinClassTypeName()
        {
            return await _unitOfWork.classTypes.GetMin();
        }

        public async Task<int> GetNewCode()
        {
            var code = await _unitOfWork.classTypes.GetNewCode();
            return code;
        }

        public async Task<ClassTypeName> GetNextClassTypeName(int id)
        {
            return await _unitOfWork.classTypes.GetNextOrPreviousItemByCode(id, "next");
        }

        public async Task<ClassTypeName> GetPreviousClassTypeName(int id)
        {
            return await _unitOfWork.classTypes.GetNextOrPreviousItemByCode(id, "previous");
        }
    }
}
