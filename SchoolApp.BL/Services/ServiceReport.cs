using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.Repositories.UnitOfWork;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.BL.Services
{
    public class ServiceReport : IServiceReport
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceReport(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<AccountStatement>  GetAccountStatement(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly? ToDate,int? studentId)
        {
            var students = _unitOfWork.students.GetAll().ToList();
            var studentsfliter = students
                      .Where(s => (!DepartmentId.HasValue || s.DepartmentId == DepartmentId)
                               && (!StageId.HasValue || s.StageId == StageId)
                               && (!studentId.HasValue || s.Id == studentId)
                               && (!ClassTypeId.HasValue || s.ClassTypeId == ClassTypeId)
                               && (!FromStudentNumber.HasValue || s.StudentNumber >= FromStudentNumber)
                               && (!ToStudentNumber.HasValue || s.StudentNumber <= ToStudentNumber)
                               && (!FromDate.HasValue || s.BrithDate >= FromDate.Value)
                               
                               && (!ToDate.HasValue || s.BrithDate <= ToDate.Value)).ToList();
           
            List<AccountStatement> accountStatements = new List<AccountStatement>();
            List<AccountStatementDetails> accountStatementDetails = null;
            AccountStatement accountStatement = null;
            List<StudentsClassType> studentsClassTypes = _unitOfWork.studentClassType.GetAll().ToList();
            StudentsClassType studentsClassType = null;
            var classType=_unitOfWork.classTypes.GetAll().ToList(); 
            string ClassTypeName = "";
            foreach (var student in studentsfliter)
            {
                decimal lastbalance = 0;
                decimal totalamount = 0;
                accountStatement = new AccountStatement();
                accountStatementDetails = new List<AccountStatementDetails>();
                AccountStatementDetails accountStatementDetails1 = null;
                var studensclasstypes = studentsClassTypes.Where(i => i.StudentId == student.Id);
                foreach (var studentclasstype in studensclasstypes)
                {
                   
                    
                    accountStatement.StudentId = studentclasstype.StudentNumber;
                    accountStatement.StudentName = studentclasstype.StudentName;
                    ClassTypeName = classType.FirstOrDefault(i => i.Id == studentclasstype.ClassTypeId).Name;
                     accountStatementDetails1 = new AccountStatementDetails();
                    accountStatementDetails1.AcountName = "رسوم الدراسية:"+ ClassTypeName;
                    accountStatementDetails1.AccountDate = (DateOnly)studentclasstype.TransferringDate;
                    accountStatementDetails1.Fees = studentclasstype.ReceiptTotalFees ;
                    totalamount += studentclasstype.ReceiptTotalFees;
                    accountStatementDetails1.RamaingPayment = totalamount;
                    accountStatementDetails1.LastBalance =0;
                    accountStatementDetails1.ReceiptIdOrName = studentclasstype.StudentNumber.ToString();
                    accountStatementDetails.Add(accountStatementDetails1);
                    if (studentclasstype.LastBalance > 0)
                    {
                        AccountStatementDetails accountStatementLastBalance = new AccountStatementDetails();
                        accountStatementLastBalance.AcountName= "الرصيد السابق ";
                        accountStatementLastBalance.ReceiptIdOrName = "ايصال";
                        accountStatementLastBalance.AccountDate = studentclasstype.RegistrationDate;
                        accountStatementLastBalance.LastBalance = studentclasstype.LastBalance;
                        accountStatementLastBalance.Payment = studentclasstype.LastBalance;
                        totalamount -= studentclasstype.LastBalance;
                        lastbalance += studentclasstype.LastBalance;
                        accountStatementLastBalance.RamaingPayment = totalamount;

                        accountStatementDetails.Add(accountStatementLastBalance);
                    }
                    
                }
               
                //if (student.LastBalance != 0)
                //{
                //    AccountStatementDetails accountStatementDetail2 = new AccountStatementDetails();
                   
                //    accountStatementDetail2.AcountName = "الرصيد السابق ";
                //    accountStatementDetail2.ReceiptIdOrName = "ايصال";
                //    accountStatementDetail2.AccountDate = student.RegistrationDate;
                    
                //    totalamount -= student.LastBalance;
                //    accountStatementDetail2.LastBalance = student.LastBalance;
                //    accountStatementDetail2.Payment= student.LastBalance;
                //    totalamount -= student.LastBalance;
                //    accountStatementDetail2.RamaingPayment = totalamount;

                //    accountStatementDetails.Add(accountStatementDetail2);
                //}
              
                var receipts=_unitOfWork.receipts.GetAll().Where(i=>i.StudentId==student.Id);
                if(receipts .Any())
                {
                    AccountStatementDetails accountStatementDetail = null;
                    foreach (var receipt in receipts)
                    {
                        accountStatementDetail = new AccountStatementDetails();
                       
                       
                        accountStatementDetail.AcountName = "  سند صرف";
                        accountStatementDetail.ReceiptIdOrName = receipt.Id.ToString();
                        accountStatementDetail.Payment = receipt.Amount;
                        accountStatementDetail.LastBalance = 0;
                        totalamount -= receipt.Amount;
                        accountStatementDetail.RamaingPayment=totalamount;
                        accountStatementDetail.AccountDate = receipt.ReceiptDate;
                        accountStatementDetails.Add(accountStatementDetail);
                    }
                }
                var payments = _unitOfWork.payments.GetAll().Where(i => i.StudentId == student.Id);
                if (payments.Any())
                {
                    AccountStatementDetails accountStatementpayment = null;
                    foreach (var pay in payments)
                    {
                        accountStatementpayment = new AccountStatementDetails();


                        accountStatementpayment.AcountName = "  سند قبض";
                        accountStatementpayment.ReceiptIdOrName = pay.Id.ToString();
                        accountStatementpayment.AmountReturn = pay.Amount;
                        accountStatementpayment.LastBalance = 0;
                        //totalamount += pay.Amount;
                        accountStatementpayment.RamaingPayment = totalamount;
                        accountStatementpayment.AccountDate = pay.PaymentDate;
                        accountStatementDetails.Add(accountStatementpayment);

                    }
                }
                accountStatement.Details = accountStatementDetails;
                accountStatements.Add(accountStatement);
            }
            return accountStatements;
        }

        public IEnumerable<ClassTypeName> GetAllClassTypeNames()
        {
            return _unitOfWork.classTypes.GetAll().ToList();
        }

        public IEnumerable<Department> GetAllDepartments()
        {
           return _unitOfWork.departments.GetAll().ToList();
        }

        public IEnumerable<Stage> GetAllStages()
        {
            return _unitOfWork.stages.GetAll().ToList();    
        }

        public List<VWReportStudent> GetAllStudentFeesReport(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly? ToDate)
        {
            var students = (from student in _unitOfWork.students.GetAll()
                            join stage in _unitOfWork.stages.GetAll()

                            on student.StageId equals stage.Id
                            join classTypeName in _unitOfWork.classTypes.GetAll()
                            on student.ClassTypeId equals classTypeName.Id
                            select new // ✅ Create an object to include both
                            {
                                student,
                                stage,
                                classTypeName
                            }).ToList();
            var studentsfliter = students
                      .Where(s => (!DepartmentId.HasValue || s.student.DepartmentId == DepartmentId)
                               && (!StageId.HasValue || s.student.StageId == StageId)
                               && (!ClassTypeId.HasValue || s.student.ClassTypeId == ClassTypeId)
                               && (!FromStudentNumber.HasValue || s.student.StudentNumber >= FromStudentNumber)
                               && (!ToStudentNumber.HasValue || s.student.StudentNumber <= ToStudentNumber)
                               && (!FromDate.HasValue || s.student.BrithDate >= FromDate.Value)
                               && (!ToDate.HasValue || s.student.BrithDate <= ToDate.Value))
                                   .Select(s => new VWReportStudent // Fix return type
                                   {
                                       StudentName = $"{s.student.StudentName} {s.student.FatherName} {s.student.GrandFatherName} {s.student.FamilyName}",
                                       StudentNumber = s.student.StudentNumber,
                                       StageName = s.stage.StageName,
                                       ClassTypeName = s.classTypeName.Name,
                                       ClassName = s.student.ClassTypeName,
                                       BusFees=s.student.CostFirstTermAfterDiscount+s.student.CostSecondTermAfterDiscount,
                                       ReceiptTotalFees=s.student.ReceiptTotalFees,
                                       ReceiptTotalPayments=s.student.ReceiptTotalPayments,
                                       LastBalance=s.student.LastBalance
                                   }).ToList();
            return studentsfliter;

        }

        public List<Student> GetAllStudents()
        {
            return _unitOfWork.students.GetAll().ToList();
        }

        public List<VWReportStudent> GetAllStudentsNameReport(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly? ToDate)
        {
            var students = (from student in _unitOfWork.students.GetAll()
                            join stage in _unitOfWork.stages.GetAll()

                            on student.StageId equals stage.Id
                            join classTypeName in _unitOfWork.classTypes.GetAll()
                            on student.ClassTypeId equals classTypeName.Id
                            select new // ✅ Create an object to include both
                            {
                                student,
                                stage,
                                classTypeName
                            }).ToList();
                                  var studentsfliter= students
                      .Where(s => (!DepartmentId.HasValue || s.student.DepartmentId == DepartmentId)
                               && (!StageId.HasValue || s.student.StageId == StageId)
                               && (!ClassTypeId.HasValue || s.student.ClassTypeId == ClassTypeId)
                               && (!FromStudentNumber.HasValue || s.student.StudentNumber >= FromStudentNumber)
                               && (!ToStudentNumber.HasValue || s.student.StudentNumber <= ToStudentNumber)
                               && (!FromDate.HasValue || s.student.BrithDate >= FromDate.Value)
                               && (!ToDate.HasValue || s.student.BrithDate <= ToDate.Value))
                                   .Select(s => new VWReportStudent // Fix return type
                                  {
                                      StudentNumber = s.student.StudentNumber,
                                      StudentName = $"{s.student.StudentName} {s.student.FatherName} {s.student.GrandFatherName} {s.student.FamilyName}",
                                      FatherName= $"{s.student.FatherName} {s.student.GrandFatherName} {s.student.FamilyName}",
                                       StageName = s.stage.StageName,
                                      ClassTypeName = s.classTypeName.Name,
                                      ClassName = s.student.ClassTypeName,
                                      GuardianMobile = s.student.GuardianMobile,
                                      HouseMobileGuardian = s.student.HouseMobileGuardian,
                                       HouseLocationGuardian = s.student.HouseLocationGuardian,
                                       NameOfClosestPerson = s.student.NameOfClosestPerson,
                                       GuardianJob = s.student.GuardianJob,
                                       GuardianWork= s.student.GuardianWork,    
                                       MotherJob=s.student.MotherJob,
                                       MotherMobilePhone=s.student.MotherMobilePhone,
                                       MotherWorkMobile=s.student.MotherWorkMobile,
                                       MotherStudentName=s.student.MotherStudentName
                                   })
                                  .ToList();

            return studentsfliter;
        }


        public ClassTypeName GetbyIdClassTypeName(int id)
        {
            return _unitOfWork.classTypes.GetById(id);
        }

        public Department GetbyIdDepartments(int id)
        {
            return _unitOfWork.departments.GetById(id);
        }

        public Stage GetbyIdStage(int id)
        {
            return _unitOfWork.stages.GetById(id);
        }

        public List<ReportClassType> GetFeeStudent( int? StageId, int? ClassTypeId)
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


                                 }).Where(s =>
                               (!StageId.HasValue || s.stageId == StageId)
                               && (!ClassTypeId.HasValue || s.ClassTypeNameId == ClassTypeId))


                                 .ToList();
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

            return reportClassTypes;
        }

        public List<VWReportPayments> GetPaymentStudent(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly? ToDate, int? studentId)
        {
            var payments = _unitOfWork.payments.GetAll();
            var paymentsfliter = payments
                     .Where(s => (!studentId.HasValue || s.StudentId == studentId) &&
                     (!FromDate.HasValue || s.PaymentDate >= FromDate.Value)
                               && (!ToDate.HasValue || s.PaymentDate <= ToDate.Value)
                             ).ToList();

           List <VWReportPayments> vWReportPayments = new List<VWReportPayments>();
            VWReportPayments wReportPayment = null;
            foreach(var pay in payments)
            {
                wReportPayment = new VWReportPayments();
                var student = _unitOfWork.students.GetById(pay.StudentId);
                if (student != null) 
                {
                    wReportPayment.studentNumber = student.StudentNumber.ToString();
                }
                else
                {
                    wReportPayment.studentNumber = "تم حذف الطالب";
                }
               
                wReportPayment.StudentName = pay.StudentName;
                wReportPayment.PaymentDate=pay.PaymentDate;
                wReportPayment.AmountPayment = pay.Amount;
                wReportPayment.PaymentId = pay.Code;
                vWReportPayments.Add(wReportPayment);   
            }
            return vWReportPayments;
        }

        public List<VWStudentCompleteFees> GetStudentCompleteFees(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly? ToDate)
        {
            var students = (from student in _unitOfWork.students.GetAll()
                            join stage in _unitOfWork.stages.GetAll()

                            on student.StageId equals stage.Id
                            join classTypeName in _unitOfWork.classTypes.GetAll()
                            on student.ClassTypeId equals classTypeName.Id
                            select new // ✅ Create an object to include both
                            {
                                student,
                                stage,
                                classTypeName
                            }).ToList();
            var studentsfliter = students
                      .Where(s => (!DepartmentId.HasValue || s.student.DepartmentId == DepartmentId)
                               && (!StageId.HasValue || s.student.StageId == StageId)
                               && (!ClassTypeId.HasValue || s.student.ClassTypeId == ClassTypeId)
                               && (!FromStudentNumber.HasValue || s.student.StudentNumber >= FromStudentNumber)
                               && (!ToStudentNumber.HasValue || s.student.StudentNumber <= ToStudentNumber)
                               && (!FromDate.HasValue || s.student.BrithDate >= FromDate.Value)
                               && (!ToDate.HasValue || s.student.BrithDate <= ToDate.Value))

                                   .Select(s => new VWStudentCompleteFees // Fix return type
                                   {
                                       StudentName = $"{s.student.StudentName} {s.student.FatherName} {s.student.GrandFatherName} {s.student.FamilyName}",
                                       StudentNumber = s.student.StudentNumber,
                                       StageName = s.stage.StageName,
                                       ClassTypeName = s.classTypeName.Name,
                                      Fees=s.student.ReceiptTotalFees,
                                      Payments=s.student.ReceiptTotalPayments
                                   }).Where(i=>i.Fees<=i.Payments).ToList();
            return studentsfliter;
        }

        public List<VWStudentCompleteFees> GetStudentNoFees(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly? ToDate)
        {
            var students = (from student in _unitOfWork.students.GetAll()
                            join stage in _unitOfWork.stages.GetAll()

                            on student.StageId equals stage.Id
                            join classTypeName in _unitOfWork.classTypes.GetAll()
                            on student.ClassTypeId equals classTypeName.Id
                            select new // ✅ Create an object to include both
                            {
                                student,
                                stage,
                                classTypeName
                            }).ToList();
            var studentsfliter = students
                      .Where(s => (!DepartmentId.HasValue || s.student.DepartmentId == DepartmentId)
                               && (!StageId.HasValue || s.student.StageId == StageId)
                               && (!ClassTypeId.HasValue || s.student.ClassTypeId == ClassTypeId)
                               && (!FromStudentNumber.HasValue || s.student.StudentNumber >= FromStudentNumber)
                               && (!ToStudentNumber.HasValue || s.student.StudentNumber <= ToStudentNumber)
                               && (!FromDate.HasValue || s.student.BrithDate >= FromDate.Value)
                               && (!ToDate.HasValue || s.student.BrithDate <= ToDate.Value))

                                   .Select(s => new VWStudentCompleteFees // Fix return type
                                   {
                                       Id=s.student.Id, 
                                       StudentName = $"{s.student.StudentName} {s.student.FatherName} {s.student.GrandFatherName} {s.student.FamilyName}",
                                       StudentNumber = s.student.StudentNumber,
                                       StageName = s.stage.StageName,
                                       ClassTypeName = s.classTypeName.Name,
                                       Fees = s.student.ReceiptTotalFees,
                                       Payments = s.student.ReceiptTotalPayments
                                   }).Where(i => i.Payments == 0).ToList();
            return studentsfliter;
        }

        public List<VWStudentCompleteFees> GetStudentPartFees(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly? ToDate)
        {
            var students = (from student in _unitOfWork.students.GetAll()
                            join stage in _unitOfWork.stages.GetAll()

                            on student.StageId equals stage.Id
                            join classTypeName in _unitOfWork.classTypes.GetAll()
                            on student.ClassTypeId equals classTypeName.Id
                            select new // ✅ Create an object to include both
                            {
                                student,
                                stage,
                                classTypeName
                            }).ToList();
            var studentsfliter = students
                      .Where(s => (!DepartmentId.HasValue || s.student.DepartmentId == DepartmentId)
                               && (!StageId.HasValue || s.student.StageId == StageId)
                               && (!ClassTypeId.HasValue || s.student.ClassTypeId == ClassTypeId)
                               && (!FromStudentNumber.HasValue || s.student.StudentNumber >= FromStudentNumber)
                               && (!ToStudentNumber.HasValue || s.student.StudentNumber <= ToStudentNumber)
                               && (!FromDate.HasValue || s.student.BrithDate >= FromDate.Value)
                               && (!ToDate.HasValue || s.student.BrithDate <= ToDate.Value))

                                   .Select(s => new VWStudentCompleteFees // Fix return type
                                   {
                                       Id = s.student.Id,
                                       StudentName = $"{s.student.StudentName} {s.student.FatherName} {s.student.GrandFatherName} {s.student.FamilyName}",
                                       StudentNumber = s.student.StudentNumber,
                                       StageName = s.stage.StageName,
                                       ClassTypeName = s.classTypeName.Name,
                                       Fees = s.student.ReceiptTotalFees,
                                       Payments = s.student.ReceiptTotalPayments
                                   }).Where(i => i.Payments >= 0).ToList();
            return studentsfliter;
        }

        public async Task<string> TraneferringClasses(int fromStage, int fromClass, int toStage, int toClass)
        {
            try
            {
                List<InstallmentCostAfterDiscount> InstallmentCostAfterDiscounts = new List<InstallmentCostAfterDiscount>();
                List<InstallmentCostBeforeDiscount> InstallmentCostBeforeDiscounts = new List<InstallmentCostBeforeDiscount>();
                int installmentCount = 1;
                var installments = _unitOfWork.installments
                .GetAll()
                
                .ToList();
                var installment = new List<Installment>();
                var students = _unitOfWork.students
   .GetAllWithInclude(
       e => e.installmentCostBeforeDiscounts,
       e => e.installmentCostAfterDiscounts
   )
   .Where(e => e.StageId == fromStage && e.ClassTypeId == fromClass)
   .ToList();
                var classType=_unitOfWork.classTypesSpecial.GetAllWithInclude(e=>e.Amounts).ToList();
                var classTypeStudent =new ClassType();
                var Amount = new Amount();
                decimal totalCostAmountStudent = 0;
                StudentsClassType studentsClassType = null;
                foreach (var student in students)
                {
                    

                    student.StageId = toStage;
                    student.ClassTypeId = toClass;
                     classTypeStudent = classType.FirstOrDefault(e => e.StageId == student.StageId);
                    Amount=classTypeStudent.Amounts.FirstOrDefault(e=>e.ClassTypeNameId==student.ClassTypeId);
                    totalCostAmountStudent = Amount.AmountPrice ;

                    installment = installments.Where(e => e.StageId == student.StageId && e.ClassTypeId == student.ClassTypeId).ToList();
                    installmentCount = installment.Count();
                    decimal TotalCostClassTypeTransferring = 0;
                    TotalCostClassTypeTransferring= Amount.AmountPrice;
                    if (installmentCount > 0)
                    {
                        foreach (var instcostaftDis in installment)
                        {
                            InstallmentCostAfterDiscount InstallmentCostAfterDiscount = new InstallmentCostAfterDiscount();
                            InstallmentCostAfterDiscount.CostInstallment = totalCostAmountStudent/installmentCount;
                            InstallmentCostAfterDiscount.InstallmentId = instcostaftDis.Id;
                            InstallmentCostAfterDiscount.StageId = student.StageId;
                            InstallmentCostAfterDiscount.ClassTypeId = student.ClassTypeId;
                            InstallmentCostAfterDiscount.InstallmentName = instcostaftDis.InstallName;
                            InstallmentCostAfterDiscounts.Add(InstallmentCostAfterDiscount);
                        }

                        foreach (var instcostaftDis in installment)
                        {
                            InstallmentCostBeforeDiscount InstallmentCostBeforeDiscount = new InstallmentCostBeforeDiscount();
                            InstallmentCostBeforeDiscount.CostInstallment = totalCostAmountStudent / installmentCount;
                            InstallmentCostBeforeDiscount.InstallmentId = instcostaftDis.Id;
                            InstallmentCostBeforeDiscount.StageId = student.StageId;
                            InstallmentCostBeforeDiscount.ClassTypeId = student.ClassTypeId;
                            InstallmentCostBeforeDiscount.InstallmentName = instcostaftDis.InstallName;
                            InstallmentCostBeforeDiscounts.Add(InstallmentCostBeforeDiscount);
                        }
                        student.TotalCost = totalCostAmountStudent;
                        student.CommunityFundDiscount = student.CommunityFundDiscount;
                        student.EarlyPaymentDiscount = student.EarlyPaymentDiscount;
                        student.EmployeeDiscount = student.EmployeeDiscount;
                        student.GeneralDiscount = student.GeneralDiscount;
                        student.ValueAddedTax = student.ValueAddedTax;
                        student.SiblingsDiscount= student.SiblingsDiscount;
                        student.SpecialDiscount = student.SpecialDiscount;
                        student.BusDiscount = student.BusDiscount;
                        totalCostAmountStudent = totalCostAmountStudent - (student.CommunityFundDiscount + student.EarlyPaymentDiscount +
                            student.SiblingsDiscount + student.SpecialDiscount + student.SpecialDiscount + (student.ValueAddedTax / 100) * student.TotalCost + (student.GeneralDiscount / 100) * student.TotalCost + (student.EmployeeDiscount / 100) * student.TotalCost);
                        if (student.AreYouWantGoWithBusSchool)
                        {
                            decimal BusCostDiscount = (student.BusDiscount / 100) * (student.CostFirstTermAfterDiscount + student.CostSecondTermAfterDiscount);
                            totalCostAmountStudent = totalCostAmountStudent + BusCostDiscount;


                        }
                        student.TotalCostAfterDiscount = totalCostAmountStudent;
                        
                        student.LastBalance = (student.ReceiptTotalPayments - student.ReceiptTotalFees) ;
                        student.ReceiptTotalPayments = student.ReceiptTotalPayments;
                        student.ReceiptTotalFees += totalCostAmountStudent;
                        var classtypeAfterTransferring = classType.Where(i => i.StageId == student.StageId).FirstOrDefault();
                        student.ReceiptTotalFees += (classtypeAfterTransferring.CLSRegs + classtypeAfterTransferring.CLSBakelite + classtypeAfterTransferring.CLSCloth + classtypeAfterTransferring.CLSAcpt);
                        student.RemainingFees = student.ReceiptTotalFees - student.ReceiptTotalPayments;
                        student.CLSAcpt = classtypeAfterTransferring.CLSAcpt;
                        student.CLSBakelite = classtypeAfterTransferring.CLSBakelite;
                        student.CLSCloth = classtypeAfterTransferring.CLSCloth;
                        student.CLSRegs = classtypeAfterTransferring.CLSRegs;
                        _unitOfWork.students.Update(student);
                        studentsClassType = new StudentsClassType();
                        studentsClassType.StudentId = student.Id;
                        studentsClassType.StudentName = $"{student.StudentName} {student.FatherName} {student.GrandFatherName} {student.FamilyName}";
                        studentsClassType.DepartmentId = student.DepartmentId;
                        studentsClassType.StageId = student.StageId;
                        studentsClassType.BrithDate = student.BrithDate;
                        studentsClassType.BrithPlace = student.BrithPlace;
                        studentsClassType.ClassTypeId = student.ClassTypeId;
                        studentsClassType.StudentNumber= student.StudentNumber;
                      
                        studentsClassType.CLSCloth = classtypeAfterTransferring.CLSCloth;
                        studentsClassType.CLSAcpt = classtypeAfterTransferring.CLSAcpt;
                        studentsClassType.CLSBakelite = classtypeAfterTransferring.CLSBakelite;
                        studentsClassType.CLSRegs = classtypeAfterTransferring.CLSRegs;
                        studentsClassType.TotalCost = TotalCostClassTypeTransferring;
                        studentsClassType.CommunityFundDiscount = student.CommunityFundDiscount;
                        studentsClassType.EarlyPaymentDiscount = student.EarlyPaymentDiscount;
                        studentsClassType.EmployeeDiscount = student.EmployeeDiscount;
                        studentsClassType.GeneralDiscount = student.GeneralDiscount;
                        studentsClassType.ValueAddedTax = student.ValueAddedTax;
                        studentsClassType.SiblingsDiscount = student.SiblingsDiscount;
                        studentsClassType.SpecialDiscount = student.SpecialDiscount;
                        studentsClassType.BusDiscount = student.BusDiscount;
                        TotalCostClassTypeTransferring = TotalCostClassTypeTransferring - (studentsClassType.CommunityFundDiscount + studentsClassType.EarlyPaymentDiscount +
                          studentsClassType.SiblingsDiscount + studentsClassType.SpecialDiscount + studentsClassType.SpecialDiscount + (studentsClassType.ValueAddedTax / 100) * student.TotalCost + (studentsClassType.GeneralDiscount / 100) * student.TotalCost + (studentsClassType.EmployeeDiscount / 100) * studentsClassType.TotalCost);

                        if (studentsClassType.AreYouWantGoWithBusSchool)
                        {
                            decimal BusCostDiscount = (studentsClassType.BusDiscount / 100) * (studentsClassType.CostFirstTermAfterDiscount + studentsClassType.CostSecondTermAfterDiscount);
                            TotalCostClassTypeTransferring = TotalCostClassTypeTransferring + BusCostDiscount;
                        }
                        studentsClassType.TotalCostAfterDiscount = TotalCostClassTypeTransferring;
                        studentsClassType.LastBalance = student.LastBalance;
                        studentsClassType.ReceiptTotalPayments = student.ReceiptTotalPayments;
                        studentsClassType.ReceiptTotalFees += TotalCostClassTypeTransferring;

                        studentsClassType.ReceiptTotalFees += (studentsClassType.CLSRegs + studentsClassType.CLSBakelite + studentsClassType.CLSCloth + studentsClassType.CLSAcpt);
                        studentsClassType.RemainingFees = studentsClassType.ReceiptTotalFees - studentsClassType.ReceiptTotalPayments;
                        studentsClassType.TransferringDate = DateOnly.FromDateTime(DateTime.Now);
                        studentsClassType.RegistrationDate = student.RegistrationDate;
                        _unitOfWork.studentClassType.Add(studentsClassType);
                    }
                    else
                    {
                        student.TotalCost = totalCostAmountStudent;
                        student.CommunityFundDiscount = student.CommunityFundDiscount;
                        student.EarlyPaymentDiscount = student.EarlyPaymentDiscount;
                        student.EmployeeDiscount = student.EmployeeDiscount;
                        student.GeneralDiscount = student.GeneralDiscount;
                        student.ValueAddedTax = student.ValueAddedTax;
                        student.SiblingsDiscount = student.SiblingsDiscount;
                        student.SpecialDiscount = student.SpecialDiscount;
                        student.BusDiscount = student.BusDiscount;
                        student.TotalDiscount= (student.CommunityFundDiscount + student.EarlyPaymentDiscount +
                            student.SiblingsDiscount + student.SpecialDiscount + student.SpecialDiscount + (student.ValueAddedTax / 100) * student.TotalCost + (student.GeneralDiscount / 100) * student.TotalCost + (student.EmployeeDiscount / 100) * student.TotalCost);

                        totalCostAmountStudent = totalCostAmountStudent - (student.CommunityFundDiscount + student.EarlyPaymentDiscount +
                            student.SiblingsDiscount + student.SpecialDiscount + student.SpecialDiscount + (student.ValueAddedTax / 100) * student.TotalCost + (student.GeneralDiscount / 100) * student.TotalCost + (student.EmployeeDiscount / 100) * student.TotalCost);
                        if (student.AreYouWantGoWithBusSchool)
                        {
                            decimal BusCostDiscount = (student.BusDiscount / 100) * (student.CostFirstTermAfterDiscount + student.CostSecondTermAfterDiscount);
                            totalCostAmountStudent = totalCostAmountStudent + BusCostDiscount;


                        }
                        student.TotalCostAfterDiscount = totalCostAmountStudent;
                        var classtypeAfterTransferring = classType.Where(i => i.StageId == student.StageId).FirstOrDefault();
                        student.LastBalance = (student.ReceiptTotalPayments - student.ReceiptTotalFees);
                        student.ReceiptTotalPayments = student.ReceiptTotalPayments;
                        student.ReceiptTotalFees += totalCostAmountStudent;
                        student.CLSAcpt = classtypeAfterTransferring.CLSAcpt;
                        student.CLSBakelite = classtypeAfterTransferring.CLSBakelite;
                        student.CLSCloth = classtypeAfterTransferring.CLSCloth;
                        student.CLSRegs = classtypeAfterTransferring.CLSRegs;
                        student.ReceiptTotalFees += (classtypeAfterTransferring.CLSRegs + classtypeAfterTransferring.CLSBakelite + classtypeAfterTransferring.CLSCloth + classtypeAfterTransferring.CLSAcpt);
                        student.RemainingFees = student.ReceiptTotalFees - student.ReceiptTotalPayments;
                        _unitOfWork.students.Update(student);
                        studentsClassType = new StudentsClassType();
                        studentsClassType.StudentId = student.Id;
                        studentsClassType.StudentName = $"{student.StudentName} {student.FatherName} {student.GrandFatherName} {student.FamilyName}";
                        studentsClassType.DepartmentId = student.DepartmentId;
                        studentsClassType.StageId = student.StageId;
                        studentsClassType.BrithDate = student.BrithDate;
                        studentsClassType.BrithPlace = student.BrithPlace;
                        studentsClassType.ClassTypeId = student.ClassTypeId;
                        studentsClassType.StudentNumber = student.StudentNumber;

                        studentsClassType.CLSCloth = student.CLSCloth;
                        studentsClassType.CLSAcpt = student.CLSAcpt;
                        studentsClassType.CLSBakelite = student.CLSBakelite;
                        studentsClassType.CLSRegs = student.CLSRegs;
                        studentsClassType.TotalCost = TotalCostClassTypeTransferring;
                        studentsClassType.CommunityFundDiscount = student.CommunityFundDiscount;
                        studentsClassType.EarlyPaymentDiscount = student.EarlyPaymentDiscount;
                        studentsClassType.EmployeeDiscount = student.EmployeeDiscount;
                        studentsClassType.GeneralDiscount = student.GeneralDiscount;
                        studentsClassType.ValueAddedTax = student.ValueAddedTax;
                        studentsClassType.SiblingsDiscount = student.SiblingsDiscount;
                        studentsClassType.SpecialDiscount = student.SpecialDiscount;
                        studentsClassType.BusDiscount = student.BusDiscount;
                        studentsClassType.TotalDiscount= (studentsClassType.CommunityFundDiscount + studentsClassType.EarlyPaymentDiscount +
                          studentsClassType.SiblingsDiscount + studentsClassType.SpecialDiscount + studentsClassType.SpecialDiscount + (studentsClassType.GeneralDiscount / 100) * student.TotalCost + (studentsClassType.EmployeeDiscount / 100) * studentsClassType.TotalCost);

                        TotalCostClassTypeTransferring = TotalCostClassTypeTransferring - (studentsClassType.CommunityFundDiscount + studentsClassType.EarlyPaymentDiscount +
                          studentsClassType.SiblingsDiscount + studentsClassType.SpecialDiscount + studentsClassType.SpecialDiscount + (studentsClassType.GeneralDiscount / 100) * student.TotalCost + (studentsClassType.EmployeeDiscount / 100) * studentsClassType.TotalCost);

                        if (studentsClassType.AreYouWantGoWithBusSchool)
                        {
                            decimal BusCostDiscount = (studentsClassType.BusDiscount / 100) * (studentsClassType.CostFirstTermAfterDiscount + studentsClassType.CostSecondTermAfterDiscount);
                            TotalCostClassTypeTransferring = TotalCostClassTypeTransferring + BusCostDiscount;
                        }
                        studentsClassType.CLSCloth = classtypeAfterTransferring.CLSCloth;
                        studentsClassType.CLSAcpt = classtypeAfterTransferring.CLSAcpt;
                        studentsClassType.CLSBakelite = classtypeAfterTransferring.CLSBakelite;
                        studentsClassType.CLSRegs = classtypeAfterTransferring.CLSRegs;

                        studentsClassType.TotalCostAfterDiscount = TotalCostClassTypeTransferring;
                        studentsClassType.LastBalance = student.LastBalance;
                        studentsClassType.ReceiptTotalPayments = student.ReceiptTotalPayments;
                        studentsClassType.ReceiptTotalFees += TotalCostClassTypeTransferring;

                        studentsClassType.ReceiptTotalFees += (studentsClassType.CLSRegs + studentsClassType.CLSBakelite + studentsClassType.CLSCloth + studentsClassType.CLSAcpt);
                        studentsClassType.RemainingFees = studentsClassType.ReceiptTotalFees - studentsClassType.ReceiptTotalPayments;
                        studentsClassType.TransferringDate = DateOnly.FromDateTime(DateTime.Now);
                        studentsClassType.RegistrationDate = student.RegistrationDate;
                        
                        _unitOfWork.studentClassType.Add(studentsClassType);

                    }

                }
                return "تم نقل الصف";
            }
            catch
            {
                return "حدثت مشكلة اثنا نقل الصف";  
            }
           

        }
    }
}
