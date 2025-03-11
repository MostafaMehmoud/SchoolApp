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

        public List<ListClassType> GetAllClassTypes()
        {
            var classTypes=_unitOfWork.classTypesSpecial.GetAllWithInclude(i=>i.Amounts);
            if(classTypes.Any())
            {
                List<ListClassType> listClassTypes = new List<ListClassType>();
                ListClassType listClassType = null;
                decimal totalcostclasstype = 0;
                
                foreach (var classType in classTypes)
                {
                    foreach (var costClassType in classType.Amounts)
                    {
                        totalcostclasstype += costClassType.AmountPrice;
                    }
                    listClassType = new ListClassType();
                    listClassType.Id = classType.Id;
                    listClassType.Code = classType.Code;
                    listClassType.CLSRegs = classType.CLSRegs;
                    listClassType.CLSBakelite = classType.CLSBakelite;
                    listClassType.CLSAcpt = classType.CLSAcpt;
                    listClassType.CLSCloth = classType.CLSCloth;
                    listClassType.CurrentDateClassType = classType.CurrentDateClassType;
                    listClassType.StageId=classType.StageId;
                    listClassType.StageName= _unitOfWork.stages.GetAll().Where(i => i.Id == listClassType.StageId).FirstOrDefault().StageName??"";
                    listClassType.TotalCostClassType= totalcostclasstype;   
                    listClassTypes.Add(listClassType);
                }
                return listClassTypes;
            }
            return new List<ListClassType>();
        }

        public IEnumerable<ReportClassType> GetAllReportTypes()
        {
            List<ReportClassType> reportClassTypes = new List<ReportClassType>();
            var classtype = _unitOfWork.classTypesSpecial.GetAllWithInclude(i=>i.Amounts);
            var classtypename = _unitOfWork.classTypes.GetAll();
            var stages = _unitOfWork.stages.GetAll();
            var installments = _unitOfWork.installments.GetAll();
            
            var ViewAlltables = (from ct in classtype
                                join ctn in classtypename
                                on ct.StageId equals ctn.StageId
                                join s in stages
                                on ctn.StageId equals s.Id
                                
                                select new ReportClassType()
                                {
                                    Id=ct.Id,
                                    StageName = s.StageName,
                                    CLSAcpt = ct.CLSAcpt,
                                    CLSBakelite = ct.CLSBakelite,
                                    CLSCloth = ct.CLSCloth,
                                    CLSRegs = ct.CLSRegs,
                                    ClassTypeName = ctn.Name,
                                    ClassTypeId=ct.Id,
                                    ClassTypeNameId=ctn.Id,
                                    stageId=s.Id
                              
                                    
                                }).ToList();
            ReportClassType reportClassType = null;
            foreach (var item in ViewAlltables)
            {
                var priceamount = classtype.
                    FirstOrDefault(i => i.StageId == item.stageId).Amounts.
                    FirstOrDefault(i => i.ClassTypeNameId == item.ClassTypeNameId).AmountPrice;
                var totalprice = priceamount
                    + item.CLSBakelite + item.CLSAcpt + item.CLSCloth + item.CLSRegs;
                 reportClassType = new ReportClassType();
                reportClassType.StageName = item.StageName;
                reportClassType.CLSBakelite=item.CLSBakelite;
                reportClassType.CLSAcpt=item.CLSAcpt;
                reportClassType.CLSCloth=item.CLSCloth;
                reportClassType.CLSRegs = item.CLSRegs;
                reportClassType.ClassTypeName=item.ClassTypeName;
                reportClassType.PriceClassType= priceamount;
                reportClassType.stageId = item.stageId;
                reportClassType.ClassTypeNameId = item.ClassTypeNameId;
                reportClassType.ClassTypeId = item.ClassTypeId;
                reportClassType.NumberOfInstallments = installments.Where(i => i.ClassTypeId == item.ClassTypeNameId).Count() == 0 ? 1: installments.Where(i => i.ClassTypeId == item.ClassTypeNameId).Count();
                reportClassType.TotalAmountClassType = totalprice;
                reportClassType.PriceInstallment = totalprice / reportClassType.NumberOfInstallments;
                reportClassTypes.Add(reportClassType);

            }
           
            return reportClassTypes;

        }

        public ClassType GetbyId(int id)
        {
            return _unitOfWork.classTypesSpecial.GetById(id);
        }

        public ListClassType GetClassTypesById(int id)
        {

            var classTypes = _unitOfWork.classTypesSpecial.GetAllWithInclude(i => i.Amounts).Where(i=>i.Id==id).FirstOrDefault();
            if (classTypes!=null)
            {
       
                ListClassType listClassType = new ListClassType();
                decimal totalcostclasstype = 0;
                List<VWAmoumt> vWAmoumt = new List<VWAmoumt>();
                VWAmoumt wAmoumt = null;
                    foreach (var costClassType in classTypes.Amounts)
                    {
                    wAmoumt=new VWAmoumt();
                        totalcostclasstype += costClassType.AmountPrice;
                    wAmoumt.AmountPrice=costClassType.AmountPrice;  
                    wAmoumt.AmountDate=costClassType.AmountDate;
                    wAmoumt.ClassTypeName = _unitOfWork.classTypes.GetAll().FirstOrDefault(i => i.Id == costClassType.ClassTypeNameId).Name;
                    wAmoumt.ClassTypeNameId=costClassType.ClassTypeNameId;
                    wAmoumt.Id=costClassType.Id;    
                    wAmoumt.ClassTypeId=costClassType.ClassTypeId;  
                    vWAmoumt.Add(wAmoumt);
                    }
                    listClassType.amounts=vWAmoumt;
                    listClassType.Id = classTypes.Id;
                    listClassType.Code = classTypes.Code;
                    listClassType.CLSRegs = classTypes.CLSRegs;
                    listClassType.CLSBakelite = classTypes.CLSBakelite;
                    listClassType.CLSAcpt = classTypes.CLSAcpt;
                    listClassType.CLSCloth = classTypes.CLSCloth;
                    listClassType.CurrentDateClassType = classTypes.CurrentDateClassType;
                    listClassType.StageId = classTypes.StageId;
                    listClassType.StageName = _unitOfWork.stages.GetAll().Where(i => i.Id == classTypes.StageId).FirstOrDefault().StageName ?? "";
                    listClassType.TotalCostClassType = totalcostclasstype;
                   
                
                return listClassType;
            }
            return new ListClassType();
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

        public ReportClassType GetReportClassTypeById(int id)
        {
            List<ReportClassType> reportClassTypes = new List<ReportClassType>();
            var classtype = _unitOfWork.classTypesSpecial.GetAllWithInclude(i => i.Amounts);
            var classtypename = _unitOfWork.classTypes.GetAll();
            var stages = _unitOfWork.stages.GetAll();
            var installments = _unitOfWork.installments.GetAll();

            var ViewAlltables = (from ct in classtype
                                 join ctn in classtypename
                                 on ct.StageId equals ctn.StageId
                                 join s in stages
                                 on ctn.StageId equals s.Id

                                 select new ReportClassType()
                                 {
                                     Id = ct.Id,
                                     StageName = s.StageName,
                                     CLSAcpt = ct.CLSAcpt,
                                     CLSBakelite = ct.CLSBakelite,
                                     CLSCloth = ct.CLSCloth,
                                     CLSRegs = ct.CLSRegs,
                                     ClassTypeName = ctn.Name,
                                     ClassTypeId = ct.Id,
                                     ClassTypeNameId = ctn.Id,
                                     stageId = s.Id


                                 }).ToList();
            ReportClassType reportClassType = null;
            foreach (var item in ViewAlltables)
            {
                var priceamount = classtype.
                    FirstOrDefault(i => i.StageId == item.stageId).Amounts.
                    FirstOrDefault(i => i.ClassTypeNameId == item.ClassTypeNameId).AmountPrice;
                var totalprice = priceamount
                    + item.CLSBakelite + item.CLSAcpt + item.CLSCloth + item.CLSRegs;
                reportClassType = new ReportClassType();
                reportClassType.StageName = item.StageName;
                reportClassType.CLSBakelite = item.CLSBakelite;
                reportClassType.CLSAcpt = item.CLSAcpt;
                reportClassType.CLSCloth = item.CLSCloth;
                reportClassType.CLSRegs = item.CLSRegs;
                reportClassType.ClassTypeName = item.ClassTypeName;
                reportClassType.PriceClassType = priceamount;
                reportClassType.stageId = item.stageId;
                reportClassType.ClassTypeNameId = item.ClassTypeNameId;
                reportClassType.ClassTypeId = item.ClassTypeId;
                reportClassType.NumberOfInstallments = installments.Where(i => i.ClassTypeId == item.ClassTypeNameId).Count() == 0 ? 1 : installments.Where(i => i.ClassTypeId == item.ClassTypeNameId).Count();
                reportClassType.TotalAmountClassType = totalprice;
                reportClassType.PriceInstallment = totalprice / reportClassType.NumberOfInstallments;
                reportClassTypes.Add(reportClassType);

            }
            return reportClassTypes.FirstOrDefault(i => i.Id == id);
        }
    }
}
