using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.BL.Services;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;

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
        public IActionResult Index()
        {
            ViewBag.ListDepartments=new SelectList(_serviceReport.GetAllDepartments(), "Id", "Name");
            ViewBag.ListClassTypeNames=new SelectList(_serviceReport.GetAllClassTypeNames(), "Id", "Name");
            ViewBag.ListStages=new SelectList(_serviceReport.GetAllStages(), "Id", "StageName");
            return View();
        }
        [HttpGet]
        public IActionResult GetClassTypeNamesByStage(int stageId)
        {
            var list = _serviceClassTypeName.GetAllClassTypesByStageId(stageId) ?? new List<ClassTypeName>();
            return Json(list);
        }
        [HttpGet]
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
        public IActionResult ReportStudentSFatherName()
        {
            ViewBag.ListDepartments = new SelectList(_serviceReport.GetAllDepartments(), "Id", "Name");
            ViewBag.ListClassTypeNames = new SelectList(_serviceReport.GetAllClassTypeNames(), "Id", "Name");
            ViewBag.ListStages = new SelectList(_serviceReport.GetAllStages(), "Id", "StageName");
            return View();
        }
        [HttpGet]
        public IActionResult ReportStudentSMotherName()
        {
            ViewBag.ListDepartments = new SelectList(_serviceReport.GetAllDepartments(), "Id", "Name");
            ViewBag.ListClassTypeNames = new SelectList(_serviceReport.GetAllClassTypeNames(), "Id", "Name");
            ViewBag.ListStages = new SelectList(_serviceReport.GetAllStages(), "Id", "StageName");
            return View();
        }
        [HttpGet]
        public IActionResult ReportStudentFeesName()
        {
            ViewBag.ListDepartments = new SelectList(_serviceReport.GetAllDepartments(), "Id", "Name");
            ViewBag.ListClassTypeNames = new SelectList(_serviceReport.GetAllClassTypeNames(), "Id", "Name");
            ViewBag.ListStages = new SelectList(_serviceReport.GetAllStages(), "Id", "StageName");
            return View();
        }
        [HttpPost]
        public IActionResult GetAllStudentFeesReport(int? DepartmentId, int? StageId, int? ClassTypeId, int? FromStudentNumber, int? ToStudentNumber, DateOnly? FromDate, DateOnly ToDate)
        {
            var data = _serviceReport.GetAllStudentFeesReport(DepartmentId, StageId, ClassTypeId, FromStudentNumber, ToStudentNumber, FromDate, ToDate);
            return Json(data); // Returns a list of student data as JSON
        }
    }
}
