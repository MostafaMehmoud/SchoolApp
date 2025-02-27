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
                // Amount.ClassTypeId=ClassTypes.Id;
                Amount.ClassTypeNameId = amount.ClassTypeNameId;
                Amounts.Add(Amount);
            }
           ClassType classType = new ClassType();   
            classType.Amounts = Amounts;
            classType.Code = ClassTypes.Code;
            classType.StageId=ClassTypes.StageId;
            classType.CLSAcpt=ClassTypes.CLSAcpt;
            classType.CLSCloth=ClassTypes.CLSCloth;
            classType.CLSRegs = ClassTypes.CLSRegs;
            classType.CLSBakelite=ClassTypes.CLSBakelite;
           classType.CurrentDateClassType=ClassTypes.CurrentDateClassType;  
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
            var existingClassType = _unitOfWork.classTypesSpecial.GetById(ClassTypes.Id);

            if (existingClassType == null)
            {
                return "لم يتم العثور على السجل";
            }

            // Update basic properties
            existingClassType.Code = ClassTypes.Code;
            existingClassType.StageId = ClassTypes.StageId;
            existingClassType.CLSAcpt = ClassTypes.CLSAcpt;
            existingClassType.CLSCloth = ClassTypes.CLSCloth;
            existingClassType.CLSRegs = ClassTypes.CLSRegs;
            existingClassType.CLSBakelite= ClassTypes.CLSBakelite;
            existingClassType.CurrentDateClassType=ClassTypes.CurrentDateClassType; 
            // Update Amounts
            foreach (var amount in ClassTypes.amounts)
            {
                var existingAmount = existingClassType.Amounts.FirstOrDefault(a => a.Id == amount.Id);

                if (existingAmount != null)
                {
                    // Update existing amount
                    existingAmount.AmountPrice = amount.AmountPrice;
                    existingAmount.AmountDate = amount.AmountDate;
                    existingAmount.ClassTypeNameId = amount.ClassTypeNameId;
                }
                else
                {
                    // Add new amount if not found
                    existingClassType.Amounts.Add(new Amount
                    {
                        Id = amount.Id,
                        AmountPrice = amount.AmountPrice,
                        AmountDate = amount.AmountDate,
                        ClassTypeId = ClassTypes.Id,
                        ClassTypeNameId = amount.ClassTypeNameId
                    });
                }
            }

            // Remove amounts that are missing from the new list
            existingClassType.Amounts.RemoveAll(a => !ClassTypes.amounts.Any(na => na.Id == a.Id));

            // Save Changes
            if (_unitOfWork.classTypesSpecial.Update(existingClassType))
            {
                return "تم التعديل بنجاح";
            }
            else
            {
                return "حدثت مشكلة أثناء التعديل";
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

        public async Task<VWClassType> GetMaxClassType()
        {
           
            var classtype = await _unitOfWork.classTypesSpecial.GetMax();
            var stage = _unitOfWork.stages.GetById(classtype.StageId);
            VWClassType vWClassType = new VWClassType();
            vWClassType.Id = classtype.Id;
            vWClassType.Code = classtype.Code;
            vWClassType.StageId = stage.Id;
            vWClassType.StageName = stage.StageName;
            vWClassType.CLSRegs = classtype.CLSRegs;
            vWClassType.CLSCloth = classtype.CLSCloth;
            vWClassType.CLSAcpt=classtype.CLSAcpt;
            vWClassType.CLSBakelite=classtype.CLSBakelite;
            vWClassType.CurrentDateClassType= classtype.CurrentDateClassType;   
            VWAmoumt vWAmoumt = null;
            List<VWAmoumt> vWAmoumts = new List<VWAmoumt>();
            foreach (var amount in classtype.Amounts)
            {
                vWAmoumt = new VWAmoumt();
                vWAmoumt.Id = amount.Id;
                vWAmoumt.ClassTypeNameId = amount.ClassTypeNameId;
                vWAmoumt.AmountPrice = amount.AmountPrice;
                vWAmoumt.AmountDate = amount.AmountDate;
                vWAmoumt.ClassTypeId = amount.ClassTypeId;
                
                vWAmoumt.ClassTypeName = _unitOfWork.classTypes.GetById(amount.ClassTypeNameId).Name;
                vWAmoumts.Add(vWAmoumt);
            }
            vWClassType.amounts = vWAmoumts;
            return vWClassType;
        }

        public int GetMaxClassTypeId()
        {
            return _unitOfWork.classTypesSpecial.GetMaxIdOfItem();
        }

        public async Task<VWClassType> GetMinClassType()
        {
            
            var classtype = await _unitOfWork.classTypesSpecial.GetMin();
            var stage = _unitOfWork.stages.GetById(classtype.StageId);
            VWClassType vWClassType = new VWClassType();
            vWClassType.Id = classtype.Id;
            vWClassType.Code = classtype.Code;
            vWClassType.StageId = stage.Id;
            vWClassType.StageName = stage.StageName;
            vWClassType.CLSRegs = classtype.CLSRegs;
            vWClassType.CLSCloth = classtype.CLSCloth;
            vWClassType.CLSAcpt = classtype.CLSAcpt;
            vWClassType.CLSBakelite = classtype.CLSBakelite;
            vWClassType.CurrentDateClassType = classtype.CurrentDateClassType;
            VWAmoumt vWAmoumt = null;
            List<VWAmoumt> vWAmoumts = new List<VWAmoumt>();
            foreach(var amount in classtype.Amounts)
            {
                vWAmoumt=new VWAmoumt();
                vWAmoumt.Id=amount.Id;
                vWAmoumt.ClassTypeNameId=amount.ClassTypeNameId;
                vWAmoumt.AmountPrice = amount.AmountPrice;
                vWAmoumt.AmountDate=amount.AmountDate;
                vWAmoumt.ClassTypeId=amount.ClassTypeId;   
                vWAmoumt.ClassTypeName=_unitOfWork.classTypes.GetById(amount.ClassTypeNameId).Name;
                vWAmoumts.Add(vWAmoumt);
            }
            vWClassType.amounts=vWAmoumts;
            return vWClassType;
            
        }

        public async Task<int> GetNewCode()
        {
            var code = await _unitOfWork.classTypesSpecial.GetNewCode();
            return code;
        }

        public async Task<VWClassType> GetNextClassType(int id)
        {
            var classtype = await _unitOfWork.classTypesSpecial.GetNextOrPreviousItemByCode(id, "next");
            if (classtype == null)
                return null;
            var stage = _unitOfWork.stages.GetById(classtype.StageId);
            VWClassType vWClassType = new VWClassType();
            vWClassType.Id = classtype.Id;
            vWClassType.Code = classtype.Code;
            vWClassType.StageId = stage.Id;
            vWClassType.StageName = stage.StageName;
            vWClassType.CLSRegs = classtype.CLSRegs;
            vWClassType.CLSCloth = classtype.CLSCloth;
            vWClassType.CLSAcpt = classtype.CLSAcpt;
            vWClassType.CLSBakelite = classtype.CLSBakelite;
            vWClassType.CurrentDateClassType= classtype.CurrentDateClassType;   
            VWAmoumt vWAmoumt = null;
            List<VWAmoumt> vWAmoumts = new List<VWAmoumt>();
            foreach (var amount in classtype.Amounts)
            {
                vWAmoumt = new VWAmoumt();
                vWAmoumt.Id = amount.Id;
                vWAmoumt.ClassTypeNameId = amount.ClassTypeNameId;
                vWAmoumt.AmountPrice = amount.AmountPrice;
                vWAmoumt.AmountDate = amount.AmountDate;
                vWAmoumt.ClassTypeId = amount.ClassTypeId;
                
                vWAmoumt.ClassTypeName = _unitOfWork.classTypes.GetById(amount.ClassTypeNameId).Name;
                vWAmoumts.Add(vWAmoumt);
            }
            vWClassType.amounts = vWAmoumts;
            return vWClassType;
            
        }

        public async Task<VWClassType> GetPreviousClassType(int id)
        {
            var classtype= await _unitOfWork.classTypesSpecial.GetNextOrPreviousItemByCode(id, "previous");
            if (classtype == null)
                return null;
            var stage = _unitOfWork.stages.GetById(classtype.StageId);
            VWClassType vWClassType = new VWClassType();
            vWClassType.Id = classtype.Id;
            vWClassType.Code = classtype.Code;
            vWClassType.StageId = stage.Id;
            vWClassType.StageName = stage.StageName;
            vWClassType.CLSRegs = classtype.CLSRegs;
            vWClassType.CLSCloth = classtype.CLSCloth;
            vWClassType.CLSAcpt = classtype.CLSAcpt;
            vWClassType.CLSBakelite = classtype.CLSBakelite;
            vWClassType.CurrentDateClassType = classtype.CurrentDateClassType;  
            VWAmoumt vWAmoumt = null;
            List<VWAmoumt> vWAmoumts = new List<VWAmoumt>();
            foreach (var amount in classtype.Amounts)
            {
                vWAmoumt = new VWAmoumt();
                vWAmoumt.Id = amount.Id;
                vWAmoumt.ClassTypeNameId = amount.ClassTypeNameId;
                vWAmoumt.AmountPrice = amount.AmountPrice;
                vWAmoumt.AmountDate = amount.AmountDate;
                vWAmoumt.ClassTypeId = amount.ClassTypeId;
                vWAmoumt.ClassTypeName = _unitOfWork.classTypes.GetById(amount.ClassTypeNameId).Name;
                vWAmoumts.Add(vWAmoumt);
            }
            vWClassType.amounts = vWAmoumts;
            return vWClassType;
            
        }
    }
}
