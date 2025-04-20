using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.BL.Services.IServices
{
    public interface IserviceAuth
    {
        Task<OperationResult> RegisterAsync(VWUser vWUser);
        Task<OperationResult> LoginAsync(string username, string password);
        Task<OperationResult> UpdateUserAsync(VWUser vWUser);
        Task<OperationResult> DeleteUserAsync(string id);
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<ApplicationUser> GetMin();
        Task<ApplicationUser> GetMax();
        Task<ApplicationUser> GetNextUser(int id);
        Task<ApplicationUser> GetPreviousUser(int id);
        Task<int> GetMaxIdOfItem();
        Task<int> GetNewCode();
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
        Task Logout();
        Task<string> GenerateResetPasswordLinkAsync(string username, HttpRequest request, IUrlHelper urlHelper);
        Task<IdentityResult> ResetPasswordAsync(string username, string token, string newPassword);
    }
}
