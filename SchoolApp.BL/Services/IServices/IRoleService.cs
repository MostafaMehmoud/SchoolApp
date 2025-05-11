using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.BL.Services.IServices
{
    public interface IRoleService
    {
        Task<bool> AssignRole(string userId, string roleName);
        Task<bool> CheckUserPermission(string userId, string requiredRole);
    }

}
