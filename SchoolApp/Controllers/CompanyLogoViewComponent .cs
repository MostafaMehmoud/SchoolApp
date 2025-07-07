using Microsoft.AspNetCore.Mvc;

using SchoolApp.BL.Services.IServices;


namespace SchoolApp.Controllers
{
   
    public class CompanyLogoViewComponent : ViewComponent
    {
        private readonly IServiceCompany _companyService;

        public CompanyLogoViewComponent(IServiceCompany companyService)
        {
            _companyService = companyService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var company = await _companyService.GetCompanyAsync();
            var logoBase64 = company?.Logo != null
                ? Convert.ToBase64String(company.Logo)
                : null;

            return View("Default", logoBase64);
        }
    }

}
