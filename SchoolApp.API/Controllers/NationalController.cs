using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalController : ControllerBase
    {
        private readonly IServiceNational _serviceNational;
        public NationalController(IServiceNational serviceNational)
        {
            _serviceNational = serviceNational; 
        }
      //  [Authorize]
        [HttpGet("GetAll")]
       // [Permission("CanAccessBusesFile")]
        public async Task<ActionResult<ApiResponse<List<National>>>> GetAll()
        {
            var nationals =  _serviceNational.GetAll().ToList();
            return Ok(ApiResponse<List<National>>.SuccessResponse(nationals));
        }
      //  [Authorize]
       // [Permission("CanAccessBusesFile")]
        // GET: api/national/{id}
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ApiResponse<National>>> GetById(int id)
        {
            var national = _serviceNational.GetbyId(id);
            if (national == null)
                return NotFound(ApiResponse<National>.ErrorResponse(new List<string> { "National not found" }));

            return Ok(ApiResponse<National>.SuccessResponse(national));
        }
      //  [Authorize]
       // [Permission("CanAccessBusesFile")]
        // POST: api/national
        [HttpPost("Add")]
        public async Task<ActionResult<ApiResponse<National>>> Add(National national)
        {
            var result=_serviceNational.Add(national.code,national.Name);
            if(result == "تم الحفظ بنجاح")
            {
                return CreatedAtAction(nameof(GetById), new { id = national.ID },
                ApiResponse<National>.SuccessResponse(national, "National added successfully"));
            }
            else
            {
                return NotFound(ApiResponse<National>.ErrorResponse(new List<string> { "National failed to added" }));
            }
            
        }
      //  [Authorize]
       // [Permission("CanAccessBusesFile")]
        // PUT: api/national/{id}
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ApiResponse<VWNationals>>> Update(int id, VWNationals national)
        {
            if (id != national.ID)
                return BadRequest(ApiResponse<VWNationals>.ErrorResponse(new List<string> { "ID mismatch" }));

           var result= _serviceNational.Edit( national.ID,national.code,national.Name);
            if (result == "تم التعديل بنجاح")
            {
                return Ok(ApiResponse<VWNationals>.SuccessResponse(national, "National updated successfully"));

            }
            else
            {
                return NotFound(ApiResponse<VWNationals>.ErrorResponse(new List<string> { "National failed to updated" }));

            }

            return Ok(ApiResponse<VWNationals>.SuccessResponse(national, "National updated successfully"));
        }
      //  [Authorize]
       // [Permission("CanAccessBusesFile")]
        // DELETE: api/national/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ApiResponse<National>>> Delete(int id)
        {
            var national = _serviceNational.GetbyId(id);
            if (national == null)
                return NotFound(ApiResponse<National>.ErrorResponse(new List<string> { "National not found" }));

           var result= _serviceNational.Delete(national.ID);


            if (result == "تم الحذف بنجاح")
            {
                return CreatedAtAction(nameof(GetById), new { id = national.ID },
                ApiResponse<National>.SuccessResponse(national, "National added successfully"));
            }
            else
            {
                return NotFound(ApiResponse<National>.ErrorResponse(new List<string> { "National failed to deleted" }));
            }
        }
      //  [Authorize]
       // [Permission("CanAccessBusesFile")]
        [HttpGet("GetMinNational")]
        public async Task<ActionResult<ApiResponse<National>>> GetMinNational()
        {
            var national = await _serviceNational.GetMinNational();

            if (national == null )
                return NotFound(ApiResponse<National>.ErrorResponse(new List<string> { "لم يتم العثور على بيانات الوطنية" }));

            return Ok(ApiResponse<National>.SuccessResponse(national));
        }
      //  [Authorize]
       // [Permission("CanAccessBusesFile")]
        [HttpGet("GetMaxNational")]
        public async Task<ActionResult<ApiResponse<National>>> GetMaxNational()
        {
            var national = await _serviceNational.GetMaxNational();
            if (national == null)
                return NotFound(ApiResponse<National>.ErrorResponse(new List<string> { "لم يتم العثور على بيانات الوطنية" }));
            return Ok(ApiResponse<National>.SuccessResponse(national));
        }
      //  [Authorize]
       // [Permission("CanAccessBusesFile")]
        [HttpGet("GetNextNational/{id}")]
        public async Task<ActionResult<ApiResponse<National>>> GetNextNational(int id)
        {
            if (id == 0)
            {
                id = _serviceNational.GetMaxNationalId();
            }
            var national = await _serviceNational.GetNextNational(id);
            if (national == null)
                return NotFound(ApiResponse<National>.ErrorResponse(new List<string> { "لم يتم العثور على بيانات الوطنية" }));
            return Ok(ApiResponse<National>.SuccessResponse(national));
        }
      //  [Authorize]
       // [Permission("CanAccessBusesFile")]
        [HttpGet("GetPreviousNational/{id}")]
        public async Task<ActionResult<ApiResponse<National>>> GetPreviousNational(int id)
        {
            if (id == 0)
            {
                id = _serviceNational.GetMaxNationalId();
            }
            var national = await _serviceNational.GetPreviousNational(id);
            if (national == null)
                return NotFound(ApiResponse<National>.ErrorResponse(new List<string> { "لم يتم العثور على بيانات الوطنية" }));
            return Ok(ApiResponse<National>.SuccessResponse(national));
        }
    }
}
