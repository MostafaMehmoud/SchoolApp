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
    public class ServicePayment : IServicePayment
    {
        private readonly IUnitOfWork _unitOfWork;
        public ServicePayment(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string Add(VWPayment Payment)
        {
            using (var transaction = _unitOfWork.BeginTransaction()) // ✅ بدء المعاملة
            {
                try
                {
                    var payment = new Payment
            {
               Id=Payment.Id,
               Code=Payment.Code,
               PaymentDate=Payment.PaymentDate,
                StudentId=Payment.StudentId,
                StudentName = Payment.StudentName,
                Amount = Payment.Amount,
                CashCheque= Payment.CashCheque,
                ChequeNumber = Payment.ChequeNumber     ,
                ChequeDate = Payment.ChequeDate ,
                Purpose= Payment.Purpose    ,
                BankName = Payment.BankName,
                AmountName= Payment.AmountName

            }; var student = _unitOfWork.students.GetById(Payment.StudentId);
                    student.ReceiptTotalFees = student.ReceiptTotalFees + Payment.Amount;
                   
                    student.RemainingFees = student.ReceiptTotalFees - student.ReceiptTotalPayments;

                    if (_unitOfWork.payments.Add(payment) && _unitOfWork.students.Update(student))
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
            using (var transaction = _unitOfWork.BeginTransaction()) // ✅ بدء المعاملة
            {
                try
                {
                    var receipt = _unitOfWork.payments.GetById(id);
                    if (receipt == null)
                    {
                        return "الإيصال غير موجود!";
                    }

                    var student = _unitOfWork.students.GetById(receipt.StudentId);
                    if (student == null)
                    {
                        return "الطالب غير موجود!";
                    }

                    // تحديث بيانات الطالب
                    student.ReceiptTotalFees-= receipt.Amount;
                    
                    student.RemainingFees = student.ReceiptTotalFees - student.ReceiptTotalPayments;

                    bool isDeleted = _unitOfWork.payments.Delete(id);
                    bool isUpdated = _unitOfWork.students.Update(student);

                    if (isDeleted && isUpdated)
                    {
                        _unitOfWork.Save(); // ✅ حفظ البيانات
                        transaction.Commit(); // ✅ تأكيد المعاملة
                        return "تم الحذف بنجاح";
                    }
                    else
                    {
                        transaction.Rollback(); // ❌ إلغاء العملية
                        return "حدثت مشكلة أثناء الحذف";
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // ❌ التراجع عن جميع العمليات في حالة حدوث خطأ
                                            // يمكن استخدام Logger هنا لتسجيل الخطأ
                    Console.WriteLine($"خطأ أثناء الحذف: {ex.Message}");
                    return "فشل الحذف: " + ex.Message;
                }
            }
        }

        public string Edit(VWPayment Payment)
        {
            var payment = new Payment
            {
                Id = Payment.Id,
                Code = Payment.Code,
                PaymentDate = Payment.PaymentDate,
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
                PaymentDate = payment.PaymentDate,
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
                PaymentDate = payment.PaymentDate,
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
            if (payment == null)
            {
                return null;
            }
            var vWPayment = new VWPayment
            {
                Id = payment.Id,
                Code = payment.Code,
                PaymentDate = payment.PaymentDate,
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
            if (payment == null)
            {
                return null;
            }
            var vWPayment = new VWPayment
            {
                Id = payment.Id,
                Code = payment.Code,
                PaymentDate = payment.PaymentDate,
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

        public VWStudentCostReceipt GetStudent(int StudentId)
        {
            var student = _unitOfWork.students.GetAll().FirstOrDefault(i => i.Id == StudentId);
            VWStudentCostReceipt receipt = new VWStudentCostReceipt();
           
            receipt.StudentName = $"{student.StudentName} {student.FatherName} {student.GrandFatherName} {student.FamilyName}";
            return receipt;

        }

        public VWPayment Print(int id)
        {
           var payment=_unitOfWork.payments.GetById(id);
            VWPayment vWPayment = new VWPayment();
            vWPayment.Id = id;
            vWPayment.Code = payment.Code;
            vWPayment.StudentId = payment.StudentId;
            vWPayment.PaymentDate = payment.PaymentDate;
            vWPayment.Amount= payment.Amount;   
            vWPayment.ChequeNumber= payment.ChequeNumber;
            vWPayment.ChequeDate=payment.ChequeDate;
            vWPayment.AmountName = payment.AmountName;
            vWPayment.Amount=payment.Amount;
            vWPayment.Purpose = payment.Purpose;
            vWPayment.StudentId= payment.StudentId;
            vWPayment.BankName = payment.BankName;
            var student = _unitOfWork.students.GetAll().FirstOrDefault(i => i.Id ==payment.StudentId);
            vWPayment.StudentName= $"{student.StudentName} {student.FatherName} {student.GrandFatherName} {student.FamilyName}";
            return vWPayment;   
        }
    }
}
