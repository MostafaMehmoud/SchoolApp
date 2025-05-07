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
    public class ReceiptController : ControllerBase
    {
        private readonly IServiceStudent _serviceStudent;
        private readonly IServiceReceipt _servicerreceipt;
        public ReceiptController(IServiceStudent serviceStudent, IServiceReceipt servicerreceipt)
        {
            _serviceStudent = serviceStudent;
            _servicerreceipt = servicerreceipt;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ApiResponse<List<Receipt>>>> GetAll()
        {
            var Receipts = _servicerreceipt.GetAll().ToList();
            return Ok(ApiResponse<List<Receipt>>.SuccessResponse(Receipts));
        }
        [HttpGet("GetStudentDetailsCost")]
        public async Task<ActionResult<ApiResponse<VWStudentCostReceipt>>> GetStudentDetailsCost(int id)
        {
            VWStudentCostReceipt receipt = _serviceStudent.GetCostReceipt(id);
            return Ok(ApiResponse<VWStudentCostReceipt>.SuccessResponse(receipt));
        }
        // GET: api/national/{id}
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ApiResponse<Receipt>>> GetById(int id)
        {
            var Receipt = _servicerreceipt.GetbyId(id);
            if (Receipt == null)
                return NotFound(ApiResponse<Receipt>.ErrorResponse(new List<string> { "Receipt not found" }));

            return Ok(ApiResponse<Receipt>.SuccessResponse(Receipt));
        }

        // POST: api/national
        [HttpPost("Add")]
        public async Task<ActionResult<ApiResponse<VWReceipt>>> Add(VWReceipt VWReceipt)
        {
            var resualt = _servicerreceipt.Add(VWReceipt);
            if (resualt == "تم الحفظ بنجاح")
            {
                return CreatedAtAction(nameof(GetById), new { id = VWReceipt.Id },
                  ApiResponse<VWReceipt>.SuccessResponse(VWReceipt, "Receipt added successfully"));
            }
            else
            {
                return NotFound(ApiResponse<VWReceipt>.ErrorResponse(new List<string> { "Receipt failed to added" }));

            }

        }

        // PUT: api/national/{id}
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ApiResponse<VWReceipt>>> Update(int id, VWReceipt vWReceipt)
        {
            if (id != vWReceipt.Id)
                return BadRequest(ApiResponse<VWReceipt>.ErrorResponse(new List<string> { "ID mismatch" }));

            var resualt = _servicerreceipt.Edit(vWReceipt);
            if (resualt == "تم التعديل بنجاح")
            {
                return Ok(ApiResponse<VWReceipt>.SuccessResponse(vWReceipt, "Receipt updated successfully"));

            }
            else
            {
                return NotFound(ApiResponse<VWReceipt>.ErrorResponse(new List<string> { "Receipt failed to updated" }));

            }

        }

        // DELETE: api/national/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ApiResponse<Receipt>>> Delete(int id)
        {
            try
            {
                var Receipt = _servicerreceipt.GetbyId(id);
                if (Receipt == null)
                    return NotFound(ApiResponse<Receipt>.ErrorResponse(new List<string> { "Receipt not found" }));

                var resualt = _servicerreceipt.Delete(Receipt.Id);
                if (resualt == "تم الحذف بنجاح")
                {
                    return Ok(ApiResponse<Receipt>.SuccessResponse(null, "Receipt deleted successfully"));

                }
                else
                {
                    return NotFound(ApiResponse<Receipt>.ErrorResponse(new List<string> { "Receipt failed to deleted" }));

                }
            }
            catch (Exception ex)
            {
                return NotFound(ApiResponse<Receipt>.ErrorResponse(new List<string> { ex.Message }));

            }


        }
        [HttpGet("GetMinReceipt")]
        public async Task<IActionResult> GetMinReceipt()
        {
            var Receipt = await _servicerreceipt.GetMinReceipt();
            if (Receipt == null)
                return NotFound(new { Message = "No records found." });
            return Ok(Receipt);
        }
        [HttpGet("GetMaxReceipt")]
        public async Task<IActionResult> GetMaxReceipt()
        {
            var national = await _servicerreceipt.GetMaxReceipt();
            if (national == null)
                return NotFound(new { Message = "No records found." });
            return Ok(national);
        }

        [HttpGet("GetNextReceipt/{id}")]
        public async Task<IActionResult> GetNextReceipt(int id)
        {
            if (id == 0)
            {
                id = _servicerreceipt.GetMaxReceiptId();
            }
            var national = await _servicerreceipt.GetNextReceipt(id);
            if (national == null)
                return NotFound(new { Message = "No next record found." });
            return Ok(national);
        }

        [HttpGet("GetPreviousReceipt/{id}")]
        public async Task<IActionResult> GetPreviousReceipt(int id)
        {
            if (id == 0)
            {
                id = _servicerreceipt.GetMaxReceiptId();
            }
            var national = await _servicerreceipt.GetPreviousReceipt(id);
            if (national == null)
                return NotFound(new { Message = "No previous record found." });
            return Ok(national);
        }
    }
}
