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
    public class ServiceFileBus : IServiceFileBus
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceFileBus(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string Add(VWFileBus fileBus)
        {
            var filBus = new FileBus
            {
                BusId=fileBus.BusId,
                BusCode=fileBus.BusCode,
                BusAll=fileBus.BusAll,
                BusDrive=fileBus.BusDrive,
                BusGo=fileBus.BusGo,
                BusNumber=fileBus.BusNumber,
                BusPlate=fileBus.BusPlate,
                BusReturn=fileBus.BusReturn,
                BusRoute=fileBus.BusRoute,
                BusState=fileBus.BusState,
                BusStud=fileBus.BusStud,
                BusSup=fileBus.BusSup,
                MobilPhone1=fileBus.MobilPhone1,
                MobilPhone2=fileBus.MobilPhone2,
                
            };
            if (_unitOfWork.fileBus.Add(filBus))
            {
                return "تم الحفظ بنجاح";
            }
            else
            {
                return "حدثت مشكلة اثناء الحفظ";
            }

        }

        public string Delete(int id)
        {
            if (_unitOfWork.fileBus.Delete(id))
            {
                return "تم الحذف بنجاح";
            }
            else
            {
                return "حدثت مشكلة اثناء الحذف";
            }
        }

        public string Edit(VWFileBus fileBus)
        {
            var filBus = new FileBus
            {
                BusId = fileBus.BusId,
                BusCode = fileBus.BusCode,
                BusAll = fileBus.BusAll,
                BusDrive = fileBus.BusDrive,
                BusGo = fileBus.BusGo,
                BusNumber = fileBus.BusNumber,
                BusPlate = fileBus.BusPlate,
                BusReturn = fileBus.BusReturn,
                BusRoute = fileBus.BusRoute,
                BusState = fileBus.BusState,
                BusStud = fileBus.BusStud,
                BusSup = fileBus.BusSup,
                MobilPhone1 = fileBus.MobilPhone1,
                MobilPhone2 = fileBus.MobilPhone2,

            };
            if (_unitOfWork.fileBus.Update(filBus))
            {
                return "تم التعديل بنجاح";
            }
            else
            {
                return "حدثت مشكلة اثناء التعديل";
            }
        }

        public IEnumerable<FileBus> GetAll()
        {
            return _unitOfWork.fileBus.GetAll();
        }

        public FileBus GetbyId(int id)
        {
            return _unitOfWork.fileBus.GetById(id);
        }

        public async Task<FileBus> GetMaxFileBus()
        {
            return await _unitOfWork.fileBus.GetMax();
        }

        public  int GetMaxFileBusId()
        {
            return _unitOfWork.fileBus.GetMaxIdOfItem();
        }

        public async Task<FileBus> GetMinFileBus()
        {
            return await _unitOfWork.fileBus.GetMin();
        }

        public async Task<int> GetNewCode()
        {
            var code = await _unitOfWork.fileBus.GetNewCode();
            return code;
        }

        public async Task<FileBus> GetNextFileBus(int id)
        {
            return await _unitOfWork.fileBus.GetNextOrPreviousItemByCode(id, "next");
        }

        public async Task<FileBus> GetPreviousFileBus(int id)
        {
            return await _unitOfWork.fileBus.GetNextOrPreviousItemByCode(id, "previous");

        }
    }
}
