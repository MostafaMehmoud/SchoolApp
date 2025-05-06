using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassTypeController : ControllerBase
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
       
       
        [HttpGet("GetAll")]
        public async Task<ActionResult<ApiResponse<List<ClassType>>>> GetAll()
        {
            var ClassTypes = _serviceClassType.GetAll().ToList();
            return Ok(ApiResponse<List<ClassType>>.SuccessResponse(ClassTypes));
        }

        // GET: api/national/{id}
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ApiResponse<ClassType>>> GetById(int id)
        {
            var classType = _serviceClassType.GetbyId(id);
            if (classType == null)
                return NotFound(ApiResponse<ClassType>.ErrorResponse(new List<string> { "ClassType not found" }));

            return Ok(ApiResponse<ClassType>.SuccessResponse(classType));
        }

        // POST: api/national
        [HttpPost("Add")]
        public async Task<ActionResult<ApiResponse<ClassType>>> Add(VWClassType vWclassType)
        {
           string resualt= _serviceClassType.Add(vWclassType);
            if (resualt == "تم الحفظ بنجاح")
            {
                return CreatedAtAction(nameof(GetById), new { id = vWclassType.Id },
               ApiResponse<VWClassType>.SuccessResponse(vWclassType, "ClassType added successfully"));

            }
            else
            {
                return NotFound(ApiResponse<ClassType>.ErrorResponse(new List<string> { "ClassType failed to added" }));

            }

        }

        // PUT: api/national/{id}
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ApiResponse<VWClassType>>> Update(int id, VWClassType vWclassType)
        {
            if (id != vWclassType.Id)
                return BadRequest(ApiResponse<VWClassType>.ErrorResponse(new List<string> { "ID mismatch" }));

            string resualt = _serviceClassType.Edit(vWclassType);
            if(resualt == "تم التعديل بنجاح")
            {
                return Ok(ApiResponse<VWClassType>.SuccessResponse(vWclassType, "ClassType updated successfully"));

            }
            else
            {
                return NotFound(ApiResponse<National>.ErrorResponse(new List<string> { "classType failed to updated" }));

            }

        }

        // DELETE: api/national/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ApiResponse<ClassType>>> Delete(int id)
        {
            var classType = _serviceClassType.GetbyId(id);
            if (classType == null)
                return NotFound(ApiResponse<National>.ErrorResponse(new List<string> { "classType not found" }));

            _serviceClassType.Delete(classType.Id);


            return Ok(ApiResponse<National>.SuccessResponse(null, "classType deleted successfully"));
        }
        [HttpGet("GetClassTypeNamesByStage/{stageId}")]
        public async Task<ActionResult<ApiResponse<List<ClassTypeName>>>> GetClassTypeNamesByStage(int stageId)
        {
            var classTypeName = _serviceClassTypeName.GetAllClassTypesByStageId(stageId);
            if (classTypeName.Count==0)
                return NotFound(ApiResponse<ClassTypeName>.ErrorResponse(new List<string> { "ClassTypeName not found" }));

            return Ok(ApiResponse<List<ClassTypeName>>.SuccessResponse(classTypeName));

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
        public async Task<IActionResult> GetPreviousClassType(int id = 0)
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
    }
}
