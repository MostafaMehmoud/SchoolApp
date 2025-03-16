using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.BL.Services.IServices
{
    public interface IServiceReceipt
    {
        public string Add(VWReceipt Receipt);
        public string Edit(VWReceipt Receipt);
        public string Delete(int id);
        public Receipt GetbyId(int id);
        public IEnumerable<Receipt> GetAll();
        public Task<int> GetNewCode();
        Task<VWReceipt> GetNextReceipt(int id);
        Task<VWReceipt> GetPreviousReceipt(int id);
        Task<VWReceipt> GetMinReceipt();
        Task<VWReceipt> GetMaxReceipt();
        int GetMaxReceiptId();
    }
}
