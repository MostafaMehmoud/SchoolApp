using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.Controllers
{
    public class NationalController : Controller
    {
        private readonly IServiceNational _national;
        public NationalController(IServiceNational national) 
        { 
            _national = national;
        }
        public IActionResult Index()
        {
            return View(new National());
        }
        [HttpGet]
        public async Task<IActionResult> GetNextNationalCode()
        {
            try
            {
                var nextCode = await _national.GetNewCode();
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
        public async Task<IActionResult> AddNational([FromBody] VWNationals national)
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
            var resultMessage = _national.Add(national.code, national.Name);
            // For this example, assume the service returns a success message string
            bool success = resultMessage.Contains("نجاح");

            return Ok(new { success, message = resultMessage });
        }

        // Action to edit an existing National
        [HttpPost]
        public IActionResult EditNational([FromBody] VWNationals national)
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

            var resultMessage = _national.Edit(national.ID, national.code, national.Name);
            bool success = resultMessage.Contains("نجاح");

            return Ok(new { success, message = resultMessage });
        }

        // Action to delete a National
        [HttpPost]
        public IActionResult DeleteNational(int id)
        {
            try
            {
                string result = _national.Delete(id);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        [HttpGet("GetMinNational")]
        public async Task<IActionResult> GetMinNational()
        {
            var national = await _national.GetMinNational();
            if (national == null)
                return NotFound(new { Message = "No records found." });
            return Ok(national);
        }

        [HttpGet("GetMaxNational")]
        public async Task<IActionResult> GetMaxNational()
        {
            var national = await _national.GetMaxNational();
            if (national == null)
                return NotFound(new { Message = "No records found." });
            return Ok(national);
        }

        [HttpGet("GetNextNational/{id}")]
        public async Task<IActionResult> GetNextNational(int id)
        {
            if (id == 0)
            {
                id = _national.GetMaxNationalId();
            }
            var national = await _national.GetNextNational(id);
            if (national == null)
                return NotFound(new { Message = "No next record found." });
            return Ok(national);
        }

        [HttpGet("GetPreviousNational/{id}")]
        public async Task<IActionResult> GetPreviousNational(int id)
        {
            if (id == 0)
            {
                id = _national.GetMaxNationalId();
            }
            var national = await _national.GetPreviousNational(id);
            if (national == null)
                return NotFound(new { Message = "No previous record found." });
            return Ok(national);
        }
    }
}
