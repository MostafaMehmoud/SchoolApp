using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.API.Auth;
using SchoolApp.BL.Services;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IServiceFileBus _servicefileBus;
        private readonly IServiceStudent _servicestudent;
        private readonly IServiceInstallment _serviceInstallment;
        public StudentController(IServiceStudent serviceStudent, IServiceFileBus serviceFileBus, IServiceInstallment serviceInstallment)
        {
            _servicestudent = serviceStudent;
            _servicefileBus = serviceFileBus;
            _serviceInstallment = serviceInstallment;
        }
        //  [Authorize]
        [AuthorizeClaim("CanAccessStudentsFile")]
        [HttpGet("GetAll")]
       // [Permission("CanAccessStudentsFile")]
        public async Task<ActionResult<ApiResponse<List<Student>>>> GetAll()
        {
            var Students = _servicestudent.GetAll().ToList();
            return Ok(ApiResponse<List<Student>>.SuccessResponse(Students));
        }
        //  [Authorize]
        // [Permission("CanAccessStudentsFile")]
        // GET: api/national/{id}
        [AuthorizeClaim("CanAccessStudentsFile")]
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ApiResponse<Student>>> GetById(int id)
        {
            var Student = _servicestudent.GetbyId(id);
            if (Student == null)
                return NotFound(ApiResponse<Student>.ErrorResponse(new List<string> { "Student not found" }));

            return Ok(ApiResponse<Student>.SuccessResponse(Student));
        }
        //  [Authorize]
        // [Permission("CanAccessStudentsFile")]
        [AuthorizeClaim("CanAccessStudentsFile")]
        [HttpGet("GetBusCostByClassType")]
        public async Task<ActionResult<ApiResponse<VWCostBus>>> GetBusCostByClassType(int buseId, int busCostTypeId)
        {
            var installments = _servicefileBus.GetBusCostByBusIdAndBusCostTypeId(buseId, busCostTypeId);

            if (installments == null)
                return NotFound(ApiResponse<VWCostBus>.ErrorResponse(new List<string> { "CostBus not found" }));

            return Ok(ApiResponse<VWCostBus>.SuccessResponse(installments));
        }
        //  [Authorize]
        // [Permission("CanAccessStudentsFile")]
        [AuthorizeClaim("CanAccessStudentsFile")]
        [HttpGet("GetInstallmentsByClassType")]
        public async Task<ActionResult<ApiResponse<InstallmentMain>>> GetInstallmentsByClassType(int stageId, int classTypeId)
        {
            InstallmentMain installments = _serviceInstallment.GetAllInstallmentsByStageIdAndClassTypeId(stageId, classTypeId);
            if (installments == null || !installments.Installments.Any())
            {
                InstallmentMain InstallmentMain = _serviceInstallment.GetAInstallmentofClassType(stageId, classTypeId);
                if (InstallmentMain == null)
                {
                   
                    return NotFound(ApiResponse<InstallmentMain>.ErrorResponse(new List<string> { "لا توجد بيانات متاحة" }));

                }
                Ok(ApiResponse<InstallmentMain>.SuccessResponse(installments)); // إرجاع مصفوفة فارغة عند عدم وجود بيانات
            }
            return Ok(ApiResponse<InstallmentMain>.SuccessResponse(installments));
        }
        //  [Authorize]
        // [Permission("CanAccessStudentsFile")]
        // POST: api/national
        [AuthorizeClaim("CanAccessStudentsFile")]
        [HttpPost("Add")]
        public async Task<ActionResult<ApiResponse<VWStudent>>> Add(VWStudent VWStudent)
        {
            var resualt = _servicestudent.Add(VWStudent);
            if (resualt == "تم الحفظ بنجاح")
            {
                return CreatedAtAction(nameof(GetById), new { id = VWStudent.Id },
                  ApiResponse<VWStudent>.SuccessResponse(VWStudent, "Student added successfully"));
            }
            else
            {
                return NotFound(ApiResponse<VWStudent>.ErrorResponse(new List<string> { "Student failed to added" }));

            }

        }
        //  [Authorize]
        // [Permission("CanAccessStudentsFile")]
        // PUT: api/national/{id}
        [AuthorizeClaim("CanAccessStudentsFile")]
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ApiResponse<VWStudent>>> Update(int id, VWStudent vWStudent)
        {
            if (id != vWStudent.Id)
                return BadRequest(ApiResponse<VWStudent>.ErrorResponse(new List<string> { "ID mismatch" }));

            var resualt = _servicestudent.Edit(vWStudent);
            if (resualt == "تم التعديل بنجاح")
            {
                return Ok(ApiResponse<VWStudent>.SuccessResponse(vWStudent, "Student updated successfully"));

            }
            else
            {
                return NotFound(ApiResponse<VWStudent>.ErrorResponse(new List<string> { "Student failed to updated" }));

            }

        }
        //  [Authorize]
        // [Permission("CanAccessStudentsFile")]
        // DELETE: api/national/{id}
        [AuthorizeClaim("CanAccessStudentsFile")]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ApiResponse<Student>>> Delete(int id)
        {
            try
            {
                var Student = _servicestudent.GetbyId(id);
                if (Student == null)
                    return NotFound(ApiResponse<Student>.ErrorResponse(new List<string> { "Student not found" }));

                var resualt = _servicestudent.Delete(Student.Id);
                if (resualt == "تم الحذف بنجاح")
                {
                    return Ok(ApiResponse<Student>.SuccessResponse(null, "Student deleted successfully"));

                }
                else
                {
                    return NotFound(ApiResponse<Student>.ErrorResponse(new List<string> { "Student failed to deleted" }));

                }
            }
            catch (Exception ex)
            {
                return NotFound(ApiResponse<Student>.ErrorResponse(new List<string> { ex.Message }));

            }

        }
        //  [Authorize]
        // [Permission("CanAccessStudentsFile")]
        [AuthorizeClaim("CanAccessStudentsFile")]
        [HttpGet("GetMinStudent")]
        public async Task<IActionResult> GetMinStudent()
        {
            var Student = await _servicestudent.GetMinStudent();
            if (Student == null)
                return NotFound(new { Message = "No records found." });
            return Ok(Student);
        }
        //  [Authorize]
        // [Permission("CanAccessStudentsFile")]
        [AuthorizeClaim("CanAccessStudentsFile")]
        [HttpGet("GetMaxStudent")]
        public async Task<IActionResult> GetMaxStudent()
        {
            var national = await _servicestudent.GetMaxStudent();
            if (national == null)
                return NotFound(new { Message = "No records found." });
            return Ok(national);
        }
        //  [Authorize]
        // [Permission("CanAccessStudentsFile")]
        [AuthorizeClaim("CanAccessStudentsFile")]
        [HttpGet("GetNextStudent/{id}")]
        public async Task<IActionResult> GetNextStudent(int id)
        {
            if (id == 0)
            {
                id = _servicestudent.GetMaxStudentId();
            }
            var national = await _servicestudent.GetNextStudent(id);
            if (national == null)
                return NotFound(new { Message = "No next record found." });
            return Ok(national);
        }
        //  [Authorize]
        // [Permission("CanAccessStudentsFile")]
        [AuthorizeClaim("CanAccessStudentsFile")]
        [HttpGet("GetPreviousStudent/{id}")]
        public async Task<IActionResult> GetPreviousStudent(int id)
        {
            if (id == 0)
            {
                id = _servicestudent.GetMaxStudentId();
            }
            var national = await _servicestudent.GetPreviousStudent(id);
            if (national == null)
                return NotFound(new { Message = "No previous record found." });
            return Ok(national);
        }
    }
}