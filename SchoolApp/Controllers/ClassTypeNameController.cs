using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.BL.Services;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.Controllers
{
    public class ClassTypeNameController : Controller
    {
        private readonly IServiceStage _serviceStage;
        private readonly IServiceClassTypeName  _serviceClassTypeName;
        public ClassTypeNameController(IServiceStage serviceStage, IServiceClassTypeName serviceClassTypeName)
        {
            _serviceStage = serviceStage;
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
                var nextCode = await _serviceClassTypeName.GetNewCode();
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
        public async Task<IActionResult> Add([FromBody] VWClassTypeName classTypeNameData)
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
            var resultMessage = _serviceClassTypeName.Add(classTypeNameData);
            // For this example, assume the service returns a success message string
            bool success = resultMessage.Contains("نجاح");

            return Ok(new { success, message = resultMessage });
        }

        // Action to edit an existing National
        [HttpPost]
        public IActionResult Edit([FromBody] VWClassTypeName classTypeNameData)
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

            var resultMessage = _serviceClassTypeName.Edit(classTypeNameData);
            bool success = resultMessage.Contains("نجاح");

            return Ok(new { success, message = resultMessage });
        }

        // Action to delete a National
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                string result = _serviceClassTypeName.Delete(id);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        [HttpGet("GetMinClassTypeName")]
        public async Task<IActionResult> GetMinClassTypeName()
        {
            var ClassTypeName = await _serviceClassTypeName.GetMinClassTypeName();
            if (ClassTypeName == null)
                return NotFound(new { Message = "No records found." });
            return Ok(ClassTypeName);
        }

        [HttpGet("GetMaxClassTypeName")]
        public async Task<IActionResult> GetMaxClassTypeName()
        {
            var national = await _serviceClassTypeName.GetMaxClassTypeName();
            if (national == null)
                return NotFound(new { Message = "No records found." });
            return Ok(national);
        }

        [HttpGet("GetNextClassTypeName/{id}")]
        public async Task<IActionResult> GetNextClassTypeName(int id)
        {
            if (id == 0)
            {
                id = _serviceClassTypeName.GetMaxClassTypeNameId();
            }
            var national = await _serviceClassTypeName.GetNextClassTypeName(id);
            if (national == null)
                return NotFound(new { Message = "No next record found." });
            return Ok(national);
        }

        [HttpGet("GetPreviousClassTypeName/{id}")]
        public async Task<IActionResult> GetPreviousClassTypeName(int id)
        {
            if (id == 0)
            {
                id = _serviceClassTypeName.GetMaxClassTypeNameId();
            }
            var national = await _serviceClassTypeName.GetPreviousClassTypeName(id);
            if (national == null)
                return NotFound(new { Message = "No previous record found." });
            return Ok(national);
        }
    }
}
