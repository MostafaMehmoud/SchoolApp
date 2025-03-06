using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolApp.BL.Services;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.Controllers
{
    public class ClassTypeController : Controller
    {
        private readonly IServiceStage _serviceStage;
        private readonly IServiceClassTypeName _serviceClassTypeName;
        private readonly IServiceClassType _serviceClassType;
        public ClassTypeController(IServiceStage serviceStage, IServiceClassTypeName serviceClassTypeName,
            IServiceClassType serviceClassType)
        {
            _serviceStage = serviceStage;
            _serviceClassTypeName = serviceClassTypeName;
            _serviceClassType = serviceClassType;
        }

        public IActionResult Index()
        {
            ViewBag.listStages = new SelectList(_serviceStage.GetAll(), "Id", "StageName");
            return View();
        }
        [HttpGet]
        public IActionResult GetClassTypeNamesByStage(int stageId)
        {
            // استخرج قائمة أنواع الفصول (ClassTypeName) حيث StageId يساوي القيمة المُرسلة

            var list = _serviceClassTypeName.GetAllClassTypesByStageId(stageId);
            return Json(list);
        }
        public async Task<IActionResult> GetNextCode()
        {
            try
            {
                var nextCode = await _serviceClassType.GetNewCode();
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
        public async Task<IActionResult> Add(VWClassType classTypeData)
        {
            ModelState.Remove("Id");
            ModelState.Remove("StageName");
            ModelState.Remove("CLSAcpt");
            ModelState.Remove("CLSRegs");
            ModelState.Remove("CLSCloth");
            ModelState.Remove("amounts.ClassTypeName");
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
            var resultMessage = _serviceClassType.Add(classTypeData);
            // For this example, assume the service returns a success message string
            bool success = resultMessage.Contains("نجاح");

            return Ok(new { success, message = resultMessage });
        }
         [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                string result = _serviceClassType.Delete(id);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
        
        [HttpPost]
        public IActionResult Edit(VWClassType classTypeData)
        {

            ModelState.Remove("StageName");
            ModelState.Remove("CLSAcpt");
            ModelState.Remove("CLSRegs");
            ModelState.Remove("CLSCloth");
            ModelState.Remove("amounts.ClassTypeName");
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

            var resultMessage = _serviceClassType.Edit(classTypeData);
            bool success = resultMessage.Contains("نجاح");

            return Ok(new { success, message = resultMessage });
        }
        [HttpGet("GetMinClassType")]
        public async Task<IActionResult> GetMinClassType()
        {
            var ClassType = await _serviceClassType.GetMinClassType();
            if (ClassType == null)
                return NotFound(new { Message = "No records found." });
            return Ok(ClassType);
        }

        [HttpGet("GetMaxClassType")]
        public async Task<IActionResult> GetMaxClassType()
        {
            var national = await _serviceClassType.GetMaxClassType();
            if (national == null)
                return NotFound(new { Message = "No records found." });
            return Ok(national);
        }

        [HttpGet("GetNextClassType/{id}")]
        public async Task<IActionResult> GetNextClassType(int id)
        {
            if (id == 0)
            {
                id = _serviceClassType.GetMaxClassTypeId();
            }
            var national = await _serviceClassType.GetNextClassType(id);
            if (national == null)
                return NotFound(new { Message = "No next record found." });
            return Ok(national);
        }

        [HttpGet("GetPreviousClassType/{id}")]
        public async Task<IActionResult> GetPreviousClassType(int id=0)
        {
          
           

           
            if (id == 0)
            {
                id = _serviceClassType.GetMaxClassTypeId();
            }
            var national = await _serviceClassType.GetPreviousClassType(id);
            if (national == null)
                return NotFound(new { Message = "No next record found." });
            return Ok(national);
        }
        public IActionResult List()
        {
            var classTypes=_serviceClassType.GetAllClassTypes();
            return View(classTypes);
        }
        public IActionResult Print(int id)
        {
            var bus = _serviceClassType.GetClassTypesById(id);

            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

    }
}
