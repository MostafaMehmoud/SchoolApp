using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.API.Auth;
using SchoolApp.BL.Services;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IserviceAuth _iserviceAuth;
        public UsersController(IserviceAuth iserviceAuth)
        {
            _iserviceAuth = iserviceAuth;
        }
        [HttpGet("GetLevel")]
        public List<SelectListItemLevel> GetLevel()
        {
            List<SelectListItemLevel> Levels = new List<SelectListItemLevel>()
            {
                new SelectListItemLevel { Text = "مشرف", Value = UserLevels.Admin },
            new SelectListItemLevel { Text = "إضافة", Value = UserLevels.Add },
            new SelectListItemLevel { Text = "تعديل وإضافة", Value = UserLevels.EditAdd },
            new SelectListItemLevel { Text = "إظهار", Value = UserLevels.View }
            };
            return Levels;
        }
        [AuthorizeClaim("CanAccessUsersFile")]
        [HttpGet("GetAll")]
        
        public async Task<ActionResult<ApiResponse<IEnumerable<ApplicationUser>>>> GetAll()
        {
            var Users = await _iserviceAuth.GetAllUsers();
            return Ok(ApiResponse<IEnumerable<ApplicationUser>>.SuccessResponse(Users));
        }
        //  [Authorize]
        // GET: api/national/{id}
        [AuthorizeClaim("CanAccessUsersFile")]
        [HttpGet("GetById/{id}")]
        // [Permission("CanAccessUsersFile")]
        public async Task<ActionResult<ApiResponse<ApplicationUser>>> GetById(string id)
        {
            var User = await _iserviceAuth.GetUserByIdAsync(id);
            if (User == null)
                return NotFound(ApiResponse<ApplicationUser>.ErrorResponse(new List<string> { "User not found" }));

            return Ok(ApiResponse<ApplicationUser>.SuccessResponse(User));
        }
        //  [Authorize]
        // POST: api/national
        [AuthorizeClaim("CanAccessUsersFile")]
        [HttpPost("Add")]
        // [Permission("CanAccessUsersFile")]
        public async Task<ActionResult<ApiResponse<VWUser>>> Add(VWUser VWUser)
        {
            var resualt = await _iserviceAuth.RegisterAsync(VWUser);
            if (resualt.Message == "تم الحفظ بنجاح")
            {
                return CreatedAtAction(nameof(GetById), new { id = VWUser.Id },
                  ApiResponse<VWUser>.SuccessResponse(VWUser, "User added successfully"));
            }
            else
            {
                return NotFound(ApiResponse<VWUser>.ErrorResponse(new List<string> { "User failed to added" }));

            }

        }
        //  [Authorize]
        // PUT: api/national/{id}
        [AuthorizeClaim("CanAccessUsersFile")]
        [HttpPut("Update/{id}")]
        // [Permission("CanAccessUsersFile")]
        public async Task<ActionResult<ApiResponse<VWUser>>> Update(string id, VWUser vWUser)
        {



            if (id != vWUser.Id)
                return BadRequest(ApiResponse<VWUser>.ErrorResponse(new List<string> { "ID mismatch" }));

            var resualt = await _iserviceAuth.UpdateUserAsync(vWUser);
            if (resualt.Message == "تم التعديل بنجاح")
            {
                return Ok(ApiResponse<VWUser>.SuccessResponse(vWUser, "User updated successfully"));

            }
            else
            {
                return NotFound(ApiResponse<VWUser>.ErrorResponse(new List<string> { "User failed to updated" }));

            }

        }
        //  [Authorize]
        // DELETE: api/national/{id}
        [AuthorizeClaim("CanAccessUsersFile")]
        [HttpDelete("Delete/{id}")]
        // [Permission("CanAccessUsersFile")]
        public async Task<ActionResult<ApiResponse<VWUser>>> Delete(string id)
        {
            try
            {
                var User = await _iserviceAuth.GetUserByIdAsync(id);
                if (User == null)
                    return NotFound(ApiResponse<VWUser>.ErrorResponse(new List<string> { "User not found" }));

                var resualt = await _iserviceAuth.DeleteUserAsync(User.Id);
                if (resualt.Message == "تم الحذف بنجاح")
                {
                    return Ok(ApiResponse<VWUser>.SuccessResponse(null, "User deleted successfully"));

                }
                else
                {
                    return NotFound(ApiResponse<VWUser>.ErrorResponse(new List<string> { "User failed to deleted" }));

                }
            }
            catch (Exception ex)
            {
                return NotFound(ApiResponse<VWUser>.ErrorResponse(new List<string> { ex.Message }));

            }
        }
        //  [Authorize]
        [AuthorizeClaim("CanAccessUsersFile")]
        [HttpGet("GetMin")]
        // [Permission("CanAccessUsersFile")]
        public async Task<IActionResult> GetMin()
        {
            var ClassType = await _iserviceAuth.GetMin();
            if (ClassType == null)
                return NotFound(new { Message = "No records found." });
            return Ok(ClassType);
        }
        //  [Authorize]
        [AuthorizeClaim("CanAccessUsersFile")]
        [HttpGet("GetMax")]
        // [Permission("CanAccessUsersFile")]
        public async Task<IActionResult> GetMax()
        {
            var national = await _iserviceAuth.GetMax();
            if (national == null)
                return NotFound(new { Message = "No records found." });
            return Ok(national);
        }
        //  [Authorize]
        [AuthorizeClaim("CanAccessUsersFile")]
        [HttpGet("GetNext/{id}")]
        // [Permission("CanAccessUsersFile")]
        public async Task<IActionResult> GetNext(int id)
        {
            if (id == 0)
            {
                id = await _iserviceAuth.GetMaxIdOfItem();
            }
            var national = await _iserviceAuth.GetNextUser(id);
            if (national == null)
                return NotFound(new { Message = "No next record found." });
            return Ok(national);
        }
        //  [Authorize]
        [AuthorizeClaim("CanAccessUsersFile")]
        [HttpGet("GetPrevious/{id}")]
        // [Permission("CanAccessUsersFile")]
        public async Task<IActionResult> GetPrevious(int id = 0)
        {




            if (id == 0)
            {
                id = await _iserviceAuth.GetMaxIdOfItem();
            }
            var national = await _iserviceAuth.GetPreviousUser(id);
            if (national == null)
                return NotFound(new { Message = "No next record found." });
            return Ok(national);
        }
      //  [Authorize]

        [HttpPost("login")]
        // [Permission("CanAccessUsersFile")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var token = await _iserviceAuth.AuthenticateUser(model.Username, model.Password);
            if (token == null)
                return Unauthorized(new { Message = "اسم المستخدم أو كلمة المرور غير صحيحة" });

            return Ok(new { Token = token });
        }

     
    }
}
