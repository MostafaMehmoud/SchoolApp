using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.Repositories.UnitOfWork;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.BL.Services
{
    public class ServiseAuth:IserviceAuth
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public ServiseAuth(IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork; 
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<string> AuthenticateUser(string username, string password)
        {
            var user = await _unitOfWork.auth.GetUserByUsernameAsync(username);
            if (user == null || !await _unitOfWork.auth.ValidateUserCredentials(username, password))
                return null;

            return GenerateJwtToken(user);
        }

        public async Task<bool> RegisterUser(ApplicationUser user, string password)
        {
            return await _unitOfWork.auth.CreateUser(user, password);
        }

        public string GenerateJwtToken(ApplicationUser user)
        {
            var secretKey = _configuration["JwtSettings:Secret"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim("UserNumber", user.UserNumber.ToString()),
        new Claim("Level", user.Level),
        new Claim("CanAccessGrades", user.CanAccessGrades.ToString()),
        new Claim("CanAccessStudentsFile", user.CanAccessStudentsFile.ToString()),
        new Claim("CanAccessBusesFile", user.CanAccessBusesFile.ToString()),
        new Claim("CanAccessTypesEncoming", user.CanAccessTypesEncoming.ToString()),
        new Claim("CanAccessReceipts", user.CanAccessReceipts.ToString()),
        new Claim("CanAccessPayments", user.CanAccessPayments.ToString()),
        new Claim("CanAccessPrintReports", user.CanAccessPrintReports.ToString()),
        new Claim("CanAccessSearch", user.CanAccessSearch.ToString()),
        new Claim("CanAccessUsersFile", user.CanAccessUsersFile.ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpirationInMinutes"])),
            signingCredentials: credentials
        );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateResetPasswordLinkAsync(string username, HttpRequest request, IUrlHelper urlHelper)
        {
            var user = await _unitOfWork.auth.GetUserByUsernameAsync(username);
            if (user == null)
                return null;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return urlHelper.Action("ResetPassword", "Users", new
            {
                token,
                username
            }, request.Scheme);
        }

        public async Task<IdentityResult> ResetPasswordAsync(string username, string token, string newPassword)
        {
            var user = await _unitOfWork.auth.GetUserByUsernameAsync(username);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "المستخدم غير موجود." });

            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }
        public async Task<OperationResult> RegisterAsync(VWUser vWUser)
        {
            var user = new ApplicationUser
            {
                UserName = vWUser.UserName,
                PasswordHash = vWUser.Password, // انتبه: الأفضل استخدام UserManager لإنشاء
                Level = vWUser.Level,
                UserNumber=vWUser.UserNumber,
                CanAccessGrades=vWUser.CanAccessGrades,
                CanAccessStudentsFile = vWUser.CanAccessStudentsFile,
                CanAccessBusesFile = vWUser.CanAccessBusesFile,
                CanAccessTypesEncoming = vWUser.CanAccessTypesEncoming,
                CanAccessReceipts = vWUser.CanAccessReceipts,
                CanAccessPayments = vWUser.CanAccessPayments,
                CanAccessPrintReports = vWUser.CanAccessPrintReports,
                CanAccessSearch = vWUser.CanAccessSearch,
                CanAccessUsersFile= vWUser.CanAccessUsersFile
                // باقي الخصائص إن وجدت
            };

            return await _unitOfWork.auth.Register(user, vWUser.Password);
        }

        public async Task<OperationResult> LoginAsync(string username, string password)
        {
            return await _unitOfWork.auth.Login(username, password);
        }

        public async Task<OperationResult> UpdateUserAsync(VWUser vWUser)
        {
            var user = await _unitOfWork.auth.GetUserById(vWUser.Id);
            if (user == null)
            {
                return new OperationResult
                {
                    Success = false,
                    Errors = new List<string> { "المستخدم غير موجود" }
                };
            }

            user.UserName = vWUser.UserName;
            user.Level = vWUser.Level;
            user.UserNumber = vWUser.UserNumber;
            user.CanAccessGrades= vWUser.CanAccessGrades;
            user.CanAccessStudentsFile= vWUser.CanAccessStudentsFile;
            user.CanAccessBusesFile= vWUser.CanAccessBusesFile;
            user.CanAccessTypesEncoming=vWUser.CanAccessTypesEncoming;      
            user.CanAccessReceipts= vWUser.CanAccessReceipts;
            user.CanAccessPayments = vWUser.CanAccessPayments;
            user.CanAccessPrintReports= vWUser.CanAccessPrintReports;
            user.CanAccessSearch=vWUser.CanAccessSearch;
            user.CanAccessUsersFile= vWUser.CanAccessUsersFile;
            // باقي التحديثات

            return await _unitOfWork.auth.UpdateUser(user);
        }

        public async Task<OperationResult> DeleteUserAsync(string id)
        {
            var user = await _unitOfWork.auth.GetUserById(id);
            if (user == null)
            {
                return new OperationResult
                {
                    Success = false,
                    Errors = new List<string> { "المستخدم غير موجود" }
                };
            }

            return await _unitOfWork.auth.DeleteUser(user);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _unitOfWork.auth.GetUserById(id);
        }

        public async Task<ApplicationUser> GetMin()
        {
            return await _unitOfWork.auth.GetMin();
        }

        public async Task<ApplicationUser> GetMax()
        {
            return await _unitOfWork.auth.GetMax();
        }

        public async Task<ApplicationUser> GetNextUser(int id)
        {
            return await _unitOfWork.auth.GetNextOrPreviousItemByCode(id, "next");
        }

        public async Task<ApplicationUser> GetPreviousUser(int id)
        {
            return await _unitOfWork.auth.GetNextOrPreviousItemByCode(id, "previous");
        }

        public async Task<int> GetMaxIdOfItem()
        {
            return _unitOfWork.auth.GetMaxIdOfItem();
        }

        public async Task<int> GetNewCode()
        {
            return await _unitOfWork.auth.GetNewCode();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            return await _unitOfWork.auth.GetAllUsers();
        }

        public async Task Logout()
        {
           await _unitOfWork.auth.Logout();
        }
    }
}
