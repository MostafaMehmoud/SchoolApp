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
    public class PaymentController : ControllerBase
    {
        private readonly IServiceStudent _serviceStudent;
        private readonly IServicePayment _servicePayment;
        public PaymentController(IServiceStudent serviceStudent, IServicePayment servicePayment)
        {
            _serviceStudent = serviceStudent;

            _servicePayment = servicePayment;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ApiResponse<List<Payment>>>> GetAll()
        {
            var Payments = _servicePayment.GetAll().ToList();
            return Ok(ApiResponse<List<Payment>>.SuccessResponse(Payments));
        }
        [HttpGet("GetStudentDetailsCost")]
        public async Task<ActionResult<ApiResponse<VWStudentCostReceipt>>> GetStudentDetailsCost(int id)
        {
            VWStudentCostReceipt receipt = _servicePayment.GetStudent(id);
            return Ok(ApiResponse<VWStudentCostReceipt>.SuccessResponse(receipt));
        }
        // GET: api/national/{id}
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ApiResponse<Payment>>> GetById(int id)
        {
            var Payment = _servicePayment.GetbyId(id);
            if (Payment == null)
                return NotFound(ApiResponse<Payment>.ErrorResponse(new List<string> { "Payment not found" }));

            return Ok(ApiResponse<Payment>.SuccessResponse(Payment));
        }

        // POST: api/national
        [HttpPost("Add")]
        public async Task<ActionResult<ApiResponse<VWPayment>>> Add(VWPayment VWPayment)
        {
            var resualt = _servicePayment.Add(VWPayment);
            if (resualt == "تم الحفظ بنجاح")
            {
                return CreatedAtAction(nameof(GetById), new { id = VWPayment.Id },
                  ApiResponse<VWPayment>.SuccessResponse(VWPayment, "Payment added successfully"));
            }
            else
            {
                return NotFound(ApiResponse<VWPayment>.ErrorResponse(new List<string> { "Payment failed to added" }));

            }

        }

        // PUT: api/national/{id}
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<ApiResponse<VWPayment>>> Update(int id, VWPayment vWPayment)
        {
            if (id != vWPayment.Id)
                return BadRequest(ApiResponse<VWPayment>.ErrorResponse(new List<string> { "ID mismatch" }));

            var resualt = _servicePayment.Edit(vWPayment);
            if (resualt == "تم التعديل بنجاح")
            {
                return Ok(ApiResponse<VWPayment>.SuccessResponse(vWPayment, "Payment updated successfully"));

            }
            else
            {
                return NotFound(ApiResponse<VWPayment>.ErrorResponse(new List<string> { "Payment failed to updated" }));

            }

        }

        // DELETE: api/national/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ApiResponse<Payment>>> Delete(int id)
        {
            try
            {
                var Payment = _servicePayment.GetbyId(id);
                if (Payment == null)
                    return NotFound(ApiResponse<Payment>.ErrorResponse(new List<string> { "Payment not found" }));

                var resualt = _servicePayment.Delete(Payment.Id);
                if (resualt == "تم الحذف بنجاح")
                {
                    return Ok(ApiResponse<Payment>.SuccessResponse(null, "Payment deleted successfully"));

                }
                else
                {
                    return NotFound(ApiResponse<Payment>.ErrorResponse(new List<string> { "Payment failed to deleted" }));

                }
            }
            catch (Exception ex)
            {
                return NotFound(ApiResponse<Payment>.ErrorResponse(new List<string> { ex.Message }));

            }


        }
        [HttpGet("GetMinPayment")]
        public async Task<IActionResult> GetMinPayment()
        {
            var Payment = await _servicePayment.GetMinPayment();
            if (Payment == null)
                return NotFound(new { Message = "No records found." });
            return Ok(Payment);
        }

        [HttpGet("GetMaxPayment")]
        public async Task<IActionResult> GetMaxPayment()
        {
            var national = await _servicePayment.GetMaxPayment();
            if (national == null)
                return NotFound(new { Message = "No records found." });
            return Ok(national);
        }

        [HttpGet("GetNextPayment/{id}")]
        public async Task<IActionResult> GetNextPayment(int id)
        {
            if (id == 0)
            {
                id = _servicePayment.GetMaxPaymentId();
            }
            var national = await _servicePayment.GetNextPayment(id);
            if (national == null)
                return NotFound(new { Message = "No next record found." });
            return Ok(national);
        }

        [HttpGet("GetPreviousPayment/{id}")]
        public async Task<IActionResult> GetPreviousPayment(int id)
        {
            if (id == 0)
            {
                id = _servicePayment.GetMaxPaymentId();
            }
            var national = await _servicePayment.GetPreviousPayment(id);
            if (national == null)
                return NotFound(new { Message = "No previous record found." });
            return Ok(national);
        }
    }
}
