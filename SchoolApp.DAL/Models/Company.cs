using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم الشركة مطلوب")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "الرقم الضريبي مطلوب")]
        [StringLength(20, ErrorMessage = "الرقم الضريبي لا يمكن أن يتجاوز 20 رقم")]
        public string TaxNumber { get; set; }

        [Required(ErrorMessage = "نسبة الضريبة مطلوبة")]
        [Range(0, 100, ErrorMessage = "النسبة يجب أن تكون بين 0 و 100")]
        public decimal TaxRate { get; set; }

        public byte[] Logo { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required(ErrorMessage = "رقم المبنى مطلوب")]
        public string BulidingAddressNumber { get; set; }
        [Required(ErrorMessage = "رقم المبنى الاضافي مطلوب")]
        public string BulidingAddressAddtionalNumber { get; set; }
        [Required(ErrorMessage = "رقم الحي مطلوب")]
        public string NeighborhoodAddress { get; set; }
        [Required(ErrorMessage = "رقم الشارع مطلوب")]
        public string StreetAddress { get; set; }
        [Required(ErrorMessage = "رقم المدينة مطلوب")]
        public string CityAddress { get; set; }
        [Required(ErrorMessage = "رقم المنطقة مطلوب")]
        public string AreaAddress { get; set; }
        [Required(ErrorMessage = "السجل التجاري مطلوب")]

        public string CommercialRegister { get; set; }

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [Phone(ErrorMessage = "رقم الهاتف غير صالح")]
        public string CompanyPhoneNumber { get; set; }

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صالح")]
        public string CompanyEmail { get; set; }

        [Required(ErrorMessage = "تاريخ التأسيس مطلوب")]
        public DateTime EstablishedDate { get; set; }
    }
}
