using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferringClassesController : ControllerBase
    {
        private readonly IServiceStage _serviceStage;
        private readonly IServiceReport _serviceReport;
        public TransferringClassesController(IServiceStage serviceStage, IServiceReport serviceReport)
        {
            _serviceStage = serviceStage;
            _serviceReport = serviceReport;
        }
        
        [HttpPost("TranseferringStudents")]
        public async Task<ActionResult<ApiResponse<object>>> TranseferringStudents(int fromStage, int fromClass, int toStage, int toClass)
        {
            try
            {
                string result = await _serviceReport.TraneferringClasses(fromStage, fromClass, toStage, toClass);
                return Ok(ApiResponse<object>.SuccessResponse(new object(), result));
            }
            catch (Exception ex)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string> { ex.Message }));

            }
        }
    }
}
