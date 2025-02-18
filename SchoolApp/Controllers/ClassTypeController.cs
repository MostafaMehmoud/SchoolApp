using Microsoft.AspNetCore.Mvc;
using SchoolApp.DAL.Models;

namespace SchoolApp.Controllers
{
    public class ClassTypeController : Controller
    {
        public IActionResult Index()
        {
            return View(new FileBus());
        }
    }
}
