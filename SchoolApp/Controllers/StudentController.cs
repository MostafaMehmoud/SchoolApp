using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.BL.Services;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly IServiceDepartment _serviceDepartment;
        private readonly IServiceNational _serviceNational;
        private readonly IServiceStage _serviceStage;
        private readonly IServiceInstallment _serviceInstallment;   
        private readonly IServiceFileBus _servicefileBus;
        private readonly IServiceStudent _servicestudent;
        public StudentController(IServiceDepartment serviceDepartment,
            IServiceNational serviceNational, IServiceStage serviceStage,
            IServiceInstallment serviceInstallment, IServiceFileBus servicefileBus, 
            IServiceStudent servicestudent)
        {
            _serviceDepartment = serviceDepartment;
            _serviceNational = serviceNational;
            _serviceStage = serviceStage;
            _serviceInstallment = serviceInstallment;
            _servicefileBus = servicefileBus;
            _servicestudent = servicestudent;
        }

        public IActionResult Index()
        {
            ViewBag.departments=new SelectList( _serviceDepartment.GetAll(),"Id","Name");
            ViewBag.nationals = new SelectList(_serviceNational.GetAll(), "ID", "Name");
            ViewBag.Stages = new SelectList(_serviceStage.GetAll(), "Id", "StageName");
            ViewBag.buses = new SelectList(_servicefileBus.GetAll(), "BusId", "BusPlate");
            return View(new VWStudent());
        }
        [HttpGet]
        public IActionResult GetInstallmentsByClassType(int stageId, int classTypeId)
        {
            InstallmentMain installments = _serviceInstallment.GetAllInstallmentsByStageIdAndClassTypeId(stageId, classTypeId);
           
            if (installments == null || !installments.Installments.Any())
            {
                InstallmentMain InstallmentMain = _serviceInstallment.GetAInstallmentofClassType(stageId, classTypeId);
               if(InstallmentMain == null)
                {
                    return Json(new { message = "لا توجد بيانات متاحة" });
                }
                return Json(InstallmentMain); // إرجاع مصفوفة فارغة عند عدم وجود بيانات
            }

            return Json(installments);
        }
        [HttpGet]
        public IActionResult GetBusCostByClassType(int buseId, int busCostTypeId)
        {
            var installments = _servicefileBus.GetBusCostByBusIdAndBusCostTypeId(buseId, busCostTypeId);

           

            return Json(installments);
        }
        public async Task<IActionResult> GetNextCode()
        {
            try
            {
                var nextCode = await _servicestudent.GetNewCode();
                return Json(new { nextCode });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Exception in GetNextNationalCode: " + ex.ToString());
                // Return full details for debugging (remove before production)
                return StatusCode(500, new { error = ex.Message, details = ex.ToString() });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add( VWStudent vWStudent)
        {
            ModelState.Remove("Id");
           
            if (!ModelState.IsValid)
            {
                // Collect validation errors
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );
                return BadRequest(errors);
            }

            // Use your BL service to add the record
            var resultMessage = _servicestudent.Add(vWStudent);
            // For this example, assume the service returns a success message string
            bool success = resultMessage.Contains("نجاح");

            return Ok(new { success, message = resultMessage });
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                string result = _servicestudent.Delete(id);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Edit(VWStudent vWStudent)
        {

           
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );
                return BadRequest(errors);
            }

            var resultMessage = _servicestudent.Edit(vWStudent);
            bool success = resultMessage.Contains("نجاح");

            return Ok(new { success, message = resultMessage });
        }
        [HttpGet("GetMinStudent")]
        public async Task<IActionResult> GetMinStudent()
        {
            var Student = await _servicestudent.GetMinStudent();
            if (Student == null)
                return NotFound(new { Message = "No records found." });
            return Ok(Student);
        }

        [HttpGet("GetMaxStudent")]
        public async Task<IActionResult> GetMaxStudent()
        {
            var national = await _servicestudent.GetMaxStudent();
            if (national == null)
                return NotFound(new { Message = "No records found." });
            return Ok(national);
        }

        [HttpGet("GetNextStudent/{id}")]
        public async Task<IActionResult> GetNextStudent(int id)
        {
            if (id == 0)
            {
                id = _servicestudent.GetMaxStudentId();
            }
            var national = await _servicestudent.GetNextStudent(id);
            if (national == null)
                return NotFound(new { Message = "No next record found." });
            return Ok(national);
        }

        [HttpGet("GetPreviousStudent/{id}")]
        public async Task<IActionResult> GetPreviousStudent(int id)
        {
            if (id == 0)
            {
                id = _servicestudent.GetMaxStudentId();
            }
            var national = await _servicestudent.GetPreviousStudent(id);
            if (national == null)
                return NotFound(new { Message = "No previous record found." });
            return Ok(national);
        }
    }
}
