using Microsoft.AspNetCore.Mvc;
using SchoolApp.BL.Services;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IServiceDepartment _serviceDepartment;
        public DepartmentController(IServiceDepartment serviceDepartment)
        {
            _serviceDepartment = serviceDepartment;
        }
        [Permission("CanAccessBusesFile")]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetNextdepartmentCode()
        {
            try
            {
                var nextCode = await _serviceDepartment.GetNewCode();
                return Json(new { nextCode });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Exception in GetNextdepartmentCode: " + ex.ToString());
                // Return full details for debugging (remove before production)
                return StatusCode(500, new { error = ex.Message, details = ex.ToString() });
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] VWDepartment department)
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
            var resultMessage = _serviceDepartment.Add(department);
            // For this example, assume the service returns a success message string
            bool success = resultMessage.Contains("نجاح");

            return Ok(new { success, message = resultMessage });
        }

        // Action to edit an existing department
        [HttpPost]
        public IActionResult EditDepartment([FromBody] VWDepartment department)
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

            var resultMessage = _serviceDepartment.Edit(department);
            bool success = resultMessage.Contains("نجاح");

            return Ok(new { success, message = resultMessage });
        }

        // Action to delete a department
        [HttpPost]
        public IActionResult DeleteDepartment(int id)
        {
            try
            {
                string result = _serviceDepartment.Delete(id);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        [HttpGet("GetMinDepartment")]
        public async Task<IActionResult> GetMinDepartment()
        {
            var department = await _serviceDepartment.GetMinDepartment();
            if (department == null)
                return NotFound(new { Message = "No records found." });
            return Ok(department);
        }

        [HttpGet("GetMaxDepartment")]
        public async Task<IActionResult> GetMaxDepartment()
        {
            var department = await _serviceDepartment.GetMaxDepartment();
            if (department == null)
                return NotFound(new { Message = "No records found." });
            return Ok(department);
        }

        [HttpGet("GetNextDepartment/{id}")]
        public async Task<IActionResult> GetNextDepartment(int id)
        {
            if (id == 0)
            {
                id = _serviceDepartment.GetMaxDepartmentId();
            }
            var department = await _serviceDepartment.GetNextDepartment(id);
            if (department == null)
                return NotFound(new { Message = "No next record found." });
            return Ok(department);
        }

        [HttpGet("GetPreviousDepartment/{id}")]
        public async Task<IActionResult> GetPreviousDepartment(int id)
        {
            if (id == 0)
            {
                id = _serviceDepartment.GetMaxDepartmentId();
            }
            var department = await _serviceDepartment.GetPreviousDepartment(id);
            if (department == null)
                return NotFound(new { Message = "No previous record found." });
            return Ok(department);
        }
    }
}
