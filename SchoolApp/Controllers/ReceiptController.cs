using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.BL.Services.IServices;

namespace SchoolApp.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly IServiceStudent _serviceStudent;
        public ReceiptController(IServiceStudent serviceStudent)
        {
            _serviceStudent = serviceStudent;
        }
        public IActionResult Index()
        {
            ViewBag.listStudents = new SelectList(_serviceStudent.GetAll(), "Id", "StudentNumber");
            return View();
        }
    }
}
