using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.Repositories.UnitOfWork;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.BL.Services
{
    public class ServiceClassType : IServiceClassType
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceClassType(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string Add(VWClassType ClassTypes)
        {
            List<Amount> Amounts = new List<Amount>();
            foreach (var amount in ClassTypes.amounts)
            {
                Amount Amount = new Amount();
                Amount.AmountPrice=amount.AmountPrice;
                Amount.AmountDate=amount.AmountDate;
                Amount.ClassTypeId=ClassTypes.Id;
                Amounts.Add(Amount);
            }
           ClassType classType = new ClassType();   
            classType.Amounts = Amounts;
            classType.Code = ClassTypes.Code;
            classType.StageId=ClassTypes.StageId;
            classType.CLSAmountInst=ClassTypes.CLSAmountInst;
            classType.CLSAmountBus=ClassTypes.CLSAmountBus;
            classType.CLSAmountAdvs=ClassTypes.CLSAmountAdvs;
            classType.CLSAmountBook=ClassTypes.CLSAmountBook;
            if (_unitOfWork.classTypesSpecial.Add(classType))
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
            if (_unitOfWork.classTypesSpecial.Delete(id))
            {
                return "تم الحذف بنجاح";
            }
            else
            {
                return "حدثت مشكلة اثناء الحذف";
            }
        }

        public string Edit(VWClassType ClassTypes)
        {
            List<Amount> Amounts = new List<Amount>();
            foreach (var amount in ClassTypes.amounts)
            {
                Amount Amount = new Amount();
                Amount.AmountPrice = amount.AmountPrice;
                Amount.AmountDate = amount.AmountDate;
                Amount.ClassTypeId = ClassTypes.Id;
                Amounts.Add(Amount);
            }
            ClassType classType = new ClassType();
            classType.Amounts = Amounts;
            classType.Code = ClassTypes.Code;
            classType.StageId = ClassTypes.StageId;
            classType.CLSAmountInst = ClassTypes.CLSAmountInst;
            classType.CLSAmountBus = ClassTypes.CLSAmountBus;
            classType.CLSAmountAdvs = ClassTypes.CLSAmountAdvs;
            classType.CLSAmountBook = ClassTypes.CLSAmountBook;
            if (_unitOfWork.classTypesSpecial.Update(classType))
            {
                return "تم التعديل بنجاح";
            }
            else
            {
                return "حدثت مشكلة اثناء التعديل";
            }
        }

        public IEnumerable<ClassType> GetAll()
        {
            return _unitOfWork.classTypesSpecial.GetAll();
        }

        public ClassType GetbyId(int id)
        {
            return _unitOfWork.classTypesSpecial.GetById(id);
        }

        public async Task<ClassType> GetMaxClassType()
        {
            return await _unitOfWork.classTypesSpecial.GetMax();
        }

        public int GetMaxClassTypeId()
        {
            return _unitOfWork.classTypesSpecial.GetMaxIdOfItem();
        }

        public async Task<ClassType> GetMinClassType()
        {
            return await _unitOfWork.classTypesSpecial.GetMin();
        }

        public async Task<int> GetNewCode()
        {
            var code = await _unitOfWork.classTypesSpecial.GetNewCode();
            return code;
        }

        public async Task<ClassType> GetNextClassType(int id)
        {
            return await _unitOfWork.classTypesSpecial.GetNextOrPreviousItemByCode(id, "next");
        }

        public async Task<ClassType> GetPreviousClassType(int id)
        {
            return await _unitOfWork.classTypesSpecial.GetNextOrPreviousItemByCode(id, "previous");
        }
    }
}
