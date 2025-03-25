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

        public List<AccountStatementDetails> GetAccountStatement(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly? ToDate,int? studentId)
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

            decimal totalamount = 0;
            List<AccountStatementDetails> accountStatements = new List<AccountStatementDetails>();
            AccountStatementDetails accountStatementDetails = null;
            foreach (var student in studentsfliter)
            {
                accountStatementDetails = new AccountStatementDetails();
                accountStatementDetails.StudentId = student.StudentNumber;
                accountStatementDetails.StudentName = $"{student.StudentName} {student.FatherName} {student.GrandFatherName} {student.FamilyName}";


                accountStatementDetails.AcountName = "رسوم الدراسية";
                accountStatementDetails.AccountDate = student.RegistrationDate;
                accountStatementDetails.Fees = student.ReceiptTotalFees;
                accountStatementDetails.RamaingPayment = student.ReceiptTotalFees;
                totalamount = student.ReceiptTotalFees;
                accountStatements.Add(accountStatementDetails);
                if (student.LastBalance != 0)
                {
                    AccountStatementDetails accountStatementDetail2 = new AccountStatementDetails();
                    accountStatementDetail2.StudentId = student.StudentNumber;
                    accountStatementDetail2.StudentName = $"{student.StudentName} {student.FatherName} {student.GrandFatherName} {student.FamilyName}";

                    accountStatementDetail2.AcountName = "الرصيد السابق ";
                    accountStatementDetail2.ReceiptIdOrName = "ايصال";
                    accountStatementDetail2.AccountDate = student.RegistrationDate;
                    accountStatementDetail2.LastBalance = student.LastBalance;
                    accountStatementDetail2.Payment= student.LastBalance;
                    totalamount -= student.LastBalance;
                    accountStatementDetail2.RamaingPayment = totalamount;

                    accountStatements.Add(accountStatementDetail2);
                }
               if(student.AdvanceRepayment != 0)
                {
                    AccountStatementDetails accountStatementDetails3 = new AccountStatementDetails();
                    accountStatementDetails3.StudentId = student.StudentNumber;
                    accountStatementDetails3.StudentName = $"{student.StudentName} {student.FatherName} {student.GrandFatherName} {student.FamilyName}";

                    accountStatementDetails3.AcountName = "دفعة مقدمة  ";
                    accountStatementDetails3.ReceiptIdOrName = student.StudentNumber.ToString();
                    accountStatementDetails3.LastBalance = student.LastBalance;
                    accountStatementDetails3.AccountDate = student.RegistrationDate;
                    accountStatementDetails3.Payment = student.AdvanceRepayment;
                    totalamount -= student.AdvanceRepayment;
                    accountStatementDetails3.RamaingPayment = totalamount;
                    accountStatements.Add(accountStatementDetails3);
                }
               
                var receipts=_unitOfWork.receipts.GetAll().Where(i=>i.StudentId==student.Id);
                if(receipts .Any())
                {
                    AccountStatementDetails accountStatementDetail = null;
                    foreach (var receipt in receipts)
                    {
                        accountStatementDetail = new AccountStatementDetails();
                        accountStatementDetail.StudentId = student.StudentNumber;
                        accountStatementDetail.StudentName = $"{student.StudentName} {student.FatherName} {student.GrandFatherName} {student.FamilyName}";

                       
                        accountStatementDetail.AcountName = "  سند صرف";
                        accountStatementDetail.ReceiptIdOrName = receipt.Id.ToString();
                        accountStatementDetail.Payment = receipt.Amount;
                        accountStatementDetail.LastBalance = student.LastBalance;
                        totalamount -= receipt.Amount;
                        accountStatementDetail.RamaingPayment=totalamount;
                        accountStatementDetail.AccountDate = receipt.ReceiptDate;
                        accountStatements.Add(accountStatementDetail);
                    }
                }
                var payments = _unitOfWork.payments.GetAll().Where(i => i.StudentId == student.Id);
                if (payments.Any())
                {
                    AccountStatementDetails accountStatementpayment = null;
                    foreach (var pay in payments)
                    {
                        accountStatementpayment = new AccountStatementDetails();
                        accountStatementpayment.StudentId = student.StudentNumber;
                        accountStatementpayment.StudentName = $"{student.StudentName} {student.FatherName} {student.GrandFatherName} {student.FamilyName}";


                        accountStatementpayment.AcountName = "  سند قبض";
                        accountStatementpayment.ReceiptIdOrName = pay.Id.ToString();
                        accountStatementpayment.AmountReturn = pay.Amount;
                        accountStatementpayment.LastBalance = student.LastBalance;
                        totalamount += pay.Amount;
                        accountStatementpayment.RamaingPayment = totalamount  ;
                        accountStatementpayment.AccountDate = pay.PaymentDate;
                        accountStatements.Add(accountStatementpayment);

                    }
                }
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
                               && (!ClassTypeId.HasValue || s.ClassTypeId == ClassTypeId))


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
    }
}
