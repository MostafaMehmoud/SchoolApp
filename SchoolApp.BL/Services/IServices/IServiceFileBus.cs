using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.ViewModels;

namespace SchoolApp.BL.Services.IServices
{
    public interface IServiceFileBus
    {
        public string Add(VWFileBus fileBus);
        public string Edit(VWFileBus fileBus);
        public string Delete(int id);
        public FileBus GetbyId(int id);
        public IEnumerable<FileBus> GetAll();
        public Task<int> GetNewCode();
        Task<FileBus> GetNextFileBus(int id);
        Task<FileBus> GetPreviousFileBus(int id);
        Task<FileBus> GetMinFileBus();
        Task<FileBus> GetMaxFileBus();
        int GetMaxFileBusId();
        public VWCostBus GetBusCostByBusIdAndBusCostTypeId(int busId, int busCostTypeId);
    }
}
