using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly IServiceDepartment _serviceDepartment;
        private readonly IServiceNational _serviceNational;
        private readonly IServiceStage _serviceStage;
        private readonly IServiceInstallment _serviceInstallment;   
        private readonly IServiceFileBus _servicefileBus;
        public StudentController(IServiceDepartment serviceDepartment, IServiceNational serviceNational, IServiceStage serviceStage, IServiceInstallment serviceInstallment, IServiceFileBus servicefileBus)
        {
            _serviceDepartment = serviceDepartment;
            _serviceNational = serviceNational;
            _serviceStage = serviceStage;
            _serviceInstallment = serviceInstallment;
            _servicefileBus = servicefileBus;
        }

        public IActionResult Index()
        {
            ViewBag.departments=new SelectList( _serviceDepartment.GetAll(),"Id","Name");
            ViewBag.nationals = new SelectList(_serviceNational.GetAll(), "ID", "Name");
            ViewBag.Stages = new SelectList(_serviceStage.GetAll(), "Id", "StageName");
            ViewBag.buses = new SelectList(_servicefileBus.GetAll(), "BusId", "BusPlate");
            return View();
        }
        [HttpGet]
        public IActionResult GetInstallmentsByClassType(int stageId, int classTypeId)
        {
            var installments = _serviceInstallment.GetAllInstallmentsByStageIdAndClassTypeId(stageId, classTypeId);
            var amountclasstype = _serviceInstallment.GetAmountofClassType(stageId, classTypeId);
            if (installments == null || !installments.Any())
            {
                return Json(new VWInstallmentStudent()
                {
                    AmountInstallment= amountclasstype.AmountPrice,
                    ClassTypeId=classTypeId,
                    Id=amountclasstype.Id

                }); // إرجاع مصفوفة فارغة عند عدم وجود بيانات
            }

            return Json(installments);
        }
        [HttpGet]
        public IActionResult GetBusCostByClassType(int buseId, int busCostTypeId)
        {
            var installments = _servicefileBus.GetBusCostByBusIdAndBusCostTypeId(buseId, busCostTypeId);

           

            return Json(installments);
        }
    }
}
