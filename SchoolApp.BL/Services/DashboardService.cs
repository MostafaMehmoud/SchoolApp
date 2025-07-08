using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.BL.Services.IServices;
using SchoolApp.DAL.Repositories.UnitOfWork;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.BL.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SchoolDashboardViewModel> GetDashboardDataAsync()
        {
            var totalStudents = ( _unitOfWork.students.GetAll()).Count();
            var totalFees =  _unitOfWork.students.GetAll().Sum(s => s.ReceiptTotalFees); // حسب خصائصك
            var totalReceipts =  _unitOfWork.receipts.GetAll().Sum(r => r.Amount);
            var totalPayments =  _unitOfWork.payments.GetAll().Sum(p => p.Amount);
            var totalBuses =  _unitOfWork.fileBus.GetAll().Count();

            return new SchoolDashboardViewModel
            {
                TotalStudents = totalStudents,
                TotalFees = totalFees,
                TotalPaid = totalReceipts,
                TotalExpenses = totalPayments,
                TotalBuses = totalBuses
            };
        }
    }

}
