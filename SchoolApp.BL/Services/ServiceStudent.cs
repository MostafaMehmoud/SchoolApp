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
    public class ServiceStudent : IServiceStudent
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceStudent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string Add(VWStudent VWStudent)
        {
            List<InstallmentCostAfterDiscount> InstallmentCostAfterDiscounts = new List<InstallmentCostAfterDiscount>();
            foreach (var instcostaftDis in VWStudent.installmentCostAfterDiscounts)
            {
                InstallmentCostAfterDiscount InstallmentCostAfterDiscount = new InstallmentCostAfterDiscount();
                InstallmentCostAfterDiscount.CostInstallment = instcostaftDis.CostInstallment;
                InstallmentCostAfterDiscount.InstallmentId = instcostaftDis.InstallmentId;
                InstallmentCostAfterDiscount.StageId = VWStudent.StageId;
                InstallmentCostAfterDiscount.ClassTypeId=VWStudent.ClassTypeId;
                InstallmentCostAfterDiscount.InstallmentName = instcostaftDis.InstallmentName;
                InstallmentCostAfterDiscounts.Add(InstallmentCostAfterDiscount);
            }
            List<InstallmentCostBeforeDiscount> InstallmentCostBeforeDiscounts = new List<InstallmentCostBeforeDiscount>();
            foreach (var instcostaftDis in VWStudent.installmentCostBeforeDiscounts)
            {
                InstallmentCostBeforeDiscount InstallmentCostBeforeDiscount = new InstallmentCostBeforeDiscount();
                InstallmentCostBeforeDiscount.CostInstallment = instcostaftDis.CostInstallment;
                InstallmentCostBeforeDiscount.InstallmentId = instcostaftDis.InstallmentId;
                InstallmentCostBeforeDiscount.StageId=VWStudent.StageId;
                InstallmentCostBeforeDiscount.ClassTypeId = VWStudent.ClassTypeId;
                InstallmentCostBeforeDiscount.InstallmentName=instcostaftDis.InstallmentName;
                InstallmentCostBeforeDiscounts.Add(InstallmentCostBeforeDiscount);
            }
            Student student = new Student();
            student.installmentCostAfterDiscounts = InstallmentCostAfterDiscounts;
            student.installmentCostBeforeDiscounts=InstallmentCostBeforeDiscounts;
            student.StudentName = VWStudent.StudentName;
            student.StudentNumber=VWStudent.StudentNumber;
            student.FatherName = VWStudent.FatherName;
            student.GrandFatherName=VWStudent.GrandFatherName;  
            student.FamilyName = VWStudent.FamilyName;
            student.DepartmentId = VWStudent.DepartmentId;  
            student.StageId = VWStudent.StageId;
            student.BrithDate = (DateOnly)VWStudent.BrithDate;
            student.BrithPlace= VWStudent.BrithPlace;   
            student.NationalId=VWStudent.NationalId;
            student.GanderType=VWStudent.GanderType;
            student.ClassTypeId = VWStudent.ClassTypeId;
            student.ClassTypeName = VWStudent.ClassTypeName;
            student.LastBalance=VWStudent.LastBalance;
            student.LastSchoolName=VWStudent.LastSchoolName;
            student.AdvanceRepayment=VWStudent.AdvanceRepayment;    
            student.MonthlyInstallment=VWStudent.MonthlyInstallment;    
            student.PersonalStatusStudentId=VWStudent.PersonalStatusStudentId;
            student.PersonalStatusStudentNumber=VWStudent.PersonalStatusStudentNumber;  
            student.PersonalStatusStudentStartDate=(DateOnly)VWStudent.PersonalStatusStudentStartDate;    
            student.PersonalStatusStudentEndDate= (DateOnly)VWStudent.PersonalStatusStudentEndDate;    
            student.PersonalStatusStudentPlace=VWStudent.PersonalStatusStudentPlace;
            student.PaymentTypeId=VWStudent.PaymentTypeId;
            student.EnrollmentPeriodId=VWStudent.EnrollmentPeriodId;
            student.GuardianName=VWStudent.GuardianName;
            student.Kinship=VWStudent.Kinship;
            student.GuardianNationalId=VWStudent.GuardianNationalId;    
            student.GuardianWork=VWStudent.GuardianSideWork;
            student.GuardianJob=VWStudent.GuardianJob;
            student.GuardianMobile=VWStudent.GuardianMobile;
            student.GuardianEmail=VWStudent.GuardianEmail;
            student.GuardianZipCode=VWStudent.GuardianZipCode;
            student.GuardianWorkMobile=VWStudent.GuardianWorkMobile;
            student.NameOfClosestPerson=VWStudent.NameOfClosestPerson;
            student.HouseLocationGuardian=VWStudent.HouseLocationGuardian;
            student.HouseMobileGuardian=VWStudent.HouseMobileGuardian;
            student.GuardianFaxNumber=VWStudent.GuardianFaxNumber;
            student.GuardianWorkingWithUs=VWStudent.GuardianWorkingWithUs;  
            student.PersonalStatusGuardianId=VWStudent.PersonalStatusGuardianId;
            student.PersonalStatusGuardianNumber=VWStudent.PersonalStatusGuardianNumber;    
            student.PersonalStatusGuardianStartDate= (DateOnly)VWStudent.PersonalStatusGuardianStartDate;  
            student.PersonalStatusGuardianEndDate= (DateOnly)VWStudent.PersonalStatusGuardianEndDate;  
            student.PersonalStatusGuardianPlace=VWStudent.PersonalStatusGuardianPlace;  
            student.MotherStudentName=VWStudent.MotherStudentName;
            student.KinshipMother=VWStudent.KinshipMother;
            student.MotherNationalId=VWStudent.MotherNationalId;
            student.MotherWork=VWStudent.MotherWork;
            student.MotherFaxNumber= VWStudent.MotherFaxNumber;
            student.MotherWorkMobile=VWStudent.MotherWorkMobile;
            student.MotherJob=VWStudent.MotherJob;
            student.MotherMobilePhone=VWStudent.MotherMobilePhone;  
            student.Notes=VWStudent.Notes;
            student.CLSCloth=VWStudent.CLSCloth;
            student.CLSAcpt=VWStudent.CLSAcpt;
            student.CLSBakelite=VWStudent.CLSBakelite;
            student.CLSRegs=VWStudent.CLSRegs;
            student.AreYouWantGoWithBusSchool = VWStudent.AreYouWantGoWithBusSchool;
            student.DirectionBus=VWStudent.DirectionBus;
            student.BusId=VWStudent.BusId;
            student.CostFirstTermBeforeDiscount = VWStudent.CostFirstTermBeforeDiscount;
            student.CostSecondTermBeforeDiscount = VWStudent.CostSecondTermBeforeDiscount;
            student.CostSecondTermAfterDiscount=VWStudent.CostSecondTermAfterDiscount;
            student.CostFirstTermAfterDiscount=VWStudent.CostFirstTermAfterDiscount;
            student.GeneralDiscount=VWStudent.GeneralDiscount;
            student.EmployeeDiscount=VWStudent.EmployeeDiscount;
            student.EarlyPaymentDiscount=VWStudent.EarlyPaymentDiscount;
            student.SiblingsDiscount=VWStudent.SiblingsDiscount;
            student.CommunityFundDiscount=VWStudent.CommunityFundDiscount;
            student.SpecialDiscount=VWStudent.SpecialDiscount;
            student.TotalDiscount = VWStudent.TotalDiscount;
            student.BusDiscount=VWStudent.BusDiscount;
            student.TotalCost=VWStudent.TotalCost;
            student.TotalCostAfterDiscount = VWStudent.TotalCostAfterDiscount;

            student.ReceiptTotalFees += student.TotalCostAfterDiscount + student.CostSecondTermAfterDiscount + student.CostFirstTermAfterDiscount + student.LastBalance
                + student.CLSAcpt + student.CLSBakelite + student.CLSCloth + student.CLSRegs;
            foreach(var item in InstallmentCostAfterDiscounts)
            {
                student.ReceiptTotalFees += item.CostInstallment;
            }
            student.ReceiptTotalPayments = 0;
            student.RemainingFees = student.ReceiptTotalFees - student.ReceiptTotalPayments;

            if (_unitOfWork.students.Add(student)) 
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
            if (_unitOfWork.students.Delete(id))
            {
                return "تم الحذف بنجاح";
            }
            else
            {
                return "حدثت مشكلة اثناء الحذف";
            }
        }

        public string Edit(VWStudent VWStudent)
        {
            List<InstallmentCostAfterDiscount> InstallmentCostAfterDiscounts = new List<InstallmentCostAfterDiscount>();
            foreach (var instcostaftDis in VWStudent.installmentCostAfterDiscounts)
            {
                InstallmentCostAfterDiscount InstallmentCostAfterDiscount = new InstallmentCostAfterDiscount();
                InstallmentCostAfterDiscount.Id=instcostaftDis.Id;
                InstallmentCostAfterDiscount.StudentId = InstallmentCostAfterDiscount.StudentId;
                InstallmentCostAfterDiscount.CostInstallment = instcostaftDis.CostInstallment;
                InstallmentCostAfterDiscount.InstallmentId = instcostaftDis.InstallmentId;
                InstallmentCostAfterDiscount.StageId = VWStudent.StageId;
                InstallmentCostAfterDiscount.ClassTypeId = VWStudent.ClassTypeId;
                InstallmentCostAfterDiscount.InstallmentName=instcostaftDis.InstallmentName;
                InstallmentCostAfterDiscounts.Add(InstallmentCostAfterDiscount);
            }
            List<InstallmentCostBeforeDiscount> InstallmentCostBeforeDiscounts = new List<InstallmentCostBeforeDiscount>();
            foreach (var instcostaftDis in VWStudent.installmentCostBeforeDiscounts)
            {
                InstallmentCostBeforeDiscount InstallmentCostBeforeDiscount = new InstallmentCostBeforeDiscount();
                InstallmentCostBeforeDiscount.Id = instcostaftDis.Id;
                InstallmentCostBeforeDiscount.StudentId= InstallmentCostBeforeDiscount.StudentId;
                InstallmentCostBeforeDiscount.CostInstallment = instcostaftDis.CostInstallment;
                InstallmentCostBeforeDiscount.InstallmentId = instcostaftDis.InstallmentId;
                InstallmentCostBeforeDiscount.StageId = VWStudent.StageId;
                InstallmentCostBeforeDiscount.ClassTypeId = VWStudent.ClassTypeId;
                InstallmentCostBeforeDiscount.InstallmentName=instcostaftDis.InstallmentName;   
                InstallmentCostBeforeDiscounts.Add(InstallmentCostBeforeDiscount);
            }
            Student student = new Student();
            student.Id = VWStudent.Id;
            student.installmentCostAfterDiscounts = InstallmentCostAfterDiscounts;
            student.installmentCostBeforeDiscounts = InstallmentCostBeforeDiscounts;
            student.StudentName = VWStudent.StudentName;
            student.StudentNumber = VWStudent.StudentNumber;
            student.FatherName = VWStudent.FatherName;
            student.GrandFatherName = VWStudent.GrandFatherName;
            student.FamilyName = VWStudent.FamilyName;
            student.DepartmentId = VWStudent.DepartmentId;
            student.StageId = VWStudent.StageId;
            student.BrithDate = (DateOnly)VWStudent.BrithDate;
            student.BrithPlace = VWStudent.BrithPlace;
            student.NationalId = VWStudent.NationalId;
            student.GanderType = VWStudent.GanderType;
            student.ClassTypeId = VWStudent.ClassTypeId;
            student.ClassTypeName = VWStudent.ClassTypeName;
            student.LastBalance = VWStudent.LastBalance;
            student.LastSchoolName = VWStudent.LastSchoolName;
            student.AdvanceRepayment = VWStudent.AdvanceRepayment;
            student.MonthlyInstallment = VWStudent.MonthlyInstallment;
            student.PersonalStatusStudentId = VWStudent.PersonalStatusStudentId;
            student.PersonalStatusStudentNumber = VWStudent.PersonalStatusStudentNumber;
            student.PersonalStatusStudentStartDate = (DateOnly)VWStudent.PersonalStatusStudentStartDate;
            student.PersonalStatusStudentEndDate = (DateOnly)VWStudent.PersonalStatusStudentEndDate;
            student.PersonalStatusStudentPlace = VWStudent.PersonalStatusStudentPlace;
            student.PaymentTypeId = VWStudent.PaymentTypeId;
            student.EnrollmentPeriodId = VWStudent.EnrollmentPeriodId;
            student.GuardianName = VWStudent.GuardianName;
            student.Kinship = VWStudent.Kinship;
            student.GuardianNationalId = VWStudent.GuardianNationalId;
            student.GuardianWork = VWStudent.GuardianSideWork;
            student.GuardianJob = VWStudent.GuardianJob;
            student.GuardianMobile = VWStudent.GuardianMobile;
            student.GuardianEmail = VWStudent.GuardianEmail;
            student.GuardianZipCode = VWStudent.GuardianZipCode;
            student.GuardianWorkMobile = VWStudent.GuardianWorkMobile;
            student.NameOfClosestPerson = VWStudent.NameOfClosestPerson;
            student.HouseLocationGuardian = VWStudent.HouseLocationGuardian;
            student.HouseMobileGuardian = VWStudent.HouseMobileGuardian;
            student.GuardianFaxNumber = VWStudent.GuardianFaxNumber;
            student.GuardianWorkingWithUs = VWStudent.GuardianWorkingWithUs;
            student.PersonalStatusGuardianId = VWStudent.PersonalStatusGuardianId;
            student.PersonalStatusGuardianNumber = VWStudent.PersonalStatusGuardianNumber;
            student.PersonalStatusGuardianStartDate = (DateOnly)VWStudent.PersonalStatusGuardianStartDate;
            student.PersonalStatusGuardianEndDate = (DateOnly)VWStudent.PersonalStatusGuardianEndDate;
            student.PersonalStatusGuardianPlace = VWStudent.PersonalStatusGuardianPlace;
            student.MotherStudentName = VWStudent.MotherStudentName;
            student.KinshipMother = VWStudent.KinshipMother;
            student.MotherNationalId = VWStudent.MotherNationalId;
            student.MotherWork = VWStudent.MotherWork;
            student.MotherFaxNumber = VWStudent.MotherFaxNumber;
            student.MotherWorkMobile = VWStudent.MotherWorkMobile;
            student.MotherJob = VWStudent.MotherJob;
            student.MotherMobilePhone = VWStudent.MotherMobilePhone;
            student.Notes = VWStudent.Notes;
            student.CLSCloth = VWStudent.CLSCloth;
            student.CLSAcpt = VWStudent.CLSAcpt;
            student.CLSBakelite = VWStudent.CLSBakelite;
            student.CLSRegs = VWStudent.CLSRegs;
            student.AreYouWantGoWithBusSchool = VWStudent.AreYouWantGoWithBusSchool;
            student.DirectionBus = VWStudent.DirectionBus;
            student.BusId = VWStudent.BusId;
            student.CostFirstTermBeforeDiscount = VWStudent.CostFirstTermBeforeDiscount;
            student.CostSecondTermBeforeDiscount = VWStudent.CostSecondTermBeforeDiscount;
            student.CostSecondTermAfterDiscount = VWStudent.CostSecondTermAfterDiscount;
            student.CostFirstTermAfterDiscount = VWStudent.CostFirstTermAfterDiscount;
            student.GeneralDiscount = VWStudent.GeneralDiscount;
            student.EmployeeDiscount = VWStudent.EmployeeDiscount;
            student.EarlyPaymentDiscount = VWStudent.EarlyPaymentDiscount;
            student.SiblingsDiscount = VWStudent.SiblingsDiscount;
            student.CommunityFundDiscount = VWStudent.CommunityFundDiscount;
            student.SpecialDiscount = VWStudent.SpecialDiscount;
            student.TotalDiscount = VWStudent.TotalDiscount;
            student.BusDiscount = VWStudent.BusDiscount;
            student.TotalCost = VWStudent.TotalCost;
            student.TotalCostAfterDiscount = VWStudent.TotalCostAfterDiscount;
            student.ReceiptTotalFees += student.TotalCostAfterDiscount + student.CostSecondTermAfterDiscount + student.CostFirstTermAfterDiscount + student.LastBalance
              + student.CLSAcpt + student.CLSBakelite + student.CLSCloth + student.CLSRegs;
            foreach (var item in InstallmentCostAfterDiscounts)
            {
                student.ReceiptTotalFees += item.CostInstallment;
            }
            student.ReceiptTotalPayments = 0;
            student.RemainingFees = student.ReceiptTotalFees - student.ReceiptTotalPayments;

            if (_unitOfWork.students.Update(student))
            {
                return "تم التعديل بنجاح";
            }
            else
            {
                return "حدثت مشكلة اثناء التعديل";
            }
        }

        public IEnumerable<Student> GetAll()
        {
            return _unitOfWork.students.GetAll();
        }

        public Student GetbyId(int id)
        {
            return _unitOfWork.students.GetById(id);
        }

        public VWStudentCostReceipt GetCostReceipt(int StudentId)
        {
            var student = _unitOfWork.students.GetAllWithInclude(i=>i.installmentCostAfterDiscounts).FirstOrDefault(i=>i.Id== StudentId);
            VWStudentCostReceipt receipt = new VWStudentCostReceipt();  
            receipt.CLSAcpt=student.CLSAcpt;
            receipt.RemainingFees=student.RemainingFees;
            receipt.CLSRegs=student.CLSRegs;
            receipt.CLSCloth = student.CLSCloth;
            receipt.CLSBakelite = student.CLSBakelite;
            receipt.CostFirstTermAfterDiscount=student.CostFirstTermAfterDiscount;
            receipt.CostSecondTermAfterDiscount = student.CostSecondTermAfterDiscount;
            receipt.StudentName = $"{student.StudentName} {student.FatherName} {student.GrandFatherName} {student.FamilyName}";
            receipt.RemainingFees = student.ReceiptTotalFees;
            receipt.ReceiptTotalFees = student.ReceiptTotalFees;
            receipt.ReceiptTotalPayments = student.ReceiptTotalPayments;
            receipt.StudentId = student.Id;
            receipt.LastBalance = student.LastBalance;
            receipt.TotalCost = student.TotalCostAfterDiscount;
            List<InstallmentForStudent> installmentForStudents = new List<InstallmentForStudent>();
            InstallmentForStudent installmentForStudent = null;
            foreach(var install in student.installmentCostAfterDiscounts)
            {
                installmentForStudent = new InstallmentForStudent();
                installmentForStudent.CostInstallment = install.CostInstallment;
                installmentForStudent.InstallmentId=install.InstallmentId;
                installmentForStudent.InstallmentName=install.InstallmentName;
                installmentForStudent.Id = install.Id;
                installmentForStudents.Add(installmentForStudent);
            }
            receipt.installmentForStudents = installmentForStudents;
            return receipt;


        }

        public async Task<VWStudent> GetMaxStudent()
        {
            var student = await _unitOfWork.students.GetMax();
            List<VWInstallmentCostAfterDiscount> VWInstallmentCostAfterDiscount = new List<VWInstallmentCostAfterDiscount>();
            foreach (var instcostaftDis in student.installmentCostAfterDiscounts)
            {
                VWInstallmentCostAfterDiscount InstallmentCostAfterDiscount = new VWInstallmentCostAfterDiscount();
                InstallmentCostAfterDiscount.Id = instcostaftDis.Id;
               
                InstallmentCostAfterDiscount.CostInstallment = instcostaftDis.CostInstallment;
                InstallmentCostAfterDiscount.InstallmentId = instcostaftDis.InstallmentId;
                InstallmentCostAfterDiscount.InstallmentName=instcostaftDis.InstallmentName;
                VWInstallmentCostAfterDiscount.Add(InstallmentCostAfterDiscount);
            }
            List<VWInstallmentCostBeforeDiscount> VWInstallmentCostBeforeDiscounts = new List<VWInstallmentCostBeforeDiscount>();
            foreach (var instcostaftDis in student.installmentCostBeforeDiscounts)
            {
                VWInstallmentCostBeforeDiscount InstallmentCostBeforeDiscount = new VWInstallmentCostBeforeDiscount();
                InstallmentCostBeforeDiscount.Id = instcostaftDis.Id;
                InstallmentCostBeforeDiscount.CostInstallment = instcostaftDis.CostInstallment;
                InstallmentCostBeforeDiscount.InstallmentId = instcostaftDis.InstallmentId;
                InstallmentCostBeforeDiscount.InstallmentName = instcostaftDis.InstallmentName;
                VWInstallmentCostBeforeDiscounts.Add(InstallmentCostBeforeDiscount);
            }
            VWStudent vwStudent = new VWStudent();
            vwStudent.Id = student.Id;
            vwStudent.installmentCostAfterDiscounts = VWInstallmentCostAfterDiscount;
            vwStudent.installmentCostBeforeDiscounts = VWInstallmentCostBeforeDiscounts;
            vwStudent.StudentName = student.StudentName;
            vwStudent.StudentNumber = student.StudentNumber;
            vwStudent.FatherName = student.FatherName;
            vwStudent.GrandFatherName = student.GrandFatherName;
            vwStudent.FamilyName = student.FamilyName;
            vwStudent.DepartmentId = student.DepartmentId;
            vwStudent.StageId = student.StageId;
            vwStudent.BrithDate = student.BrithDate;
            vwStudent.BrithPlace = student.BrithPlace;
            vwStudent.NationalId = student.NationalId;
            vwStudent.GanderType = student.GanderType;
            vwStudent.ClassTypeId = student.ClassTypeId;
            vwStudent.ClassTypeName = student.ClassTypeName;
            vwStudent.LastBalance = student.LastBalance;
            vwStudent.LastSchoolName = student.LastSchoolName;
            vwStudent.AdvanceRepayment = student.AdvanceRepayment;
            vwStudent.MonthlyInstallment = student.MonthlyInstallment;
            vwStudent.PersonalStatusStudentId = student.PersonalStatusStudentId;
            vwStudent.PersonalStatusStudentNumber = student.PersonalStatusStudentNumber;
            vwStudent.PersonalStatusStudentStartDate = student.PersonalStatusStudentStartDate;
            vwStudent.PersonalStatusStudentEndDate = student.PersonalStatusStudentEndDate;
            vwStudent.PersonalStatusStudentPlace = student.PersonalStatusStudentPlace;
            vwStudent.PaymentTypeId = student.PaymentTypeId;
            vwStudent.EnrollmentPeriodId = student.EnrollmentPeriodId;
            vwStudent.GuardianName = student.GuardianName;
            vwStudent.Kinship = student.Kinship;
            vwStudent.GuardianNationalId = student.GuardianNationalId;
            vwStudent.GuardianSideWork = student.GuardianWork;
            vwStudent.GuardianJob = student.GuardianJob;
            vwStudent.GuardianMobile = student.GuardianMobile;
            vwStudent.GuardianEmail = student.GuardianEmail;
            vwStudent.GuardianZipCode = student.GuardianZipCode;
            vwStudent.GuardianWorkMobile = student.GuardianWorkMobile;
            vwStudent.NameOfClosestPerson = student.NameOfClosestPerson;
            vwStudent.HouseLocationGuardian = student.HouseLocationGuardian;
            vwStudent.HouseMobileGuardian = student.HouseMobileGuardian;
            vwStudent.GuardianFaxNumber = student.GuardianFaxNumber;
            vwStudent.GuardianWorkingWithUs = student.GuardianWorkingWithUs;
            vwStudent.PersonalStatusGuardianId = student.PersonalStatusGuardianId;
            vwStudent.PersonalStatusGuardianNumber = student.PersonalStatusGuardianNumber;
            vwStudent.PersonalStatusGuardianStartDate = student.PersonalStatusGuardianStartDate;
            vwStudent.PersonalStatusGuardianEndDate = student.PersonalStatusGuardianEndDate;
            vwStudent.PersonalStatusGuardianPlace = student.PersonalStatusGuardianPlace;
            vwStudent.MotherStudentName = student.MotherStudentName;
            vwStudent.KinshipMother = student.KinshipMother;
            vwStudent.MotherNationalId = student.MotherNationalId;
            vwStudent.MotherWork = student.MotherWork;
            vwStudent.MotherFaxNumber = student.MotherFaxNumber;
            vwStudent.MotherWorkMobile = student.MotherWorkMobile;
            vwStudent.MotherJob = student.MotherJob;
            vwStudent.MotherMobilePhone = student.MotherMobilePhone;
            vwStudent.Notes = student.Notes;
            vwStudent.CLSCloth = student.CLSCloth;
            vwStudent.CLSAcpt = student.CLSAcpt;
            vwStudent.CLSBakelite = student.CLSBakelite;
            vwStudent.CLSRegs = student.CLSRegs;
            vwStudent.AreYouWantGoWithBusSchool = student.AreYouWantGoWithBusSchool;
            vwStudent.DirectionBus = student.DirectionBus;
            vwStudent.BusId = student.BusId;
            vwStudent.CostFirstTermBeforeDiscount = student.CostFirstTermBeforeDiscount;
            vwStudent.CostSecondTermBeforeDiscount = student.CostSecondTermBeforeDiscount;
            vwStudent.CostSecondTermAfterDiscount = student.CostSecondTermAfterDiscount;
            vwStudent.CostFirstTermAfterDiscount = student.CostFirstTermAfterDiscount;
            vwStudent.GeneralDiscount = student.GeneralDiscount;
            vwStudent.EmployeeDiscount = student.EmployeeDiscount;
            vwStudent.EarlyPaymentDiscount = student.EarlyPaymentDiscount;
            vwStudent.SiblingsDiscount = student.SiblingsDiscount;
            vwStudent.CommunityFundDiscount = student.CommunityFundDiscount;
            vwStudent.SpecialDiscount = student.SpecialDiscount;
            vwStudent.TotalDiscount = student.TotalDiscount;
            vwStudent.BusDiscount = student.BusDiscount;
            vwStudent.TotalCost = student.TotalCost;
            vwStudent.TotalCostAfterDiscount = student.TotalCostAfterDiscount;
            vwStudent.ReceiptTotalFees= student.ReceiptTotalFees;   
            vwStudent.ReceiptTotalPayments=student.ReceiptTotalPayments;    
            vwStudent.RemainingFees=student.RemainingFees;
            return vwStudent;
        }

        public int GetMaxStudentId()
        {
            return _unitOfWork.students.GetMaxIdOfItem();
        }

        public async Task<VWStudent> GetMinStudent()
        {
            var student = await _unitOfWork.students.GetMin();
            List<VWInstallmentCostAfterDiscount> VWInstallmentCostAfterDiscount = new List<VWInstallmentCostAfterDiscount>();
            foreach (var instcostaftDis in student.installmentCostAfterDiscounts)
            {
                VWInstallmentCostAfterDiscount InstallmentCostAfterDiscount = new VWInstallmentCostAfterDiscount();
                InstallmentCostAfterDiscount.Id = instcostaftDis.Id;

                InstallmentCostAfterDiscount.CostInstallment = instcostaftDis.CostInstallment;
                InstallmentCostAfterDiscount.InstallmentId = instcostaftDis.InstallmentId;
                InstallmentCostAfterDiscount.InstallmentName = instcostaftDis.InstallmentName;
                VWInstallmentCostAfterDiscount.Add(InstallmentCostAfterDiscount);
            }
            List<VWInstallmentCostBeforeDiscount> VWInstallmentCostBeforeDiscounts = new List<VWInstallmentCostBeforeDiscount>();
            foreach (var instcostaftDis in student.installmentCostBeforeDiscounts)
            {
                VWInstallmentCostBeforeDiscount InstallmentCostBeforeDiscount = new VWInstallmentCostBeforeDiscount();
                InstallmentCostBeforeDiscount.Id = instcostaftDis.Id;
                InstallmentCostBeforeDiscount.CostInstallment = instcostaftDis.CostInstallment;
                InstallmentCostBeforeDiscount.InstallmentId = instcostaftDis.InstallmentId;
                InstallmentCostBeforeDiscount.InstallmentName= instcostaftDis.InstallmentName;  
                VWInstallmentCostBeforeDiscounts.Add(InstallmentCostBeforeDiscount);
            }
            VWStudent vwStudent = new VWStudent();
            vwStudent.Id = student.Id;
            vwStudent.installmentCostAfterDiscounts = VWInstallmentCostAfterDiscount;
            vwStudent.installmentCostBeforeDiscounts = VWInstallmentCostBeforeDiscounts;
            vwStudent.StudentName = student.StudentName;
            vwStudent.StudentNumber = student.StudentNumber;
            vwStudent.FatherName = student.FatherName;
            vwStudent.GrandFatherName = student.GrandFatherName;
            vwStudent.FamilyName = student.FamilyName;
            vwStudent.DepartmentId = student.DepartmentId;
            vwStudent.StageId = student.StageId;
            vwStudent.BrithDate = student.BrithDate;
            vwStudent.BrithPlace = student.BrithPlace;
            vwStudent.NationalId = student.NationalId;
            vwStudent.GanderType = student.GanderType;
            vwStudent.ClassTypeId = student.ClassTypeId;
            vwStudent.ClassTypeName = student.ClassTypeName;
            vwStudent.LastBalance = student.LastBalance;
            vwStudent.LastSchoolName = student.LastSchoolName;
            vwStudent.AdvanceRepayment = student.AdvanceRepayment;
            vwStudent.MonthlyInstallment = student.MonthlyInstallment;
            vwStudent.PersonalStatusStudentId = student.PersonalStatusStudentId;
            vwStudent.PersonalStatusStudentNumber = student.PersonalStatusStudentNumber;
            vwStudent.PersonalStatusStudentStartDate = student.PersonalStatusStudentStartDate;
            vwStudent.PersonalStatusStudentEndDate = student.PersonalStatusStudentEndDate;
            vwStudent.PersonalStatusStudentPlace = student.PersonalStatusStudentPlace;
            vwStudent.PaymentTypeId = student.PaymentTypeId;
            vwStudent.EnrollmentPeriodId = student.EnrollmentPeriodId;
            vwStudent.GuardianName = student.GuardianName;
            vwStudent.Kinship = student.Kinship;
            vwStudent.GuardianNationalId = student.GuardianNationalId;
            vwStudent.GuardianSideWork = student.GuardianWork;
            vwStudent.GuardianJob = student.GuardianJob;
            vwStudent.GuardianMobile = student.GuardianMobile;
            vwStudent.GuardianEmail = student.GuardianEmail;
            vwStudent.GuardianZipCode = student.GuardianZipCode;
            vwStudent.GuardianWorkMobile = student.GuardianWorkMobile;
            vwStudent.NameOfClosestPerson = student.NameOfClosestPerson;
            vwStudent.HouseLocationGuardian = student.HouseLocationGuardian;
            vwStudent.HouseMobileGuardian = student.HouseMobileGuardian;
            vwStudent.GuardianFaxNumber = student.GuardianFaxNumber;
            vwStudent.GuardianWorkingWithUs = student.GuardianWorkingWithUs;
            vwStudent.PersonalStatusGuardianId = student.PersonalStatusGuardianId;
            vwStudent.PersonalStatusGuardianNumber = student.PersonalStatusGuardianNumber;
            vwStudent.PersonalStatusGuardianStartDate = student.PersonalStatusGuardianStartDate;
            vwStudent.PersonalStatusGuardianEndDate = student.PersonalStatusGuardianEndDate;
            vwStudent.PersonalStatusGuardianPlace = student.PersonalStatusGuardianPlace;
            vwStudent.MotherStudentName = student.MotherStudentName;
            vwStudent.KinshipMother = student.KinshipMother;
            vwStudent.MotherNationalId = student.MotherNationalId;
            vwStudent.MotherWork = student.MotherWork;
            vwStudent.MotherFaxNumber = student.MotherFaxNumber;
            vwStudent.MotherWorkMobile = student.MotherWorkMobile;
            vwStudent.MotherJob = student.MotherJob;
            vwStudent.MotherMobilePhone = student.MotherMobilePhone;
            vwStudent.Notes = student.Notes;
            vwStudent.CLSCloth = student.CLSCloth;
            vwStudent.CLSAcpt = student.CLSAcpt;
            vwStudent.CLSBakelite = student.CLSBakelite;
            vwStudent.CLSRegs = student.CLSRegs;
            vwStudent.AreYouWantGoWithBusSchool = student.AreYouWantGoWithBusSchool;
            vwStudent.DirectionBus = student.DirectionBus;
            vwStudent.BusId = student.BusId;
            vwStudent.CostFirstTermBeforeDiscount = student.CostFirstTermBeforeDiscount;
            vwStudent.CostSecondTermBeforeDiscount = student.CostSecondTermBeforeDiscount;
            vwStudent.CostSecondTermAfterDiscount = student.CostSecondTermAfterDiscount;
            vwStudent.CostFirstTermAfterDiscount = student.CostFirstTermAfterDiscount;
            vwStudent.GeneralDiscount = student.GeneralDiscount;
            vwStudent.EmployeeDiscount = student.EmployeeDiscount;
            vwStudent.EarlyPaymentDiscount = student.EarlyPaymentDiscount;
            vwStudent.SiblingsDiscount = student.SiblingsDiscount;
            vwStudent.CommunityFundDiscount = student.CommunityFundDiscount;
            vwStudent.SpecialDiscount = student.SpecialDiscount;
            vwStudent.TotalDiscount = student.TotalDiscount;
            vwStudent.BusDiscount = student.BusDiscount;
            vwStudent.TotalCost = student.TotalCost;
            vwStudent.TotalCostAfterDiscount = student.TotalCostAfterDiscount;
            vwStudent.ReceiptTotalFees = student.ReceiptTotalFees;
            vwStudent.ReceiptTotalPayments = student.ReceiptTotalPayments;
            vwStudent.RemainingFees = student.RemainingFees;
            return vwStudent;
        }

        public async Task<int> GetNewCode()
        {
            return await _unitOfWork.students.GetNewCode();
        }

        public async Task<VWStudent> GetNextStudent(int id)
           {
            var student = await _unitOfWork.students.GetNextOrPreviousItemByCode(id, "next");
            if (student == null)
            {
                return null;
            }
            List<VWInstallmentCostAfterDiscount> VWInstallmentCostAfterDiscount = new List<VWInstallmentCostAfterDiscount>();
            foreach (var instcostaftDis in student.installmentCostAfterDiscounts)
            {
                VWInstallmentCostAfterDiscount InstallmentCostAfterDiscount = new VWInstallmentCostAfterDiscount();
                InstallmentCostAfterDiscount.Id = instcostaftDis.Id;

                InstallmentCostAfterDiscount.CostInstallment = instcostaftDis.CostInstallment;
                InstallmentCostAfterDiscount.InstallmentId = instcostaftDis.InstallmentId;
                InstallmentCostAfterDiscount.InstallmentName = instcostaftDis.InstallmentName;
                VWInstallmentCostAfterDiscount.Add(InstallmentCostAfterDiscount);
            }
            List<VWInstallmentCostBeforeDiscount> VWInstallmentCostBeforeDiscounts = new List<VWInstallmentCostBeforeDiscount>();
            foreach (var instcostaftDis in student.installmentCostBeforeDiscounts)
            {
                VWInstallmentCostBeforeDiscount InstallmentCostBeforeDiscount = new VWInstallmentCostBeforeDiscount();
                InstallmentCostBeforeDiscount.Id = instcostaftDis.Id;
                InstallmentCostBeforeDiscount.CostInstallment = instcostaftDis.CostInstallment;
                InstallmentCostBeforeDiscount.InstallmentId = instcostaftDis.InstallmentId;
                InstallmentCostBeforeDiscount.InstallmentName = instcostaftDis.InstallmentName;
                VWInstallmentCostBeforeDiscounts.Add(InstallmentCostBeforeDiscount);
            }
            VWStudent vwStudent = new VWStudent();
            vwStudent.Id = student.Id;
            vwStudent.installmentCostAfterDiscounts = VWInstallmentCostAfterDiscount;
            vwStudent.installmentCostBeforeDiscounts = VWInstallmentCostBeforeDiscounts;
            vwStudent.StudentName = student.StudentName;
            vwStudent.StudentNumber = student.StudentNumber;
            vwStudent.FatherName = student.FatherName;
            vwStudent.GrandFatherName = student.GrandFatherName;
            vwStudent.FamilyName = student.FamilyName;
            vwStudent.DepartmentId = student.DepartmentId;
            vwStudent.StageId = student.StageId;
            vwStudent.BrithDate = student.BrithDate;
            vwStudent.BrithPlace = student.BrithPlace;
            vwStudent.NationalId = student.NationalId;
            vwStudent.GanderType = student.GanderType;
            vwStudent.ClassTypeId = student.ClassTypeId;
            vwStudent.ClassTypeName = student.ClassTypeName;
            vwStudent.LastBalance = student.LastBalance;
            vwStudent.LastSchoolName = student.LastSchoolName;
            vwStudent.AdvanceRepayment = student.AdvanceRepayment;
            vwStudent.MonthlyInstallment = student.MonthlyInstallment;
            vwStudent.PersonalStatusStudentId = student.PersonalStatusStudentId;
            vwStudent.PersonalStatusStudentNumber = student.PersonalStatusStudentNumber;
            vwStudent.PersonalStatusStudentStartDate = student.PersonalStatusStudentStartDate;
            vwStudent.PersonalStatusStudentEndDate = student.PersonalStatusStudentEndDate;
            vwStudent.PersonalStatusStudentPlace = student.PersonalStatusStudentPlace;
            vwStudent.PaymentTypeId = student.PaymentTypeId;
            vwStudent.EnrollmentPeriodId = student.EnrollmentPeriodId;
            vwStudent.GuardianName = student.GuardianName;
            vwStudent.Kinship = student.Kinship;
            vwStudent.GuardianNationalId = student.GuardianNationalId;
            vwStudent.GuardianSideWork = student.GuardianWork;
            vwStudent.GuardianJob = student.GuardianJob;
            vwStudent.GuardianMobile = student.GuardianMobile;
            vwStudent.GuardianEmail = student.GuardianEmail;
            vwStudent.GuardianZipCode = student.GuardianZipCode;
            vwStudent.GuardianWorkMobile = student.GuardianWorkMobile;
            vwStudent.NameOfClosestPerson = student.NameOfClosestPerson;
            vwStudent.HouseLocationGuardian = student.HouseLocationGuardian;
            vwStudent.HouseMobileGuardian = student.HouseMobileGuardian;
            vwStudent.GuardianFaxNumber = student.GuardianFaxNumber;
            vwStudent.GuardianWorkingWithUs = student.GuardianWorkingWithUs;
            vwStudent.PersonalStatusGuardianId = student.PersonalStatusGuardianId;
            vwStudent.PersonalStatusGuardianNumber = student.PersonalStatusGuardianNumber;
            vwStudent.PersonalStatusGuardianStartDate = student.PersonalStatusGuardianStartDate;
            vwStudent.PersonalStatusGuardianEndDate = student.PersonalStatusGuardianEndDate;
            vwStudent.PersonalStatusGuardianPlace = student.PersonalStatusGuardianPlace;
            vwStudent.MotherStudentName = student.MotherStudentName;
            vwStudent.KinshipMother = student.KinshipMother;
            vwStudent.MotherNationalId = student.MotherNationalId;
            vwStudent.MotherWork = student.MotherWork;
            vwStudent.MotherFaxNumber = student.MotherFaxNumber;
            vwStudent.MotherWorkMobile = student.MotherWorkMobile;
            vwStudent.MotherJob = student.MotherJob;
            vwStudent.MotherMobilePhone = student.MotherMobilePhone;
            vwStudent.Notes = student.Notes;
            vwStudent.CLSCloth = student.CLSCloth;
            vwStudent.CLSAcpt = student.CLSAcpt;
            vwStudent.CLSBakelite = student.CLSBakelite;
            vwStudent.CLSRegs = student.CLSRegs;
            vwStudent.AreYouWantGoWithBusSchool = student.AreYouWantGoWithBusSchool;
            vwStudent.DirectionBus = student.DirectionBus;
            vwStudent.BusId = student.BusId;
            vwStudent.CostFirstTermBeforeDiscount = student.CostFirstTermBeforeDiscount;
            vwStudent.CostSecondTermBeforeDiscount = student.CostSecondTermBeforeDiscount;
            vwStudent.CostSecondTermAfterDiscount = student.CostSecondTermAfterDiscount;
            vwStudent.CostFirstTermAfterDiscount = student.CostFirstTermAfterDiscount;
            vwStudent.GeneralDiscount = student.GeneralDiscount;
            vwStudent.EmployeeDiscount = student.EmployeeDiscount;
            vwStudent.EarlyPaymentDiscount = student.EarlyPaymentDiscount;
            vwStudent.SiblingsDiscount = student.SiblingsDiscount;
            vwStudent.CommunityFundDiscount = student.CommunityFundDiscount;
            vwStudent.SpecialDiscount = student.SpecialDiscount;
            vwStudent.TotalDiscount = student.TotalDiscount;
            vwStudent.BusDiscount = student.BusDiscount;
            vwStudent.TotalCost = student.TotalCost;
            vwStudent.TotalCostAfterDiscount = student.TotalCostAfterDiscount;
            vwStudent.ReceiptTotalFees = student.ReceiptTotalFees;
            vwStudent.ReceiptTotalPayments = student.ReceiptTotalPayments;
            vwStudent.RemainingFees = student.RemainingFees;
            return vwStudent;
        }

        public async Task<VWStudent> GetPreviousStudent(int id)
        {
            var student = await _unitOfWork.students.GetNextOrPreviousItemByCode(id, "previous");
            if (student == null)
            {
                return null;
            }
            List<VWInstallmentCostAfterDiscount> VWInstallmentCostAfterDiscount = new List<VWInstallmentCostAfterDiscount>();
            foreach (var instcostaftDis in student.installmentCostAfterDiscounts)
            {
                VWInstallmentCostAfterDiscount InstallmentCostAfterDiscount = new VWInstallmentCostAfterDiscount();
                InstallmentCostAfterDiscount.Id = instcostaftDis.Id;

                InstallmentCostAfterDiscount.CostInstallment = instcostaftDis.CostInstallment;
                InstallmentCostAfterDiscount.InstallmentId = instcostaftDis.InstallmentId;
                InstallmentCostAfterDiscount.InstallmentName= instcostaftDis.InstallmentName;
                VWInstallmentCostAfterDiscount.Add(InstallmentCostAfterDiscount);
            }
            List<VWInstallmentCostBeforeDiscount> VWInstallmentCostBeforeDiscounts = new List<VWInstallmentCostBeforeDiscount>();
            foreach (var instcostaftDis in student.installmentCostBeforeDiscounts)
            {
                VWInstallmentCostBeforeDiscount InstallmentCostBeforeDiscount = new VWInstallmentCostBeforeDiscount();
                InstallmentCostBeforeDiscount.Id = instcostaftDis.Id;
                InstallmentCostBeforeDiscount.CostInstallment = instcostaftDis.CostInstallment;
                InstallmentCostBeforeDiscount.InstallmentId = instcostaftDis.InstallmentId;
                InstallmentCostBeforeDiscount.InstallmentName=instcostaftDis.InstallmentName;   
                VWInstallmentCostBeforeDiscounts.Add(InstallmentCostBeforeDiscount);
            }
            VWStudent vwStudent = new VWStudent();
            vwStudent.Id = student.Id;
            vwStudent.installmentCostAfterDiscounts = VWInstallmentCostAfterDiscount;
            vwStudent.installmentCostBeforeDiscounts = VWInstallmentCostBeforeDiscounts;
            vwStudent.StudentName = student.StudentName;
            vwStudent.StudentNumber = student.StudentNumber;
            vwStudent.FatherName = student.FatherName;
            vwStudent.GrandFatherName = student.GrandFatherName;
            vwStudent.FamilyName = student.FamilyName;
            vwStudent.DepartmentId = student.DepartmentId;
            vwStudent.StageId = student.StageId;
            vwStudent.BrithDate = student.BrithDate;
            vwStudent.BrithPlace = student.BrithPlace;
            vwStudent.NationalId = student.NationalId;
            vwStudent.GanderType = student.GanderType;
            vwStudent.ClassTypeId = student.ClassTypeId;
            vwStudent.ClassTypeName = student.ClassTypeName;
            vwStudent.LastBalance = student.LastBalance;
            vwStudent.LastSchoolName = student.LastSchoolName;
            vwStudent.AdvanceRepayment = student.AdvanceRepayment;
            vwStudent.MonthlyInstallment = student.MonthlyInstallment;
            vwStudent.PersonalStatusStudentId = student.PersonalStatusStudentId;
            vwStudent.PersonalStatusStudentNumber = student.PersonalStatusStudentNumber;
            vwStudent.PersonalStatusStudentStartDate = student.PersonalStatusStudentStartDate;
            vwStudent.PersonalStatusStudentEndDate = student.PersonalStatusStudentEndDate;
            vwStudent.PersonalStatusStudentPlace = student.PersonalStatusStudentPlace;
            vwStudent.PaymentTypeId = student.PaymentTypeId;
            vwStudent.EnrollmentPeriodId = student.EnrollmentPeriodId;
            vwStudent.GuardianName = student.GuardianName;
            vwStudent.Kinship = student.Kinship;
            vwStudent.GuardianNationalId = student.GuardianNationalId;
            vwStudent.GuardianSideWork = student.GuardianWork;
            vwStudent.GuardianJob = student.GuardianJob;
            vwStudent.GuardianMobile = student.GuardianMobile;
            vwStudent.GuardianEmail = student.GuardianEmail;
            vwStudent.GuardianZipCode = student.GuardianZipCode;
            vwStudent.GuardianWorkMobile = student.GuardianWorkMobile;
            vwStudent.NameOfClosestPerson = student.NameOfClosestPerson;
            vwStudent.HouseLocationGuardian = student.HouseLocationGuardian;
            vwStudent.HouseMobileGuardian = student.HouseMobileGuardian;
            vwStudent.GuardianFaxNumber = student.GuardianFaxNumber;
            vwStudent.GuardianWorkingWithUs = student.GuardianWorkingWithUs;
            vwStudent.PersonalStatusGuardianId = student.PersonalStatusGuardianId;
            vwStudent.PersonalStatusGuardianNumber = student.PersonalStatusGuardianNumber;
            vwStudent.PersonalStatusGuardianStartDate = student.PersonalStatusGuardianStartDate;
            vwStudent.PersonalStatusGuardianEndDate = student.PersonalStatusGuardianEndDate;
            vwStudent.PersonalStatusGuardianPlace = student.PersonalStatusGuardianPlace;
            vwStudent.MotherStudentName = student.MotherStudentName;
            vwStudent.KinshipMother = student.KinshipMother;
            vwStudent.MotherNationalId = student.MotherNationalId;
            vwStudent.MotherWork = student.MotherWork;
            vwStudent.MotherFaxNumber = student.MotherFaxNumber;
            vwStudent.MotherWorkMobile = student.MotherWorkMobile;
            vwStudent.MotherJob = student.MotherJob;
            vwStudent.MotherMobilePhone = student.MotherMobilePhone;
            vwStudent.Notes = student.Notes;
            vwStudent.CLSCloth = student.CLSCloth;
            vwStudent.CLSAcpt = student.CLSAcpt;
            vwStudent.CLSBakelite = student.CLSBakelite;
            vwStudent.CLSRegs = student.CLSRegs;
            vwStudent.AreYouWantGoWithBusSchool = student.AreYouWantGoWithBusSchool;
            vwStudent.DirectionBus = student.DirectionBus;
            vwStudent.BusId = student.BusId;
            vwStudent.CostFirstTermBeforeDiscount = student.CostFirstTermBeforeDiscount;
            vwStudent.CostSecondTermBeforeDiscount = student.CostSecondTermBeforeDiscount;
            vwStudent.CostSecondTermAfterDiscount = student.CostSecondTermAfterDiscount;
            vwStudent.CostFirstTermAfterDiscount = student.CostFirstTermAfterDiscount;
            vwStudent.GeneralDiscount = student.GeneralDiscount;
            vwStudent.EmployeeDiscount = student.EmployeeDiscount;
            vwStudent.EarlyPaymentDiscount = student.EarlyPaymentDiscount;
            vwStudent.SiblingsDiscount = student.SiblingsDiscount;
            vwStudent.CommunityFundDiscount = student.CommunityFundDiscount;
            vwStudent.SpecialDiscount = student.SpecialDiscount;
            vwStudent.TotalDiscount = student.TotalDiscount;
            vwStudent.BusDiscount = student.BusDiscount;
            vwStudent.TotalCost = student.TotalCost;
            vwStudent.TotalCostAfterDiscount = student.TotalCostAfterDiscount;
            vwStudent.ReceiptTotalFees = student.ReceiptTotalFees;
            vwStudent.ReceiptTotalPayments = student.ReceiptTotalPayments;
            vwStudent.RemainingFees = student.RemainingFees;
            return vwStudent;
        }
    }
}
