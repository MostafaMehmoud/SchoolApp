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
    public class ServiceInstallment : IServiceInstallment
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceInstallment(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string Add(VWInstallment Installment)
        {
            var installment = new Installment
            {
                Id = Installment.Id,
                Code = Installment.Code,
                StageId = Installment.StageId,
                InstallName = Installment.InstallName,
                ClassTypeId = Installment.ClassTypeId,
            };
            if (_unitOfWork.installments.Add(installment))
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
            if (_unitOfWork.installments.Delete(id))
            {
                return "تم الحذف بنجاح";
            }
            else
            {
                return "حدثت مشكلة اثناء الحذف";
            }
        }

        public string Edit(VWInstallment Installment)
        {
            var installment = new Installment
            {
                Id = Installment.Id,
                Code = Installment.Code,
                StageId = Installment.StageId,
                InstallName = Installment.InstallName,
                ClassTypeId = Installment.ClassTypeId,
            };
            if (_unitOfWork.installments.Update(installment))
            {
                return "تم التعديل بنجاح";
            }
            else
            {
                return "حدثت مشكلة اثناء التعديل";
            }
        }

        public IEnumerable<Installment> GetAll()
        {
            return _unitOfWork.installments.GetAll();
        }

        public List<Installment> GetAllInstallmentsByStageId(int StageId)
        {
            return _unitOfWork.installments.GetAll().Where(i => i.StageId == StageId).ToList();
        }

        public InstallmentMain GetAllInstallmentsByStageIdAndClassTypeId(int StageId, int ClassTypeId)
        {
            List<VWInstallmentStudent> vWInstallmentStudent = new List<VWInstallmentStudent>();
            var classtype = _unitOfWork.classTypesSpecial.GetAll()
                .Where(i => i.StageId == StageId).FirstOrDefault();
            var amounts = _unitOfWork.classTypesSpecial
               .GetAllWithInclude(i => i.Amounts).Where(i=>i.StageId==StageId)
               .Select(i => i.Amounts).FirstOrDefault();
            var amountPrice = amounts.FirstOrDefault(i => i.ClassTypeNameId == ClassTypeId);
            // Get amount price safely
            
            // If no amountPrice is found, return an empty list
            if (amountPrice == null)
            {
                return null;
            }

            // Get installments
            var installments = _unitOfWork.installments
                .GetAll()
                .Where(i => i.StageId == StageId && i.ClassTypeId == ClassTypeId)
                .ToList();

            // Prevent division by zero
            int lengthOfInstallments = installments.Count;
            if (lengthOfInstallments == 0)
            {
                return null;
            }

            decimal amount = amountPrice.AmountPrice / lengthOfInstallments;
            
            // Construct VWInstallmentStudent list
            foreach (var student in installments)
            {
                vWInstallmentStudent.Add(new VWInstallmentStudent
                {
                    InstallName = student.InstallName,
                    Code = student.Code,
                    StageId = student.StageId,
                    ClassTypeId = student.ClassTypeId,
                    Id = student.Id,
                    AmountInstallment = amount,
                    
                });
            }
            InstallmentMain installmentMain = new InstallmentMain()
            {
                CLSAcpt = classtype.CLSAcpt,
                CLSBakelite = classtype.CLSBakelite,
                CLSCloth = classtype.CLSCloth,
                CLSRegs = classtype.CLSRegs,
                Installments= vWInstallmentStudent
            };
            return installmentMain;
        }

        public InstallmentMain GetAInstallmentofClassType(int StageId,int ClassTypeId)
        {
            var classtype = _unitOfWork.classTypesSpecial.GetAll()
                .Where(i => i.StageId == StageId).FirstOrDefault();
            var amounts = _unitOfWork.classTypesSpecial
                .GetAllWithInclude(i => i.Amounts).Where(i=>i.StageId== StageId)
                .Select(i => i.Amounts).FirstOrDefault();
            var amount = amounts.FirstOrDefault(i => i.ClassTypeNameId == ClassTypeId);
            List<VWInstallmentStudent> vWInstallmentStudent=new List<VWInstallmentStudent>();
            vWInstallmentStudent.Add(new VWInstallmentStudent
            {
                AmountInstallment = amount.AmountPrice
            });
            InstallmentMain installmentMain = new InstallmentMain()
            {
                CLSAcpt = classtype.CLSAcpt,
                CLSBakelite = classtype.CLSBakelite,
                CLSCloth = classtype.CLSCloth,
                CLSRegs = classtype.CLSRegs,
                Installments = vWInstallmentStudent
            };
            return installmentMain;
        }

        public Installment GetbyId(int id)
        {
            return _unitOfWork.installments.GetById(id);
        }

        public async Task<Installment> GetMaxInstallment()
        {
            return await _unitOfWork.installments.GetMax();
        }

        public int GetMaxInstallmentId()
        {
            return _unitOfWork.installments.GetMaxIdOfItem();
        }

        public async Task<Installment> GetMinInstallment()
        {
            return await _unitOfWork.installments.GetMin();
        }

        public async Task<int> GetNewCode()
        {
            var code = await _unitOfWork.installments.GetNewCode();
            return code;
        }

        public async Task<Installment> GetNextInstallment(int id)
        {
            return await _unitOfWork.installments.GetNextOrPreviousItemByCode(id, "next");
        }

        public async Task<Installment> GetPreviousInstallment(int id)
        {
            return await _unitOfWork.installments.GetNextOrPreviousItemByCode(id, "previous");
        }
    }
}
