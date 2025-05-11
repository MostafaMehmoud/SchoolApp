using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SchoolApp.BL.Services;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileBusController : ControllerBase
    {
        private readonly IServiceFileBus _serviceFileBus;
        private readonly IServiceStage _serviceStage;
        public FileBusController(IServiceFileBus serviceFileBus, IServiceStage serviceStage)
        {
            _serviceFileBus = serviceFileBus;
            _serviceStage = serviceStage;
        }
      //  [Authorize]
        [HttpGet("GetAll")]
        //[Permission("CanAccessBusesFile")]
        public async Task<ActionResult<ApiResponse<List<FileBus>>>> GetAll()
        {
            var FileBuses = _serviceFileBus.GetAll().ToList();
            return Ok(ApiResponse<List<FileBus>>.SuccessResponse(FileBuses));
        }
      //  [Authorize]

        // GET: api/national/{id}
        [HttpGet("GetById/{id}")]
        //[Permission("CanAccessBusesFile")]
        public async Task<ActionResult<ApiResponse<FileBus>>> GetById(int id)
        {
            var FileBus = _serviceFileBus.GetbyId(id);
            if (FileBus == null)
                return NotFound(ApiResponse<FileBus>.ErrorResponse(new List<string> { "FileBus not found" }));

            return Ok(ApiResponse<FileBus>.SuccessResponse(FileBus));
        }
      //  [Authorize]
        //[Permission("CanAccessBusesFile")]
        // POST: api/national
        [HttpPost("Add")]
        public async Task<ActionResult<ApiResponse<VWFileBus>>> Add(VWFileBus VWFileBus)
        {
            var resualt = _serviceFileBus.Add(VWFileBus);
            if (resualt == "تم الحفظ بنجاح")
            {
                return CreatedAtAction(nameof(GetById), new { id = VWFileBus.BusId },
                  ApiResponse<VWFileBus>.SuccessResponse(VWFileBus, "FileBus added successfully"));
            }
            else
            {
                return NotFound(ApiResponse<VWFileBus>.ErrorResponse(new List<string> { "FileBus failed to added" }));

            }

        }
      //  [Authorize]
        //[Permission("CanAccessBusesFile")]
        // PUT: api/national/{id}
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ApiResponse<VWFileBus>>> Update(int id, VWFileBus VWFileBus)
        {
            if (id != VWFileBus.BusId)
                return BadRequest(ApiResponse<VWFileBus>.ErrorResponse(new List<string> { "ID mismatch" }));

            var resualt = _serviceFileBus.Edit(VWFileBus);
            if (resualt == "تم التعديل بنجاح")
            {
                return Ok(ApiResponse<VWFileBus>.SuccessResponse(VWFileBus, "FileBus updated successfully"));

            }
            else
            {
                return NotFound(ApiResponse<VWFileBus>.ErrorResponse(new List<string> { "FileBus failed to updated" }));

            }

        }
      //  [Authorize]
        //[Permission("CanAccessBusesFile")]
        // DELETE: api/national/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ApiResponse<FileBus>>> Delete(int id)
        {
            try
            {
                var FileBus = _serviceFileBus.GetbyId(id);
                if (FileBus == null)
                    return NotFound(ApiResponse<FileBus>.ErrorResponse(new List<string> { "Department not found" }));

                var resualt = _serviceFileBus.Delete(FileBus.BusId);
                if (resualt == "تم الحذف بنجاح")
                {
                    return Ok(ApiResponse<FileBus>.SuccessResponse(null, "Department deleted successfully"));

                }
                else
                {
                    return NotFound(ApiResponse<Department>.ErrorResponse(new List<string> { "Department failed to deleted" }));

                }
            }
            catch (Exception ex)
            {
                return NotFound(ApiResponse<Department>.ErrorResponse(new List<string> { ex.Message }));

            }


        }
      //  [Authorize]
        //[Permission("CanAccessBusesFile")]
        [HttpGet("GetMinFileBus")]
        public async Task<IActionResult> GetMinFileBus()
        {
            var fileBus = await _serviceFileBus.GetMinFileBus();
            if (fileBus == null)
                return NotFound(new { Message = "No records found." });
            return Ok(fileBus);
        }
      //  [Authorize]
        //[Permission("CanAccessBusesFile")]
        [HttpGet("GetMaxFileBus")]
        public async Task<IActionResult> GetMaxFileBus()
        {
            var national = await _serviceFileBus.GetMaxFileBus();
            if (national == null)
                return NotFound(new { Message = "No records found." });
            return Ok(national);
        }
      //  [Authorize]
        //[Permission("CanAccessBusesFile")]
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
      //  [Authorize]
        //[Permission("CanAccessBusesFile")]
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
    }
}
