using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.Repositories.UnitOfWork;

namespace SchoolApp.BL.Services
{
    public class ServiceStage : IServiceStage
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceStage(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string Add(int code, string name)
        {
            var Stage = new Stage
            {
                StageCode = code,
                StageName = name
            };
            if (_unitOfWork.stages.Add(Stage))
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

            var classtypename = _unitOfWork.classTypes.GetAll().Where(i => i.StageId == id);
            if (classtypename.Any())
                {
                    throw new Exception("يجب حذف المرحلة  الدراسية و الفصل أولا");
                }
            if (_unitOfWork.stages.Delete(id))
            {
                return "تم الحذف بنجاح";
            }
            else
            {
                throw new Exception("حدثت مشكلة اثناء الحذف");
            }
        }

        public string Edit(int id, int code, string name)
        {
            var Stage = new Stage
            {
                Id = id,
                StageCode = code,
                StageName = name
            };
            if (_unitOfWork.stages.Update(Stage))
            {
                return "تم التعديل بنجاح";
            }
            else
            {
                return "حدثت مشكلة اثناء التعديل";
            }
        }

        public IEnumerable<Stage> GetAll()
        {
            return _unitOfWork.stages.GetAll();
        }

        public Stage GetbyId(int id)
        {
            return _unitOfWork.stages.GetById(id);
        }

        public async Task<Stage> GetMaxStage()
        {
            return await _unitOfWork.stages.GetMax();
        }

        public int GetMaxStageId()
        {
            return _unitOfWork.stages.GetMaxIdOfItem();
        }

        public async Task<Stage> GetMinStage()
        {
            return await _unitOfWork.stages.GetMin();
        }

        public async Task<int> GetNewCode()
        {
            var code = await _unitOfWork.stages.GetNewCode();
            return code;
        }

        public async Task<Stage> GetNextStage(int id)
        {
            return await _unitOfWork.stages.GetNextOrPreviousItemByCode(id, "next");
        }

        public  async Task<Stage> GetPreviousStage(int id)
        {
            return await _unitOfWork.stages.GetNextOrPreviousItemByCode(id, "previous");
        }
    }
}
