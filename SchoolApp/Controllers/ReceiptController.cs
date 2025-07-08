using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.BL.Services;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly IServiceStudent _serviceStudent;
        private readonly IServiceReceipt _servicerreceipt;
        private readonly IServiceCompany _serviceCompany;   
        public ReceiptController(IServiceStudent serviceStudent, IServiceReceipt servicerreceipt, IServiceCompany serviceCompany)
        {
            _serviceStudent = serviceStudent;
            _servicerreceipt = servicerreceipt;
            _serviceCompany = serviceCompany;
        }
        [Permission("CanAccessReceipts")]
        public IActionResult Index()
        {
            ViewBag.listStudents = new SelectList(_serviceStudent.GetAll(), "Id", "StudentNumber");
            return View(new VWReceipt());
        }
        public IActionResult GetStudentDetailsCost(int id)
        {
           VWStudentCostReceipt receipt=_serviceStudent.GetCostReceipt(id);

            return Json(receipt);
        }
        public async Task<IActionResult> GetNextCode()
        {
            try
            {
                var nextCode = await _servicerreceipt.GetNewCode();
                return Json(new { nextCode });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Exception in GetNextNationalCode: " + ex.ToString());
                // Return full details for debugging (remove before production)
                return StatusCode(500, new { error = ex.Message, details = ex.ToString() });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(VWReceipt VWReceipt)
        {
            ModelState.Remove("Id");
            ModelState.Remove("BankName");
            ModelState.Remove("Purpose");
            ModelState.Remove("ChequeDate");
            
            ModelState.Remove("installmentReceipts");
            if (!ModelState.IsValid)
            {
                // Collect validation errors
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );
                return BadRequest(errors);
            }

            // Use your BL service to add the record
            var resultMessage = _servicerreceipt.Add(VWReceipt);
            // For this example, assume the service returns a success message string
            bool success = resultMessage.Contains("نجاح");

            return Ok(new { success, message = resultMessage });
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                _servicerreceipt.Delete(id);
                return Json(new { success = true, message = "تم الحذف بنجاح" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
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
        public async Task<IActionResult> Print(int id)
        {
            var receipt = _servicerreceipt.GetbyId(id); // هذا يجب أن يكون async أيضًا إن أمكن
            var company = await _serviceCompany.GetCompanyAsync();

            var viewModel = new ReceiptViewModel
            {
                Code = receipt.Code,
                ReceiptDate = receipt.ReceiptDate,
                StudentName = receipt.StudentName,
                Amount = receipt.Amount,
                AmountName = receipt.AmountName,
                CashCheque = receipt.CashCheque,
                ChequeNumber = receipt.ChequeNumber,
                ChequeDate = receipt.ChequeDate,
                BankName = receipt.BankName,
                Purpose = receipt.Purpose,
                Installments = receipt.installmentReceipts // أو .Select(...) لو كنت تستخدم نوع مختلف
            };

            var model = new PrintViewModel<ReceiptViewModel>
            {
                model = viewModel,
                Company = company
            };

            return View(model);
        }

    }
}
