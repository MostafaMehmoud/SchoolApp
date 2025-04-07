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
        public PaymentController(IServiceStudent serviceStudent, IServicePayment servicePayment)
        {
            _serviceStudent = serviceStudent;
            
            _servicePayment = servicePayment;
        }
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
        public ActionResult Print(int id)
        {
            var voucher = _servicePayment.Print(id);
           

       
            string htmlContent = $@"
        <html>
        <head>
            <title>طباعة سند الصرف</title>
            <style>
                body {{ font-family: Arial, sans-serif; text-align: right; direction: rtl; }}
                .voucher-container {{ width: 80%; margin: auto; border: 2px solid black; padding: 20px; }}
                .voucher-header {{ text-align: center; font-size: 20px; font-weight: bold; }}
                .voucher-content {{ margin-top: 20px; }}
                .voucher-row {{ display: flex; justify-content: space-between; padding: 5px 0; }}
                .voucher-label {{ font-weight: bold; }}
                .signature-section {{ margin-top: 30px; display: flex; justify-content: space-between; }}
                .signature {{ text-align: center; width: 24%; border-top: 1px solid black; }}
            </style>
        </head>
        <body>
            <div class='voucher-container'>
                <div class='voucher-header'>سند صرف</div>
                <div class='voucher-content'>
                    <div class='voucher-row'><span class='voucher-label'>التاريخ:</span> {voucher.PaymentDate.ToShortDateString()}</div>
                    <div class='voucher-row'><span class='voucher-label'>المبلغ:</span> {voucher.Amount} ريال</div>
                    <div class='voucher-row'><span class='voucher-label'>إدفعوا إلى:</span> {voucher.StudentName}</div>
                    <div class='voucher-row'><span class='voucher-label'>المبلغ كتابةً:</span> {voucher.AmountName}</div>
                    <div class='voucher-row'><span class='voucher-label'>البنك:</span> {voucher.BankName}</div>
                    <div class='voucher-row'><span class='voucher-label'>شيك رقم:</span> {voucher.ChequeNumber}</div>
                </div>
                <div class='signature-section'>
                    <div class='signature'>المدير</div>
                    <div class='signature'>المحاسب</div>
                    <div class='signature'>أمين الصندوق</div>
                    <div class='signature'>المستلم</div>
                </div>
            </div>
            <script>window.print();</script>
        </body>
        </html>";
            return Content(htmlContent, "text/html");
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
