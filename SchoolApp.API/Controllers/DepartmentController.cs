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
    public class DepartmentController : ControllerBase
    {
        private readonly IServiceDepartment _serviceDepartment;
        public DepartmentController(IServiceDepartment serviceDepartment)
        {
            _serviceDepartment = serviceDepartment;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ApiResponse<List<Department>>>> GetAll()
        {
            var Departments = _serviceDepartment.GetAll().ToList();
            return Ok(ApiResponse<List<Department>>.SuccessResponse(Departments));
        }

        // GET: api/national/{id}
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ApiResponse<Department>>> GetById(int id)
        {
            var Department = _serviceDepartment.GetbyId(id);
            if (Department == null)
                return NotFound(ApiResponse<Department>.ErrorResponse(new List<string> { "Department not found" }));

            return Ok(ApiResponse<Department>.SuccessResponse(Department));
        }

        // POST: api/national
        [HttpPost("Add")]
        public async Task<ActionResult<ApiResponse<VWDepartment>>> Add(VWDepartment VWDepartment)
        {
            var resualt = _serviceDepartment.Add(VWDepartment);
            if (resualt == "تم الحفظ بنجاح")
            {
                return CreatedAtAction(nameof(GetById), new { id = VWDepartment.Id },
                  ApiResponse<VWDepartment>.SuccessResponse(VWDepartment, "Department added successfully"));
            }
            else
            {
                return NotFound(ApiResponse<VWDepartment>.ErrorResponse(new List<string> { "Department failed to added" }));

            }

        }

        // PUT: api/national/{id}
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ApiResponse<VWDepartment>>> Update(int id, VWDepartment vWDepartment)
        {
            if (id != vWDepartment.Id)
                return BadRequest(ApiResponse<VWDepartment>.ErrorResponse(new List<string> { "ID mismatch" }));

            var resualt = _serviceDepartment.Edit(vWDepartment);
            if (resualt == "تم التعديل بنجاح")
            {
                return Ok(ApiResponse<VWDepartment>.SuccessResponse(vWDepartment, "Department updated successfully"));

            }
            else
            {
                return NotFound(ApiResponse<VWDepartment>.ErrorResponse(new List<string> { "Department failed to updated" }));

            }

        }

        // DELETE: api/national/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ApiResponse<Department>>> Delete(int id)
        {
            try
            {
                var Department = _serviceDepartment.GetbyId(id);
                if (Department == null)
                    return NotFound(ApiResponse<Department>.ErrorResponse(new List<string> { "Department not found" }));

                var resualt = _serviceDepartment.Delete(Department.Id);
                if (resualt == "تم الحذف بنجاح")
                {
                    return Ok(ApiResponse<Department>.SuccessResponse(null, "Department deleted successfully"));

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
        [HttpGet("GetMinDepartment")]
        public async Task<IActionResult> GetMinDepartment()
        {
            var department = await _serviceDepartment.GetMinDepartment();
            if (department == null)
                return NotFound(new { Message = "No records found." });
            return Ok(department);
        }

        [HttpGet("GetMaxDepartment")]
        public async Task<IActionResult> GetMaxDepartment()
        {
            var department = await _serviceDepartment.GetMaxDepartment();
            if (department == null)
                return NotFound(new { Message = "No records found." });
            return Ok(department);
        }

        [HttpGet("GetNextDepartment/{id}")]
        public async Task<IActionResult> GetNextDepartment(int id)
        {
            if (id == 0)
            {
                id = _serviceDepartment.GetMaxDepartmentId();
            }
            var department = await _serviceDepartment.GetNextDepartment(id);
            if (department == null)
                return NotFound(new { Message = "No next record found." });
            return Ok(department);
        }

        [HttpGet("GetPreviousDepartment/{id}")]
        public async Task<IActionResult> GetPreviousDepartment(int id)
        {
            if (id == 0)
            {
                id = _serviceDepartment.GetMaxDepartmentId();
            }
            var department = await _serviceDepartment.GetPreviousDepartment(id);
            if (department == null)
                return NotFound(new { Message = "No previous record found." });
            return Ok(department);
        }
    }
}
