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
    }
}
