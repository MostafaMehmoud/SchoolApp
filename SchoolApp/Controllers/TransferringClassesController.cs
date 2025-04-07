using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.BL.Services;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;

namespace SchoolApp.Controllers
{
    public class TransferringClassesController : Controller
    {
        private readonly IServiceStage _serviceStage;
        private readonly IServiceReport _serviceReport;
        public TransferringClassesController(IServiceStage serviceStage, IServiceReport serviceReport)
        {
            _serviceStage = serviceStage;
            _serviceReport = serviceReport;
        }
        public IActionResult Index()
        {
            ViewBag.Stages = new SelectList(_serviceStage.GetAll(), "Id", "StageName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TranseferringStudents(int fromStage, int fromClass, int toStage, int toClass)
        {
            try
            {
                string result =await _serviceReport.TraneferringClasses(fromStage, fromClass, toStage, toClass);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
