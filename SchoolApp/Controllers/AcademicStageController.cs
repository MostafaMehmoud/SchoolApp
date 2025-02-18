using Microsoft.AspNetCore.Mvc;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.Controllers
{
    public class AcademicStageController : Controller
    {
        private readonly IServiceStage _serviceStage;
        public AcademicStageController(IServiceStage serviceStage)
        {
            _serviceStage = serviceStage;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetNextStageCode()
        {
            try
            {
                var nextCode = await _serviceStage.GetNewCode();
                return Json(new { nextCode });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Exception in GetNextStageCode: " + ex.ToString());
                // Return full details for debugging (remove before production)
                return StatusCode(500, new { error = ex.Message, details = ex.ToString() });
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddStage([FromBody] VWStage Stage)
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
            var resultMessage = _serviceStage.Add(Stage.code, Stage.Name);
            // For this example, assume the service returns a success message string
            bool success = resultMessage.Contains("نجاح");

            return Ok(new { success, message = resultMessage });
        }

        // Action to edit an existing Stage
        [HttpPost]
        public IActionResult EditStage([FromBody] VWStage Stage)
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

            var resultMessage = _serviceStage.Edit(Stage.ID, Stage.code, Stage.Name);
            bool success = resultMessage.Contains("نجاح");

            return Ok(new { success, message = resultMessage });
        }

        // Action to delete a Stage
        [HttpPost]
        public IActionResult DeleteStage(int id)
        {
            try
            {
                string result = _serviceStage.Delete(id);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        [HttpGet("GetMinStage")]
        public async Task<IActionResult> GetMinStage()
        {
            var Stage = await _serviceStage.GetMinStage();
            if (Stage == null)
                return NotFound(new { Message = "No records found." });
            return Ok(Stage);
        }

        [HttpGet("GetMaxStage")]
        public async Task<IActionResult> GetMaxStage()
        {
            var Stage = await _serviceStage.GetMaxStage();
            if (Stage == null)
                return NotFound(new { Message = "No records found." });
            return Ok(Stage);
        }

        [HttpGet("GetNextStage/{id}")]
        public async Task<IActionResult> GetNextStage(int id)
        {
            if (id == 0)
            {
                id = _serviceStage.GetMaxStageId();
            }
            var Stage = await _serviceStage.GetNextStage(id);
            if (Stage == null)
                return NotFound(new { Message = "No next record found." });
            return Ok(Stage);
        }

        [HttpGet("GetPreviousStage/{id}")]
        public async Task<IActionResult> GetPreviousStage(int id)
        {
            if (id == 0)
            {
                id = _serviceStage.GetMaxStageId();
            }
            var Stage = await _serviceStage.GetPreviousStage(id);
            if (Stage == null)
                return NotFound(new { Message = "No previous record found." });
            return Ok(Stage);
        }
    }
}
