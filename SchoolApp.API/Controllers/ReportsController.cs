using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IServiceReport _serviceReport;
        private readonly IServiceClassTypeName _serviceClassTypeName;
        public ReportsController(IServiceReport serviceReport, IServiceClassTypeName serviceClassTypeName)
        {
            _serviceReport = serviceReport;
            _serviceClassTypeName = serviceClassTypeName;
        }
        [HttpPost("GetStudents")]
        public async Task<ActionResult<ApiResponse<List<VWReportStudent>>>> GetStudents(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate)
        {
            var data = _serviceReport.GetAllStudentsNameReport(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate);
            return ApiResponse<List<VWReportStudent>>.SuccessResponse(data, "Operation is successfull");
        }
        [HttpPost("GetAllStudentFeesReport")]
        public async Task<ActionResult<ApiResponse<List<VWReportStudent>>>> GetAllStudentFeesReport(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate)
        {
            var data = _serviceReport.GetAllStudentFeesReport(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate);
            return ApiResponse<List<VWReportStudent>>.SuccessResponse(data, "Operation is successfull");
        }
        [HttpPost("GetAccountStatement")]
        public async Task<ActionResult<ApiResponse<List<AccountStatement>>>> GetAccountStatement(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate, int? studentId)
        {
            var data = _serviceReport.GetAccountStatement(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate, studentId);
            return ApiResponse<List<AccountStatement>>.SuccessResponse(data, "Operation is successfull");
        }
        [HttpPost("GetStudentCompleteFees")]
        public async Task<ActionResult<ApiResponse<List<VWStudentCompleteFees>>>> GetStudentCompleteFees(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate)
        {
            var data = _serviceReport.GetStudentCompleteFees(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate);
            return ApiResponse<List<VWStudentCompleteFees>>.SuccessResponse(data, "Operation is successfull");
        }
        [HttpPost("GetStudentPartFees")]
        public async Task<ActionResult<ApiResponse<List<VWStudentCompleteFees>>>> GetStudentPartFees(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate)
        {
            var data = _serviceReport.GetStudentPartFees(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate);
            return ApiResponse<List<VWStudentCompleteFees>>.SuccessResponse(data, "Operation is successfull");
        }
        [HttpPost("GetStudentPartFees")]
        public async Task<ActionResult<ApiResponse<List<VWStudentCompleteFees>>>> GetStudentNoFees(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate)
        {
            var data = _serviceReport.GetStudentNoFees(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate);
            return ApiResponse<List<VWStudentCompleteFees>>.SuccessResponse(data, "Operation is successfull");
        }
        [HttpPost("ReportStudentNotificationFees")]
        public async Task<ActionResult<ApiResponse<List<VWStudentCompleteFees>>>> ReportStudentNotificationFees(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate)
        {
            var data = _serviceReport.GetStudentPartFees(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate);
            return ApiResponse<List<VWStudentCompleteFees>>.SuccessResponse(data, "Operation is successfull");
        }
       
    [HttpPost("ReportDefinationStudent")]
        public async Task<ActionResult<ApiResponse<List<VWStudentCompleteFees>>>> ReportDefinationStudent(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate, int? studentId)
        {
            var data = _serviceReport.GetStudentPartFees(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate)
           .Where(i => !studentId.HasValue || i.Id == studentId).ToList();
            return ApiResponse<List<VWStudentCompleteFees>>.SuccessResponse(data, "Operation is successfull");
        }
        [HttpPost("ReportBarcodeStudent")]
        public async Task<ActionResult<ApiResponse<List<VWStudentCompleteFees>>>> ReportBarcodeStudent(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate, int? studentId)
        {
            var data = _serviceReport.GetStudentPartFees(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate)
            .Where(i => !studentId.HasValue || i.Id == studentId).ToList();
            return ApiResponse<List<VWStudentCompleteFees>>.SuccessResponse(data, "Operation is successfull");
        }
        [HttpPost("ReportBarcodeStudent")]
        public async Task<ActionResult<ApiResponse<List<VWReportPayments>>>> ReportPayments(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate, int? studentId)
        {
            var data = _serviceReport.GetPaymentStudent(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate, studentId);
            return ApiResponse<List<VWReportPayments>>.SuccessResponse(data, "Operation is successfull");
        }
        [HttpPost("ReportFees")]
        public async Task<ActionResult<ApiResponse<List<ReportClassType>>>> ReportFees(int? StageId, int? ClassTypeId)
        {
            var data = _serviceReport.GetFeeStudent(StageId, ClassTypeId);
            return ApiResponse<List<ReportClassType>>.SuccessResponse(data, "Operation is successfull");
        }
        [HttpPost("ReportBarcodeStudent")]
        public async Task<ActionResult<ApiResponse<List<VWReportStudent>>>> GetStudentsOrderByStudentName(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate)
        {
            var data = _serviceReport.GetAllStudentsNameReport(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate)
               .OrderBy(i => i.StudentName).ToList();
            return ApiResponse<List<VWReportStudent>>.SuccessResponse(data, "Operation is successfull");
        }
    }
}
