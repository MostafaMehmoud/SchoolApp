using Microsoft.AspNetCore.Http;
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
        [HttpGet("GetAll")]
        public async Task<ActionResult<ApiResponse<List<National>>>> GetAll()
        {
            var nationals =  _serviceNational.GetAll().ToList();
            return Ok(ApiResponse<List<National>>.SuccessResponse(nationals));
        }

        // GET: api/national/{id}
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ApiResponse<National>>> GetById(int id)
        {
            var national = _serviceNational.GetbyId(id);
            if (national == null)
                return NotFound(ApiResponse<National>.ErrorResponse(new List<string> { "National not found" }));

            return Ok(ApiResponse<National>.SuccessResponse(national));
        }

        // POST: api/national
        [HttpPost("Add")]
        public async Task<ActionResult<ApiResponse<National>>> Add(National national)
        {
            _serviceNational.Add(national.code,national.Name);
            
            return CreatedAtAction(nameof(GetById), new { id = national.ID },
                ApiResponse<National>.SuccessResponse(national, "National added successfully"));
        }

        // PUT: api/national/{id}
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ApiResponse<VWNationals>>> Update(int id, VWNationals national)
        {
            if (id != national.ID)
                return BadRequest(ApiResponse<National>.ErrorResponse(new List<string> { "ID mismatch" }));

            _serviceNational.Edit( national.ID,national.code,national.Name);
           

            return Ok(ApiResponse<VWNationals>.SuccessResponse(national, "National updated successfully"));
        }

        // DELETE: api/national/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ApiResponse<National>>> Delete(int id)
        {
            var national = _serviceNational.GetbyId(id);
            if (national == null)
                return NotFound(ApiResponse<National>.ErrorResponse(new List<string> { "National not found" }));

            _serviceNational.Delete(national.ID);
            

            return Ok(ApiResponse<National>.SuccessResponse(null, "National deleted successfully"));
        }
        [HttpGet("GetMinNational")]
        public async Task<ActionResult<ApiResponse<National>>> GetMinNational()
        {
            var national = await _serviceNational.GetMinNational();

            if (national == null )
                return NotFound(ApiResponse<National>.ErrorResponse(new List<string> { "لم يتم العثور على بيانات الوطنية" }));

            return Ok(ApiResponse<National>.SuccessResponse(national));
        }


        [HttpGet("GetMaxNational")]
        public async Task<ActionResult<ApiResponse<National>>> GetMaxNational()
        {
            var national = await _serviceNational.GetMaxNational();
            if (national == null)
                return NotFound(ApiResponse<National>.ErrorResponse(new List<string> { "لم يتم العثور على بيانات الوطنية" }));
            return Ok(ApiResponse<National>.SuccessResponse(national));
        }

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
