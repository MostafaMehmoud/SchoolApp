using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.BL.Services;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IServiceStudent _serviceStudent;
       private readonly IServicePayment _servicePayment;   
        private readonly IServiceCompany _serviceCompany;
        public PaymentController(IServiceStudent serviceStudent, IServicePayment servicePayment,
            IServiceCompany serviceCompany)
        {
            _serviceStudent = serviceStudent;
            _serviceCompany = serviceCompany;
            _servicePayment = servicePayment;
        }
        [Permission("CanAccessPayments")]
        public IActionResult Index()
        {
            ViewBag.listStudents = new SelectList(_serviceStudent.GetAll(), "Id", "StudentNumber");
            return View();
        }
        public IActionResult GetStudentDetailsCost(int id)
        {
            VWStudentCostReceipt receipt = _servicePayment.GetStudent(id);

            return Json(receipt);
        }
        public async Task<IActionResult> GetNextCode()
        {
            try
            {
                var nextCode = await _servicePayment.GetNewCode();
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
        public async Task<IActionResult> Add(VWPayment VWPayment)
        {
            ModelState.Remove("Id");
            ModelState.Remove("Id");
            ModelState.Remove("BankName");
            ModelState.Remove("Purpose");
            ModelState.Remove("ChequeDate");

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
            var resultMessage = _servicePayment.Add(VWPayment);
            // For this example, assume the service returns a success message string
            bool success = resultMessage.Contains("نجاح");

            return Ok(new { success, message = resultMessage });
        }
        public async Task<ActionResult> Print(int id)
        {
            var voucher = _servicePayment.Print(id);
           var company=await _serviceCompany.GetCompanyAsync();
            PrintViewModel<VWPayment> printViewModel = new PrintViewModel<VWPayment>
            {
                model = voucher,
                Company = company,
            };


            return View(printViewModel);
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
        [HttpPost]
        public IActionResult Delete(int id)
        {

            try
            {
                _servicePayment.Delete(id);
                return Json(new { success = true, message = "تم الحذف بنجاح" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

    }
}
