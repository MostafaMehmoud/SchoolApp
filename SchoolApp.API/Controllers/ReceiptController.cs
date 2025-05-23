﻿using Microsoft.AspNetCore.Authorization;
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
    public class ReceiptController : ControllerBase
    {
        private readonly IServiceStudent _serviceStudent;
        private readonly IServiceReceipt _servicerreceipt;
        public ReceiptController(IServiceStudent serviceStudent, IServiceReceipt servicerreceipt)
        {
            _serviceStudent = serviceStudent;
            _servicerreceipt = servicerreceipt;
        }
        //  [Authorize]
        [AuthorizeClaim("CanAccessReceipts")]
        [HttpGet("GetAll")]
        //[Permission("CanAccessReceipts")]
        public async Task<ActionResult<ApiResponse<List<Receipt>>>> GetAll()
        {
            var Receipts = _servicerreceipt.GetAll().ToList();
            return Ok(ApiResponse<List<Receipt>>.SuccessResponse(Receipts));
        }
        //  [Authorize]
        [AuthorizeClaim("CanAccessReceipts")]
        //[Permission("CanAccessReceipts")]
        [HttpGet("GetStudentDetailsCost")]
        public async Task<ActionResult<ApiResponse<VWStudentCostReceipt>>> GetStudentDetailsCost(int id)
        {
            VWStudentCostReceipt receipt = _serviceStudent.GetCostReceipt(id);
            return Ok(ApiResponse<VWStudentCostReceipt>.SuccessResponse(receipt));
        }
        //  [Authorize]
        //[Permission("CanAccessReceipts")]
        // GET: api/national/{id}
        [AuthorizeClaim("CanAccessReceipts")]
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ApiResponse<Receipt>>> GetById(int id)
        {
            var Receipt = _servicerreceipt.GetbyId(id);
            if (Receipt == null)
                return NotFound(ApiResponse<Receipt>.ErrorResponse(new List<string> { "Receipt not found" }));

            return Ok(ApiResponse<Receipt>.SuccessResponse(Receipt));
        }
        //  [Authorize]
        //[Permission("CanAccessReceipts")]
        // POST: api/national
        [AuthorizeClaim("CanAccessReceipts")]
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
        [AuthorizeClaim("CanAccessReceipts")]
        //  [Authorize]
        // PUT: api/national/{id}
        [HttpPut("Update/{id}")]
        //[Permission("CanAccessReceipts")]
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
        [AuthorizeClaim("CanAccessReceipts")]
        //  [Authorize]
        // DELETE: api/national/{id}
        //[Permission("CanAccessReceipts")]
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
        [AuthorizeClaim("CanAccessReceipts")]
        //  [Authorize]
        [HttpGet("GetMinReceipt")]
        //[Permission("CanAccessReceipts")]
        public async Task<IActionResult> GetMinReceipt()
        {
            var Receipt = await _servicerreceipt.GetMinReceipt();
            if (Receipt == null)
                return NotFound(new { Message = "No records found." });
            return Ok(Receipt);
        }
        [AuthorizeClaim("CanAccessReceipts")]
        //  [Authorize]
        [HttpGet("GetMaxReceipt")]
        //[Permission("CanAccessReceipts")]
        public async Task<IActionResult> GetMaxReceipt()
        {
            var national = await _servicerreceipt.GetMaxReceipt();
            if (national == null)
                return NotFound(new { Message = "No records found." });
            return Ok(national);
        }
        //  [Authorize]
        [AuthorizeClaim("CanAccessReceipts")]
        //[Permission("CanAccessReceipts")]
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
        //  [Authorize]
        [AuthorizeClaim("CanAccessReceipts")]
        //[Permission("CanAccessReceipts")]
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
