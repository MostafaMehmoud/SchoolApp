using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.BL.Services.IServices
{
    public interface IServiceReport
    {
        public List<Student> GetAllStudents();
        public ClassTypeName GetbyIdClassTypeName(int id);
        public IEnumerable<ClassTypeName> GetAllClassTypeNames(); 
        public Department GetbyIdDepartments(int id);
        public IEnumerable<Department> GetAllDepartments();
        public Stage GetbyIdStage(int id);
        public IEnumerable<Stage> GetAllStages();
        public List<VWReportStudent> GetAllStudentsNameReport(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly? ToDate);
        public List<VWReportStudent> GetAllStudentFeesReport(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly? ToDate);
        public List<AccountStatement> GetAccountStatement(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly? ToDate,int? studentId);
        public List<VWStudentCompleteFees> GetStudentCompleteFees(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly? ToDate);
        public List<VWStudentCompleteFees> GetStudentPartFees(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly? ToDate);
        public List<VWStudentCompleteFees> GetStudentNoFees(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly? ToDate);
        public List<VWReportPayments> GetPaymentStudent(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly? ToDate, int? studentId);
        public List<ReportClassType> GetFeeStudent( int? StageId, int? ClassTypeId);
        public Task<string> TraneferringClasses(int fromStage, int fromClass, int toStage, int toClass);

    }
}
