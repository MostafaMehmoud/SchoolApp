using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Repositories.IRepository;

namespace SchoolApp.BL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> AssignRole(string userId, string roleName)
        {
            return await _roleRepository.AssignRoleToUser(userId, roleName);
        }

        public async Task<bool> CheckUserPermission(string userId, string requiredRole)
        {
            var roles = await _roleRepository.GetUserRoles(userId);
            return roles.Contains(requiredRole);
        }
    }

}
