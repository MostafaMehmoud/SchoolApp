using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicStageController : ControllerBase
    {
        private readonly IServiceStage _serviceStage;
        public AcademicStageController(IServiceStage serviceStage)
        {
            _serviceStage = serviceStage;
        }
       
        [HttpGet("GetAll")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<List<Stage>>>> GetAll()
        {
            var canAccessGrades = User.HasClaim(c => c.Type == "CanAccessGrades" && c.Value == "True");

            if (!canAccessGrades)
            {
                return Forbid(); // المستخدم ليس لديه صلاحية الوصول
            }
            var Stages = _serviceStage.GetAll().ToList();
            return Ok(ApiResponse<List<Stage>>.SuccessResponse(Stages));
        }
      
        [Permission("CanAccessGrades")]
        // GET: api/national/{id}
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ApiResponse<Stage>>> GetById(int id)
        {
            var stage = _serviceStage.GetbyId(id);
            if (stage == null)
                return NotFound(ApiResponse<Stage>.ErrorResponse(new List<string> { "Stage not found" }));

            return Ok(ApiResponse<Stage>.SuccessResponse(stage));
        }
      
        [Permission("CanAccessGrades")]
        // POST: api/national
        [HttpPost("Add")]
        public async Task<ActionResult<ApiResponse<Stage>>> Add(Stage stage)
        {
            var resualt=_serviceStage.Add(stage.StageCode, stage.StageName);
            if (resualt == "تم الحفظ بنجاح")
            {
                return CreatedAtAction(nameof(GetById), new { id = stage.Id },
                  ApiResponse<Stage>.SuccessResponse(stage, "Stage added successfully"));
            }
            else
            {
                return NotFound(ApiResponse<Stage>.ErrorResponse(new List<string> { "Stage failed to added" }));

            }
           
        }
      [Authorize]
        [Permission("CanAccessGrades")]
        // PUT: api/national/{id}
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ApiResponse<VWStage>>> Update(int id, VWStage vWStage)
        {
            if (id != vWStage.ID)
                return BadRequest(ApiResponse<Stage>.ErrorResponse(new List<string> { "ID mismatch" }));

           var resualt= _serviceStage.Edit(vWStage.ID, vWStage.code, vWStage.Name);
            if (resualt == "تم التعديل بنجاح")
            {
                return Ok(ApiResponse<VWStage>.SuccessResponse(vWStage, "Stage updated successfully"));

            }
            else
            {
                return NotFound(ApiResponse<VWStage>.ErrorResponse(new List<string> { "Stage failed to updated" }));

            }

           }
      [Authorize]
        [Permission("CanAccessGrades")]
        // DELETE: api/national/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ApiResponse<Stage>>> Delete(int id)
        {
            try
            {
                var stage = _serviceStage.GetbyId(id);
                if (stage == null)
                    return NotFound(ApiResponse<Stage>.ErrorResponse(new List<string> { "Stage not found" }));

                var resualt = _serviceStage.Delete(stage.Id);
                if (resualt == "تم الحذف بنجاح")
                {
                    return Ok(ApiResponse<Stage>.SuccessResponse(null, "Stage deleted successfully"));

                }
                else
                {
                    return NotFound(ApiResponse<VWStage>.ErrorResponse(new List<string> { "Stage failed to deleted" }));

                }
            }
            catch (Exception ex)
            {
                return NotFound(ApiResponse<VWStage>.ErrorResponse(new List<string> { ex.Message }));

            }


        }
      [Authorize]
        [Permission("CanAccessGrades")]
        [HttpGet("GetMinStage")]
        public async Task<IActionResult> GetMinStage()
        {
            var Stage = await _serviceStage.GetMinStage();
            if (Stage == null)
                return NotFound(new { Message = "No records found." });
            return Ok(Stage);
        }
      [Authorize]
        [Permission("CanAccessGrades")]
        [HttpGet("GetMaxStage")]
        public async Task<IActionResult> GetMaxStage()
        {
            var Stage = await _serviceStage.GetMaxStage();
            if (Stage == null)
                return NotFound(new { Message = "No records found." });
            return Ok(Stage);
        }
      [Authorize]
        [Permission("CanAccessGrades")]
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
      [Authorize]
        [Permission("CanAccessGrades")]
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
