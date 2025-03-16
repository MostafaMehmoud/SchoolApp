using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.BL.Services.IServices
{
    public interface IServicePayment
    {
        public string Add(VWPayment Payment);
        public string Edit(VWPayment Payment);
        public string Delete(int id);
        public Payment GetbyId(int id);
        public IEnumerable<Payment> GetAll();
        public Task<int> GetNewCode();
        Task<VWPayment> GetNextPayment(int id);
        Task<VWPayment> GetPreviousPayment(int id);
        Task<VWPayment> GetMinPayment();
        Task<VWPayment> GetMaxPayment();
        int GetMaxPaymentId();
    }
}
