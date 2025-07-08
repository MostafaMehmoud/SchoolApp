using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.BL.Services.IServices
{
    public interface IDashboardService
    {
        Task<SchoolDashboardViewModel> GetDashboardDataAsync();
    }

}
