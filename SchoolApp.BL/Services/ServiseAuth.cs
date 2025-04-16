using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.Repositories.UnitOfWork;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.BL.Services
{
    public class ServiseAuth:IserviceAuth
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiseAuth(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
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
    }
}
