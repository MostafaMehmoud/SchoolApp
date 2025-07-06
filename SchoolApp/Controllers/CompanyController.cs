using Microsoft.AspNetCore.Mvc;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;

namespace SchoolApp.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IServiceCompany _companyService;

        public CompanyController(IServiceCompany companyService)
        {
            _companyService = companyService;
        }

        public async Task<IActionResult> Index()
        {
            var company = await _companyService.GetCompanyAsync();
            return View(company ?? new Company());
        }

        [HttpPost]
        public async Task<IActionResult> Save(Company model, IFormFile logoFile)
        {
            if (logoFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    await logoFile.CopyToAsync(ms);
                    model.Logo = ms.ToArray();
                }
            }

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            await _companyService.SaveCompanyAsync(model);
            TempData["Success"] = "تم حفظ بيانات الشركة بنجاح";
            return RedirectToAction("Index");
        }

    }

}
