using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.Controllers
{
    public class FileBusController : Controller
    {
        private readonly IServiceFileBus _serviceFileBus;
        private readonly IServiceStage _serviceStage;
        public FileBusController(IServiceFileBus serviceFileBus, IServiceStage serviceStage)
        {
            _serviceFileBus = serviceFileBus;
            _serviceStage = serviceStage;
        }
        [Permission("CanAccessBusesFile")]
        public IActionResult Index(int id=0)
        {
            var fileBus = _serviceFileBus.GetbyId(id);
            ViewBag.listStages = new SelectList(_serviceStage.GetAll(), "Id", "StageName");
            return View(fileBus);
        }
        [HttpGet]
        public async Task<IActionResult> GetNextCode()
        {
            try
            {
                var nextCode = await _serviceFileBus.GetNewCode();
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
        public async Task<IActionResult> Add(VWFileBus fileBus)
        {
            ModelState.Remove("BusId");
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
            var resultMessage = _serviceFileBus.Add(fileBus);
            // For this example, assume the service returns a success message string
            bool success = resultMessage.Contains("نجاح");

            return Ok(new { success, message = resultMessage });
        }

        // Action to edit an existing National
        [HttpPost]
        public IActionResult Edit(VWFileBus fileBus )
        {
            ModelState.Remove("BusId");
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

            var resultMessage = _serviceFileBus.Edit(fileBus);
            bool success = resultMessage.Contains("نجاح");

            return Ok(new { success, message = resultMessage });
        }

        // Action to delete a National
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                string result = _serviceFileBus.Delete(id);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        [HttpGet("GetMinFileBus")]
        public async Task<IActionResult> GetMinFileBus()
        {
            var fileBus = await _serviceFileBus.GetMinFileBus();
            if (fileBus == null)
                return NotFound(new { Message = "No records found." });
            return Ok(fileBus);
        }

        [HttpGet("GetMaxFileBus")]
        public async Task<IActionResult> GetMaxFileBus()
        {
            var national = await _serviceFileBus.GetMaxFileBus();
            if (national == null)
                return NotFound(new { Message = "No records found." });
            return Ok(national);
        }

        [HttpGet("GetNextFileBus/{id}")]
        public async Task<IActionResult> GetNextFileBus(int id)
        {
            if (id == 0)
            {
                id = _serviceFileBus.GetMaxFileBusId();
            }
            var national = await _serviceFileBus.GetNextFileBus(id);
            if (national == null)
                return NotFound(new { Message = "No next record found." });
            return Ok(national);
        }

        [HttpGet("GetPreviousFileBus/{id}")]
        public async Task<IActionResult> GetPreviousFileBus(int id)
        {
            if (id == 0)
            {
                id = _serviceFileBus.GetMaxFileBusId();
            }
            var national = await _serviceFileBus.GetPreviousFileBus(id);
            if (national == null)
                return NotFound(new { Message = "No previous record found." });
            return Ok(national);
        }
        public IActionResult List()
        {
            var filebus=_serviceFileBus.GetAll();
            return View(filebus);
        }
        public IActionResult PrintBus(int id)
        {
            var bus = _serviceFileBus.GetbyId(id);

            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

    }
}
