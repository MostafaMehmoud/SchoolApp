using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.BL.Services.IServices
{
    public interface IServiceInstallment
    {
        public string Add(VWInstallment Installment);
        public string Edit(VWInstallment Installment);
        public string Delete(int id);
        public Installment GetbyId(int id);
        public IEnumerable<Installment> GetAll();
        public Task<int> GetNewCode();
        Task<Installment> GetNextInstallment(int id);
        Task<Installment> GetPreviousInstallment(int id);
        Task<Installment> GetMinInstallment();
        Task<Installment> GetMaxInstallment();
        int GetMaxInstallmentId();
        public List<Installment> GetAllInstallmentsByStageId(int StageId);
        public List<VWInstallmentStudent> GetAllInstallmentsByStageIdAndClassTypeId(int StageId, int ClassTypeId);      
        public Amount GetAmountofClassType(int StageId, int ClassTypeId);
    }
}
