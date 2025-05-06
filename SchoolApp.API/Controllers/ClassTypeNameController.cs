using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassTypeNameController : ControllerBase
    {
        private readonly IServiceStage _serviceStage;
        private readonly IServiceClassTypeName _serviceClassTypeName;
        public ClassTypeNameController(IServiceStage serviceStage, IServiceClassTypeName serviceClassTypeName)
        {
            _serviceStage = serviceStage;
            _serviceClassTypeName = serviceClassTypeName;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ApiResponse<List<ClassTypeName>>>> GetAll()
        {
            var ClassTypeNames = _serviceClassTypeName.GetAll().ToList();
            return Ok(ApiResponse<List<ClassTypeName>>.SuccessResponse(ClassTypeNames));
        }

        // GET: api/national/{id}
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ApiResponse<ClassTypeName>>> GetById(int id)
        {
            var ClassTypeName = _serviceClassTypeName.GetbyId(id);
            if (ClassTypeName == null)
                return NotFound(ApiResponse<ClassTypeName>.ErrorResponse(new List<string> { "ClassTypeName not found" }));

            return Ok(ApiResponse<ClassTypeName>.SuccessResponse(ClassTypeName));
        }

        // POST: api/national
        [HttpPost("Add")]
        public async Task<ActionResult<ApiResponse<ClassTypeName>>> Add(VWClassTypeName VWClassTypeName)
        {
            var resualt = _serviceClassTypeName.Add(VWClassTypeName);
            if (resualt == "تم الحفظ بنجاح")
            {
                return CreatedAtAction(nameof(GetById), new { id = VWClassTypeName.Id },
                  ApiResponse<VWClassTypeName>.SuccessResponse(VWClassTypeName, "ClassTypeName added successfully"));
            }
            else
            {
                return NotFound(ApiResponse<VWClassTypeName>.ErrorResponse(new List<string> { "ClassTypeName failed to added" }));

            }

        }

        // PUT: api/national/{id}
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ApiResponse<VWClassTypeName>>> Update(int id, VWClassTypeName vWClassTypeName)
        {
            if (id != vWClassTypeName.Id)
                return BadRequest(ApiResponse<VWClassTypeName>.ErrorResponse(new List<string> { "ID mismatch" }));

            var resualt = _serviceClassTypeName.Edit(vWClassTypeName);
            if (resualt == "تم التعديل بنجاح")
            {
                return Ok(ApiResponse<VWClassTypeName>.SuccessResponse(vWClassTypeName, "ClassTypeName updated successfully"));

            }
            else
            {
                return NotFound(ApiResponse<VWClassTypeName>.ErrorResponse(new List<string> { "ClassTypeName failed to updated" }));

            }

        }

        // DELETE: api/national/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ApiResponse<ClassTypeName>>> Delete(int id)
        {
            try
            {
                var ClassTypeName = _serviceClassTypeName.GetbyId(id);
                if (ClassTypeName == null)
                    return NotFound(ApiResponse<ClassTypeName>.ErrorResponse(new List<string> { "ClassTypeName not found" }));

                var resualt = _serviceClassTypeName.Delete(ClassTypeName.Id);
                if (resualt == "تم الحذف بنجاح")
                {
                    return Ok(ApiResponse<ClassTypeName>.SuccessResponse(null, "ClassTypeName deleted successfully"));

                }
                else
                {
                    return NotFound(ApiResponse<ClassTypeName>.ErrorResponse(new List<string> { "ClassTypeName failed to deleted" }));

                }
            }
            catch (Exception ex)
            {
                return NotFound(ApiResponse<ClassTypeName>.ErrorResponse(new List<string> { ex.Message }));

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
