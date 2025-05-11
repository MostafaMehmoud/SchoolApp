using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.Repositories.IRepository
{
    public interface IRoleRepository
    {
        Task<bool> AssignRoleToUser(string userId, string roleName);
        Task<List<string>> GetUserRoles(string userId);
    }

}
