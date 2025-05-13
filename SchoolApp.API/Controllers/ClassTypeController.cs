using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.API.Auth;
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
        [AuthorizeClaim("CanAccessGrades")]
        //  [Authorize]
        [HttpGet("GetAll")]
        //[Permission("CanAccessGrades")]
        public async Task<ActionResult<ApiResponse<List<ClassType>>>> GetAll()
        {
            var ClassTypes = _serviceClassType.GetAll().ToList();
            return Ok(ApiResponse<List<ClassType>>.SuccessResponse(ClassTypes));
        }
        //  [Authorize]
        [AuthorizeClaim("CanAccessGrades")]
        // GET: api/national/{id}
        [HttpGet("GetById/{id}")]
        //[Permission("CanAccessGrades")]
        public async Task<ActionResult<ApiResponse<ClassType>>> GetById(int id)
        {
            var classType = _serviceClassType.GetbyId(id);
            if (classType == null)
                return NotFound(ApiResponse<ClassType>.ErrorResponse(new List<string> { "ClassType not found" }));

            return Ok(ApiResponse<ClassType>.SuccessResponse(classType));
        }
        //  [Authorize]
        // POST: api/national
        [AuthorizeClaim("CanAccessGrades")]
        [HttpPost("Add")]
        //[Permission("CanAccessGrades")]
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
        //  [Authorize]
        // PUT: api/national/{id}
        [AuthorizeClaim("CanAccessGrades")]
        [HttpPut("Update/{id}")]
        //[Permission("CanAccessGrades")]
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
        //  [Authorize]
        // DELETE: api/national/{id}
        [AuthorizeClaim("CanAccessGrades")]
        [HttpDelete("Delete/{id}")]
        //[Permission("CanAccessGrades")]
        public async Task<ActionResult<ApiResponse<ClassType>>> Delete(int id)
        {
            var classType = _serviceClassType.GetbyId(id);
            if (classType == null)
                return NotFound(ApiResponse<National>.ErrorResponse(new List<string> { "classType not found" }));

            _serviceClassType.Delete(classType.Id);


            return Ok(ApiResponse<National>.SuccessResponse(null, "classType deleted successfully"));
        }
        //  [Authorize]
        [AuthorizeClaim("CanAccessGrades")]
        [HttpGet("GetClassTypeNamesByStage/{stageId}")]
        //[Permission("CanAccessGrades")]
        public async Task<ActionResult<ApiResponse<List<ClassTypeName>>>> GetClassTypeNamesByStage(int stageId)
        {
            var classTypeName = _serviceClassTypeName.GetAllClassTypesByStageId(stageId);
            if (classTypeName.Count==0)
                return NotFound(ApiResponse<ClassTypeName>.ErrorResponse(new List<string> { "ClassTypeName not found" }));

            return Ok(ApiResponse<List<ClassTypeName>>.SuccessResponse(classTypeName));

        }
        //  [Authorize]
        [AuthorizeClaim("CanAccessGrades")]
        [HttpGet("GetMinClassType")]
        //[Permission("CanAccessGrades")]
        public async Task<IActionResult> GetMinClassType()
        {
            var ClassType = await _serviceClassType.GetMinClassType();
            if (ClassType == null)
                return NotFound(new { Message = "No records found." });
            return Ok(ClassType);
        }
        //  [Authorize]
        [AuthorizeClaim("CanAccessGrades")]
        [HttpGet("GetMaxClassType")]
        //[Permission("CanAccessGrades")]
        public async Task<IActionResult> GetMaxClassType()
        {
            var national = await _serviceClassType.GetMaxClassType();
            if (national == null)
                return NotFound(new { Message = "No records found." });
            return Ok(national);
        }
        //  [Authorize]
        [AuthorizeClaim("CanAccessGrades")]
        [HttpGet("GetNextClassType/{id}")]
        //[Permission("CanAccessGrades")]
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
        //  [Authorize]
        [AuthorizeClaim("CanAccessGrades")]
        [HttpGet("GetPreviousClassType/{id}")]
        //[Permission("CanAccessGrades")]
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
