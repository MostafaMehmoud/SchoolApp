using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.BL.Services;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.Controllers
{
    public class InstallmentController : Controller
    {
        private readonly IServiceClassTypeName _serviceClassTypeName;
        private readonly IServiceStage _serviceStage;
        private readonly IServiceInstallment _serviceInstallment;
        public InstallmentController(IServiceStage serviceStage, IServiceInstallment serviceInstallment, IServiceClassTypeName serviceClassTypeName)
        {
            _serviceStage = serviceStage;
            _serviceInstallment = serviceInstallment;
            _serviceClassTypeName = serviceClassTypeName;
        }
        public IActionResult Index()
        {
            ViewBag.listStages = new SelectList(_serviceStage.GetAll(), "Id", "StageName");
            return View();
        }
        public async Task<IActionResult> GetNextCode()
        {
            try
            {
                var nextCode = await _serviceInstallment.GetNewCode();
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
        public async Task<IActionResult> Add([FromBody] VWInstallment InstallmentData)
        {
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
            var resultMessage = _serviceInstallment.Add(InstallmentData);
            // For this example, assume the service returns a success message string
            bool success = resultMessage.Contains("نجاح");

            return Ok(new { success, message = resultMessage });
        }

        // Action to edit an existing National
        [HttpPost]
        public IActionResult Edit([FromBody] VWInstallment InstallmentData)
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

            var resultMessage = _serviceInstallment.Edit(InstallmentData);
            bool success = resultMessage.Contains("نجاح");

            return Ok(new { success, message = resultMessage });
        }

        // Action to delete a National
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                string result = _serviceInstallment.Delete(id);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        [HttpGet("GetMinInstallment")]
        public async Task<IActionResult> GetMinInstallment()
        {
            var Installment = await _serviceInstallment.GetMinInstallment();
            if (Installment == null)
                return NotFound(new { Message = "No records found." });
            return Ok(Installment);
        }

        [HttpGet("GetMaxInstallment")]
        public async Task<IActionResult> GetMaxInstallment()
        {
            var national = await _serviceInstallment.GetMaxInstallment();
            if (national == null)
                return NotFound(new { Message = "No records found." });
            return Ok(national);
        }

        [HttpGet("GetNextInstallment/{id}")]
        public async Task<IActionResult> GetNextInstallment(int id)
        {
            if (id == 0)
            {
                id = _serviceInstallment.GetMaxInstallmentId();
            }
            var national = await _serviceInstallment.GetNextInstallment(id);
            if (national == null)
                return NotFound(new { Message = "No next record found." });
            return Ok(national);
        }

        [HttpGet("GetPreviousInstallment/{id}")]
        public async Task<IActionResult> GetPreviousInstallment(int id)
        {
            if (id == 0)
            {
                id = _serviceInstallment.GetMaxInstallmentId();
            }
            var national = await _serviceInstallment.GetPreviousInstallment(id);
            if (national == null)
                return NotFound(new { Message = "No previous record found." });
            return Ok(national);
        }
        [HttpGet]
        public IActionResult GetClassTypeNamesByStage(int stageId)
        {
            var list = _serviceClassTypeName.GetAllClassTypesByStageId(stageId) ?? new List<ClassTypeName>();
            return Json(list);
        }

    }
}
