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
    public class ServiceReceipt : IServiceReceipt
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServiceReceipt(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string Add(VWReceipt Receipt)
        {
            using (var transaction = _unitOfWork.BeginTransaction()) // ✅ بدء المعاملة
            {
                try
                {
                    Receipt receipt = new Receipt();
                    receipt.Code = Receipt.Code;
                    receipt.StudentId = Receipt.StudentId;
                    receipt.ReceiptDate = Receipt.ReceiptDate;
                    receipt.Amount = Receipt.Amount;
                    receipt.CLSCloth = Receipt.CLSCloth;
                    receipt.CLSAcpt = Receipt.CLSAcpt;
                    receipt.CLSBakelite = Receipt.CLSBakelite;
                    receipt.CLSRegs = Receipt.CLSRegs;
                    receipt.CashCheque = Receipt.CashCheque;
                    receipt.ChequeNumber = Receipt.ChequeNumber;
                    receipt.ChequeDate = Receipt.ChequeDate;
                    receipt.Purpose = Receipt.Purpose;
                    receipt.ReceiptBusFirstTremCost = Receipt.ReceiptBusFirstTremCost;
                    receipt.ReceiptBusSecondTremCost = Receipt.ReceiptBusSecondTremCost;
                    receipt.installmentReceipts = Receipt.installmentReceipts;
                    receipt.StudentName= Receipt.StudentName;
                    receipt.BankName= Receipt.BankName;
                    receipt.AmountName=Receipt.AmountName;
                    var student = _unitOfWork.students.GetById(receipt.StudentId);
                    student.ReceiptTotalFees =student.ReceiptTotalFees- Receipt.ReceiptTotalFees;
                    student.ReceiptTotalPayments =student.ReceiptTotalPayments+ Receipt.Amount;
                    student.RemainingFees = student.ReceiptTotalFees - student.ReceiptTotalPayments;    
                    
                    if (_unitOfWork.receipts.Add(receipt)&&_unitOfWork.students.Update(student))
                    {
                        _unitOfWork.Save(); // ✅ حفظ البيانات
                        transaction.Commit(); // ✅ تأكيد المعاملة
                        return "تم الحفظ بنجاح";
                    }
                    else
                    {
                        transaction.Rollback(); // ❌ إلغاء العملية في حالة الفشل
                        return "حدثت مشكلة أثناء الحفظ";
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // ❌ التراجع عن جميع العمليات في حالة حدوث خطأ
                    return "فشل الحفظ: " + ex.Message;
                }
            }
        }
        public string Delete(int id)
        {

            if (_unitOfWork.receipts.Delete(id))
            {
                return "تم الحذف بنجاح";
            }
            else
            {
                return "حدثت مشكلة اثناء الحذف";
            }
        }

        public string Edit(VWReceipt Receipt)
        {
            using (var transaction = _unitOfWork.BeginTransaction()) // ✅ بدء المعاملة
            {
                try
                {
                    Receipt receipt = new Receipt();
                    receipt.Id = Receipt.Id;
                    receipt.Code = Receipt.Code;
                    receipt.StudentId = Receipt.StudentId;
                    receipt.ReceiptDate = Receipt.ReceiptDate;
                    receipt.Amount = Receipt.Amount;
                    receipt.CLSCloth = Receipt.CLSCloth;
                    receipt.CLSAcpt = Receipt.CLSAcpt;
                    receipt.CLSBakelite = Receipt.CLSBakelite;
                    receipt.CLSRegs = Receipt.CLSRegs;
                    receipt.CashCheque = Receipt.CashCheque;
                    receipt.ChequeNumber = Receipt.ChequeNumber;
                    receipt.ChequeDate = Receipt.ChequeDate;
                    receipt.Purpose = Receipt.Purpose;
                    receipt.ReceiptBusFirstTremCost = Receipt.ReceiptBusFirstTremCost;
                    receipt.ReceiptBusSecondTremCost = Receipt.ReceiptBusSecondTremCost;
                    receipt.installmentReceipts = Receipt.installmentReceipts;
                    receipt.StudentName = Receipt.StudentName;
                    receipt.BankName = Receipt.BankName;
                    var student = _unitOfWork.students.GetById(receipt.StudentId);
                    student.ReceiptTotalFees = Receipt.ReceiptTotalFees;
                    student.ReceiptTotalPayments = Receipt.ReceiptTotalPayments;
                    student.RemainingFees = Receipt.RemainingFees;

                    if (_unitOfWork.receipts.Add(receipt) && _unitOfWork.students.Update(student))
                    {
                        _unitOfWork.Save(); // ✅ حفظ البيانات
                        transaction.Commit(); // ✅ تأكيد المعاملة
                        return "تم التعديل بنجاح";
                    }
                    else
                    {
                        transaction.Rollback(); // ❌ إلغاء العملية في حالة الفشل
                        return "حدثت مشكلة أثناء التعديل";
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // ❌ التراجع عن جميع العمليات في حالة حدوث خطأ
                    return "فشل الحفظ: " + ex.Message;
                }
            }
        }

        public IEnumerable<Receipt> GetAll()
        {
            return _unitOfWork.receipts.GetAll();
        }

        public Receipt GetbyId(int id)
        {
            return _unitOfWork.receipts.GetById(id);
        }

        public async Task<VWReceipt> GetMaxReceipt()
        {
            var receipt = await _unitOfWork.receipts.GetMax();
            var student = _unitOfWork.students.GetById(receipt.StudentId);
            VWReceipt vWReceipt = new VWReceipt();
            vWReceipt.Id = receipt.Id;
            vWReceipt.Code = receipt.Code;
            vWReceipt.StudentId = receipt.StudentId;
            vWReceipt.ReceiptDate = receipt.ReceiptDate;
            vWReceipt.Amount = receipt.Amount;
            vWReceipt.CLSCloth = receipt.CLSCloth;
            vWReceipt.CLSAcpt = receipt.CLSAcpt;
            vWReceipt.CLSBakelite = receipt.CLSBakelite;
            vWReceipt.CLSRegs = receipt.CLSRegs;
            vWReceipt.CashCheque = receipt.CashCheque;
            vWReceipt.ChequeNumber = receipt.ChequeNumber;
            vWReceipt.ChequeDate = receipt.ChequeDate;
            vWReceipt.Purpose = receipt.Purpose;
            vWReceipt.ReceiptBusFirstTremCost = receipt.ReceiptBusFirstTremCost;
            vWReceipt.ReceiptBusSecondTremCost = receipt.ReceiptBusSecondTremCost;
            vWReceipt.installmentReceipts = receipt.installmentReceipts;
            vWReceipt.ReceiptTotalFees = student.ReceiptTotalFees;
            vWReceipt.ReceiptTotalPayments = student.ReceiptTotalPayments;
            vWReceipt.RemainingFees = student.RemainingFees;
            vWReceipt.LastBalance = student.LastBalance;
            vWReceipt.StudentName = receipt.StudentName;
            vWReceipt.BankName=receipt.BankName;
            vWReceipt.AmountName=receipt.AmountName;
            vWReceipt.Amount = receipt.Amount;

            return vWReceipt;
        }

        public int GetMaxReceiptId()
        {
            return _unitOfWork.receipts.GetMaxIdOfItem();
        }

        public async Task<VWReceipt> GetMinReceipt()
        {
            var receipt = await _unitOfWork.receipts.GetMin();
            var student = _unitOfWork.students.GetById(receipt.StudentId);
            VWReceipt vWReceipt = new VWReceipt();
            vWReceipt.Id = receipt.Id;
            vWReceipt.Code = receipt.Code;
            vWReceipt.StudentId = receipt.StudentId;
            vWReceipt.ReceiptDate = receipt.ReceiptDate;
            vWReceipt.Amount = receipt.Amount;
            vWReceipt.CLSCloth = receipt.CLSCloth;
            vWReceipt.CLSAcpt = receipt.CLSAcpt;
            vWReceipt.CLSBakelite = receipt.CLSBakelite;
            vWReceipt.CLSRegs = receipt.CLSRegs;
            vWReceipt.CashCheque = receipt.CashCheque;
            vWReceipt.ChequeNumber = receipt.ChequeNumber;
            vWReceipt.ChequeDate = receipt.ChequeDate;
            vWReceipt.Purpose = receipt.Purpose;
            vWReceipt.ReceiptBusFirstTremCost = receipt.ReceiptBusFirstTremCost;
            vWReceipt.ReceiptBusSecondTremCost = receipt.ReceiptBusSecondTremCost;
            vWReceipt.installmentReceipts = receipt.installmentReceipts;
            vWReceipt.ReceiptTotalFees = student.ReceiptTotalFees;
            vWReceipt.ReceiptTotalPayments = student.ReceiptTotalPayments;
            vWReceipt.RemainingFees = student.RemainingFees;
            vWReceipt.LastBalance = student.LastBalance;
            vWReceipt.StudentName = receipt.StudentName;
            vWReceipt.BankName = receipt.BankName;
            vWReceipt.AmountName = receipt.AmountName;
            vWReceipt.Amount = receipt.Amount;
            return vWReceipt;
        }

        public async Task<int> GetNewCode()
        {
            var code = await _unitOfWork.receipts.GetNewCode();
            return code;
        }

        public async Task<VWReceipt> GetNextReceipt(int id)
        {
            var receipt = await _unitOfWork.receipts.GetNextOrPreviousItemByCode(id, "next");
            if (receipt == null)
            {
                return null;
            }
            var student = _unitOfWork.students.GetById(receipt.StudentId);
            VWReceipt vWReceipt = new VWReceipt();
            vWReceipt.Id = receipt.Id;
            vWReceipt.Code = receipt.Code;
            vWReceipt.StudentId = receipt.StudentId;
            vWReceipt.ReceiptDate = receipt.ReceiptDate;
            vWReceipt.Amount = receipt.Amount;
            vWReceipt.CLSCloth = receipt.CLSCloth;
            vWReceipt.CLSAcpt = receipt.CLSAcpt;
            vWReceipt.CLSBakelite = receipt.CLSBakelite;
            vWReceipt.CLSRegs = receipt.CLSRegs;
            vWReceipt.CashCheque = receipt.CashCheque;
            vWReceipt.ChequeNumber = receipt.ChequeNumber;
            vWReceipt.ChequeDate = receipt.ChequeDate;
            vWReceipt.Purpose = receipt.Purpose;
            vWReceipt.ReceiptBusFirstTremCost = receipt.ReceiptBusFirstTremCost;
            vWReceipt.ReceiptBusSecondTremCost = receipt.ReceiptBusSecondTremCost;
            vWReceipt.installmentReceipts = receipt.installmentReceipts;
            vWReceipt.ReceiptTotalFees = student.ReceiptTotalFees;
            vWReceipt.ReceiptTotalPayments = student.ReceiptTotalPayments;
            vWReceipt.RemainingFees = student.RemainingFees;
            vWReceipt.LastBalance = student.LastBalance;
            vWReceipt.StudentName = receipt.StudentName;
            vWReceipt.BankName = receipt.BankName;
            vWReceipt.AmountName = receipt.AmountName;
            vWReceipt.Amount = receipt.Amount;
            return vWReceipt;
        }

        public async Task<VWReceipt> GetPreviousReceipt(int id)
        {
            var receipt = await _unitOfWork.receipts.GetNextOrPreviousItemByCode(id, "previous"); 
            if (receipt == null)
            {
                return null;
            }
            var student = _unitOfWork.students.GetById(receipt.StudentId);
            VWReceipt vWReceipt = new VWReceipt();
            vWReceipt.Id = receipt.Id;
            vWReceipt.Code = receipt.Code;
            vWReceipt.StudentId = receipt.StudentId;
            vWReceipt.ReceiptDate = receipt.ReceiptDate;
            vWReceipt.Amount = receipt.Amount;
            vWReceipt.CLSCloth = receipt.CLSCloth;
            vWReceipt.CLSAcpt = receipt.CLSAcpt;
            vWReceipt.CLSBakelite = receipt.CLSBakelite;
            vWReceipt.CLSRegs = receipt.CLSRegs;
            vWReceipt.CashCheque = receipt.CashCheque;
            vWReceipt.ChequeNumber = receipt.ChequeNumber;
            vWReceipt.ChequeDate = receipt.ChequeDate;
            vWReceipt.Purpose = receipt.Purpose;
            vWReceipt.ReceiptBusFirstTremCost = receipt.ReceiptBusFirstTremCost;
            vWReceipt.ReceiptBusSecondTremCost = receipt.ReceiptBusSecondTremCost;
            vWReceipt.installmentReceipts = receipt.installmentReceipts;
            vWReceipt.ReceiptTotalFees = student.ReceiptTotalFees;
            vWReceipt.ReceiptTotalPayments = student.ReceiptTotalPayments;
            vWReceipt.RemainingFees = student.RemainingFees;
            vWReceipt.LastBalance = student.LastBalance;
            vWReceipt.StudentName = receipt.StudentName;
            vWReceipt.BankName = receipt.BankName;
            vWReceipt.AmountName = receipt.AmountName;
            vWReceipt.Amount=receipt.Amount;
            return vWReceipt;
        }
    }
}
