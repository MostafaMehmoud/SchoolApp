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
        [Permission("CanAccessUsersFile")]
        public async Task<IActionResult> Index()
        {
            var company = await _companyService.GetCompanyAsync();
            return View(company ?? new Company());
        }

        [HttpPost]
        public async Task<IActionResult> Save(Company model, IFormFile logoFile)
        {
            var existing = await _companyService.GetCompanyAsync();

            if (existing == null)
            {
                // تحقق من رفع الصورة عند الإضافة فقط
                if (logoFile == null || logoFile.Length == 0)
                {
                    ModelState.AddModelError("Logo", "يرجى رفع شعار الشركة");
                    return View("Index", model);
                }
            }
            ModelState.Remove("Logo");
            ModelState.Remove("logoFile");
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            // تحميل الصورة الجديدة إن وُجدت
            if (logoFile != null && logoFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await logoFile.CopyToAsync(ms);
                    model.Logo = ms.ToArray();
                }
            }
            else if (existing != null)
            {
                // استخدم الشعار القديم إن لم تُرسل صورة جديدة
                model.Logo = existing.Logo;
            }

            await _companyService.SaveCompanyAsync(model);

            TempData["Success"] = "تم حفظ بيانات الشركة بنجاح";
            return RedirectToAction("Index");
        }


    }

}
