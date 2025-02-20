using Microsoft.AspNetCore.Mvc;

namespace SchoolApp.Controllers
{
    public class StudentController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
