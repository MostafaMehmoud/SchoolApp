﻿using System;
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
    public class ServicePayment : IServicePayment
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServicePayment(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string Add(VWPayment Payment)
        {
            var payment = new Payment
            {
               Id=Payment.Id,
               Code=Payment.Code,
               ReceiptDate=Payment.ReceiptDate,
                StudentId=Payment.StudentId,
                StudentName = Payment.StudentName,
                Amount = Payment.Amount,
                CashCheque= Payment.CashCheque,
                ChequeNumber = Payment.ChequeNumber     ,
                ChequeDate = Payment.ChequeDate ,
                Purpose= Payment.Purpose    ,
                BankName = Payment.BankName,
                AmountName= Payment.AmountName

            };
            if (_unitOfWork.payments.Add(payment))
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
            throw new NotImplementedException();
        }

        public string Edit(VWPayment Payment)
        {
            var payment = new Payment
            {
                Id = Payment.Id,
                Code = Payment.Code,
                ReceiptDate = Payment.ReceiptDate,
                StudentId = Payment.StudentId,
                StudentName = Payment.StudentName,
                Amount = Payment.Amount,
                CashCheque = Payment.CashCheque,
                ChequeNumber = Payment.ChequeNumber,
                ChequeDate = Payment.ChequeDate,
                Purpose = Payment.Purpose,
                BankName = Payment.BankName,
                AmountName = Payment.AmountName

            };
            if (_unitOfWork.payments.Update(payment))
            
                {
                    return "تم التعديل بنجاح";
                }
            else
                {
                    return "حدثت مشكلة اثناء التعديل";
                }
            
        }

        public IEnumerable<Payment> GetAll()
        {
            return _unitOfWork.payments.GetAll();
        }

        public Payment GetbyId(int id)
        {
            return _unitOfWork.payments.GetById(id);
        }

        public async Task<VWPayment> GetMaxPayment()
        {
            var payment=await _unitOfWork.payments.GetMax();
         
            var vWPayment = new VWPayment
            {
                Id = payment.Id,
                Code = payment.Code,
                ReceiptDate = payment.ReceiptDate,
                StudentId = payment.StudentId,
                StudentName = payment.StudentName,
                Amount = payment.Amount,
                CashCheque = payment.CashCheque,
                ChequeNumber = payment.ChequeNumber,
                ChequeDate = payment.ChequeDate,
                Purpose = payment.Purpose,
                BankName = payment.BankName,
                AmountName = payment.AmountName

            };
            return vWPayment;   

        }

        public int GetMaxPaymentId()
        {
            return _unitOfWork.payments.GetMaxIdOfItem();
        }

        public async Task<VWPayment> GetMinPayment()
        {
            var payment = await _unitOfWork.payments.GetMin();
            var vWPayment = new VWPayment
            {
                Id = payment.Id,
                Code = payment.Code,
                ReceiptDate = payment.ReceiptDate,
                StudentId = payment.StudentId,
                StudentName = payment.StudentName,
                Amount = payment.Amount,
                CashCheque = payment.CashCheque,
                ChequeNumber = payment.ChequeNumber,
                ChequeDate = payment.ChequeDate,
                Purpose = payment.Purpose,
                BankName = payment.BankName,
                AmountName = payment.AmountName

            };
            return vWPayment;
        }
        public async Task<int> GetNewCode()
        {
            var code = await _unitOfWork.payments.GetNewCode();
            return code;
        }

        public async Task<VWPayment> GetNextPayment(int id)
        {
            var payment =await _unitOfWork.payments.GetNextOrPreviousItemByCode(id, "next");
            var vWPayment = new VWPayment
            {
                Id = payment.Id,
                Code = payment.Code,
                ReceiptDate = payment.ReceiptDate,
                StudentId = payment.StudentId,
                StudentName = payment.StudentName,
                Amount = payment.Amount,
                CashCheque = payment.CashCheque,
                ChequeNumber = payment.ChequeNumber,
                ChequeDate = payment.ChequeDate,
                Purpose = payment.Purpose,
                BankName = payment.BankName,
                AmountName = payment.AmountName

            };
            return vWPayment;
        }

        public async Task<VWPayment> GetPreviousPayment(int id)
        {
            var payment =await _unitOfWork.payments.GetNextOrPreviousItemByCode(id, "previous");
            var vWPayment = new VWPayment
            {
                Id = payment.Id,
                Code = payment.Code,
                ReceiptDate = payment.ReceiptDate,
                StudentId = payment.StudentId,
                StudentName = payment.StudentName,
                Amount = payment.Amount,
                CashCheque = payment.CashCheque,
                ChequeNumber = payment.ChequeNumber,
                ChequeDate = payment.ChequeDate,
                Purpose = payment.Purpose,
                BankName = payment.BankName,
                AmountName = payment.AmountName

            };
            return vWPayment;
        }
    }
}
