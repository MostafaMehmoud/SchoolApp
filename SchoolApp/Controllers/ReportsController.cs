using BarcodeStandard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.BL.Services;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using Microsoft.AspNetCore.Mvc;
using System;
using Type = BarcodeStandard.Type;
namespace SchoolApp.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IServiceReport _serviceReport;
        private readonly IServiceClassTypeName _serviceClassTypeName;
        public ReportsController(IServiceReport serviceReport, IServiceClassTypeName serviceClassTypeName)
        { 
            _serviceReport = serviceReport; 
            _serviceClassTypeName = serviceClassTypeName;
        }
        [Permission("CanAccessPrintReports")]
        public IActionResult Index()
        {
            ViewBag.ListDepartments=new SelectList(_serviceReport.GetAllDepartments(), "Id", "Name");
            ViewBag.ListClassTypeNames=new SelectList(_serviceReport.GetAllClassTypeNames(), "Id", "Name");
            ViewBag.ListStages=new SelectList(_serviceReport.GetAllStages(), "Id", "StageName");
            return View();
        }
        [HttpGet]
        [Permission("CanAccessPrintReports")]
        public IActionResult GetClassTypeNamesByStage(int stageId)
        {
            var list = _serviceClassTypeName.GetAllClassTypesByStageId(stageId) ?? new List<ClassTypeName>();
            return Json(list);
        }
        [HttpGet]
        [Permission("CanAccessPrintReports")]
        public IActionResult ReportStudentSName()
        {
            ViewBag.ListDepartments = new SelectList(_serviceReport.GetAllDepartments(), "Id", "Name");
            ViewBag.ListClassTypeNames = new SelectList(_serviceReport.GetAllClassTypeNames(), "Id", "Name");
            ViewBag.ListStages = new SelectList(_serviceReport.GetAllStages(), "Id", "StageName");
            return View();
        }
        [HttpPost]
        public IActionResult GetStudents(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate)
        {
            var data = _serviceReport.GetAllStudentsNameReport(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate);
            return Json(data); // Returns a list of student data as JSON
        }
        [HttpGet]
        [Permission("CanAccessPrintReports")]
        public IActionResult ReportStudentSFatherName()
        {
            ViewBag.ListDepartments = new SelectList(_serviceReport.GetAllDepartments(), "Id", "Name");
            ViewBag.ListClassTypeNames = new SelectList(_serviceReport.GetAllClassTypeNames(), "Id", "Name");
            ViewBag.ListStages = new SelectList(_serviceReport.GetAllStages(), "Id", "StageName");
            return View();
        }
        [HttpGet]
        [Permission("CanAccessPrintReports")]
        public IActionResult ReportStudentSMotherName()
        {
            ViewBag.ListDepartments = new SelectList(_serviceReport.GetAllDepartments(), "Id", "Name");
            ViewBag.ListClassTypeNames = new SelectList(_serviceReport.GetAllClassTypeNames(), "Id", "Name");
            ViewBag.ListStages = new SelectList(_serviceReport.GetAllStages(), "Id", "StageName");
            return View();
        }
        [HttpGet]
        [Permission("CanAccessPrintReports")]
        public IActionResult ReportStudentFeesName()
        {
            ViewBag.ListDepartments = new SelectList(_serviceReport.GetAllDepartments(), "Id", "Name");
            ViewBag.ListClassTypeNames = new SelectList(_serviceReport.GetAllClassTypeNames(), "Id", "Name");
            ViewBag.ListStages = new SelectList(_serviceReport.GetAllStages(), "Id", "StageName");
            return View();
        }
        [HttpPost][Permission("CanAccessPrintReports")]
        public IActionResult GetAllStudentFeesReport(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate)
        {
            var data = _serviceReport.GetAllStudentFeesReport(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate);
            return Json(data); // Returns a list of student data as JSON
        }
        [HttpGet]
        [Permission("CanAccessPrintReports")]
        public IActionResult ReportAccountStatement()
        {
            ViewBag.ListDepartments = new SelectList(_serviceReport.GetAllDepartments(), "Id", "Name");
            ViewBag.ListClassTypeNames = new SelectList(_serviceReport.GetAllClassTypeNames(), "Id", "Name");
            ViewBag.ListStages = new SelectList(_serviceReport.GetAllStages(), "Id", "StageName");
            ViewBag.ListStudents = new SelectList(_serviceReport.GetAllStudents(), "Id", "StudentNumber");
            return View();
        }
        [HttpPost]
        public IActionResult GetAccountStatement(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate,int? studentId)
        {
            var data = _serviceReport.GetAccountStatement(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate,studentId);
            return Json(data); // Returns a list of student data as JSON
        }
        [HttpGet]
        [Permission("CanAccessPrintReports")]
        public IActionResult ReportStudentCompleteFees()
        {
            ViewBag.ListDepartments = new SelectList(_serviceReport.GetAllDepartments(), "Id", "Name");
            ViewBag.ListClassTypeNames = new SelectList(_serviceReport.GetAllClassTypeNames(), "Id", "Name");
            ViewBag.ListStages = new SelectList(_serviceReport.GetAllStages(), "Id", "StageName");
           
            return View();
        }
        [HttpPost]
        public IActionResult GetStudentCompleteFees(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate)
        {
            var data = _serviceReport.GetStudentCompleteFees(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate);
            return Json(data); // Returns a list of student data as JSON
        }
        [HttpGet]
        [Permission("CanAccessPrintReports")]
        public IActionResult ReportStudentPartFees()
        {
            ViewBag.ListDepartments = new SelectList(_serviceReport.GetAllDepartments(), "Id", "Name");
            ViewBag.ListClassTypeNames = new SelectList(_serviceReport.GetAllClassTypeNames(), "Id", "Name");
            ViewBag.ListStages = new SelectList(_serviceReport.GetAllStages(), "Id", "StageName");

            return View();
        }
        [HttpPost]
        public IActionResult GetStudentPartFees(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate)
        {
            var data = _serviceReport.GetStudentPartFees(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate);
            return Json(data); // Returns a list of student data as JSON
        }
        [HttpGet]
        [Permission("CanAccessPrintReports")]
        public IActionResult ReportStudentNoFees()
        {
            ViewBag.ListDepartments = new SelectList(_serviceReport.GetAllDepartments(), "Id", "Name");
            ViewBag.ListClassTypeNames = new SelectList(_serviceReport.GetAllClassTypeNames(), "Id", "Name");
            ViewBag.ListStages = new SelectList(_serviceReport.GetAllStages(), "Id", "StageName");

            return View();
        }
        [HttpPost]
        public IActionResult GetStudentNoFees(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate)
        {
            var data = _serviceReport.GetStudentNoFees(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate);
            return Json(data); // Returns a list of student data as JSON
        }
        [HttpGet]
        [Permission("CanAccessPrintReports")]
        public IActionResult ReportStudentNotificationFees()
        {
            ViewBag.ListDepartments = new SelectList(_serviceReport.GetAllDepartments(), "Id", "Name");
            ViewBag.ListClassTypeNames = new SelectList(_serviceReport.GetAllClassTypeNames(), "Id", "Name");
            ViewBag.ListStages = new SelectList(_serviceReport.GetAllStages(), "Id", "StageName");

            return View();
        }
        [HttpPost]
        public IActionResult ReportStudentNotificationFees(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate)
        {
            var data = _serviceReport.GetStudentPartFees(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate);
            return Json(data); // Returns a list of student data as JSON
        }
        public IActionResult PrintNotificationFees()
        {
            return View();
        }
        [HttpGet]
        [Permission("CanAccessPrintReports")]
        public IActionResult ReportDefinationStudent()
        {
            ViewBag.ListDepartments = new SelectList(_serviceReport.GetAllDepartments(), "Id", "Name");
            ViewBag.ListClassTypeNames = new SelectList(_serviceReport.GetAllClassTypeNames(), "Id", "Name");
            ViewBag.ListStages = new SelectList(_serviceReport.GetAllStages(), "Id", "StageName");
            ViewBag.ListStudents = new SelectList(_serviceReport.GetAllStudents(), "Id", "StudentNumber");
            return View();
        }
        [HttpPost]
        public IActionResult ReportDefinationStudent(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate, int? studentId)
        {
            var data = _serviceReport.GetStudentPartFees(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate)
            .Where(i => !studentId.HasValue || i.Id == studentId);
            return Json(data); // Returns a list of student data as JSON
        }
        public IActionResult PrintDefination()
        {
            return View();
        }// 🆕 توليد الباركود من رقم الطالب وإرجاعه كصورة
        [HttpGet]
        [Permission("CanAccessPrintReports")]
        public IActionResult ReportBarcodeStudent()
        {
            ViewBag.ListDepartments = new SelectList(_serviceReport.GetAllDepartments(), "Id", "Name");
            ViewBag.ListClassTypeNames = new SelectList(_serviceReport.GetAllClassTypeNames(), "Id", "Name");
            ViewBag.ListStages = new SelectList(_serviceReport.GetAllStages(), "Id", "StageName");
            ViewBag.ListStudents = new SelectList(_serviceReport.GetAllStudents(), "Id", "StudentNumber");
            return View();
        }
        [HttpPost]
        public IActionResult ReportBarcodeStudent(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate, int? studentId)
        {
            var data = _serviceReport.GetStudentPartFees(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate)
            .Where(i => !studentId.HasValue || i.Id == studentId);
            return Json(data); // Returns a list of student data as JSON
        }
        [Permission("CanAccessPrintReports")]
        public IActionResult ReportPayments()
        {
            ViewBag.ListDepartments = new SelectList(_serviceReport.GetAllDepartments(), "Id", "Name");
            ViewBag.ListClassTypeNames = new SelectList(_serviceReport.GetAllClassTypeNames(), "Id", "Name");
            ViewBag.ListStages = new SelectList(_serviceReport.GetAllStages(), "Id", "StageName");
            ViewBag.ListStudents = new SelectList(_serviceReport.GetAllStudents(), "Id", "StudentNumber");
            return View();
        }
        [HttpPost]
        public IActionResult ReportPayments(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate, int? studentId)
        {
            var data = _serviceReport.GetPaymentStudent(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate, studentId);
            return Json(data); // Returns a list of student data as JSON
        }
        [Permission("CanAccessPrintReports")]
        public IActionResult ReportFees()
        {
            
            ViewBag.ListClassTypeNames = new SelectList(_serviceReport.GetAllClassTypeNames(), "Id", "Name");
            ViewBag.ListStages = new SelectList(_serviceReport.GetAllStages(), "Id", "StageName");
           
            return View();
        }
        [HttpPost]
        public IActionResult ReportFees( int? StageId, int? ClassTypeId)
        {
            var data = _serviceReport.GetFeeStudent( StageId, ClassTypeId);
            return Json(data); // Returns a list of student data as JSON
        }
        [HttpGet]
        [Permission("CanAccessPrintReports")]
        public IActionResult ReportStudentSNameOrderByStudentName()
        {
            ViewBag.ListDepartments = new SelectList(_serviceReport.GetAllDepartments(), "Id", "Name");
            ViewBag.ListClassTypeNames = new SelectList(_serviceReport.GetAllClassTypeNames(), "Id", "Name");
            ViewBag.ListStages = new SelectList(_serviceReport.GetAllStages(), "Id", "StageName");
            return View();
        }
        [HttpPost]
        public IActionResult GetStudentsOrderByStudentName(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate)
        {
            var data = _serviceReport.GetAllStudentsNameReport(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate)
                .OrderBy(i => i.StudentName);
            return Json(data); // Returns a list of student data as JSON
        }
    }
   
    
}
