using System.ComponentModel.DataAnnotations;

namespace SchoolApp.DAL.ViewModels
{
    public class VWFileBus
    {
        public int BusId { get; set; }
        public int BusCode { get; set; }
        [Required(ErrorMessage = "ادخل رقم اللوحة")]
        [MaxLength(15, ErrorMessage = "الحد الاقصي لرقم اللوحة 15 رقم")]
        public string BusPlate { get; set; }
        [Required(ErrorMessage = "ادخل سعر الذهب")]
        public decimal BusGo { get; set; }
        [Required(ErrorMessage = "ادخل سعر العودة")]
        public decimal BusReturn { get; set; }
        [Required(ErrorMessage = "ادخل سعر الذهب و العودة")]
        public decimal BusAll { get; set; }
        [MaxLength(15, ErrorMessage = "الحد الاقصي لرقم الجوال هو 15 رقم")]
        public string? MobilPhone1 { get; set; }
        [MaxLength(15, ErrorMessage = "الحد الاقصي لرقم الجوال هو 15 رقم")]
        public string? MobilPhone2 { get; set; }
        [MaxLength(30, ErrorMessage = "الحد الاقصي لاسم السائق هو 30 حرف")]
        public string? BusDrive { get; set; }
        [MaxLength(15, ErrorMessage = "الحد الاقصي لاسم  المشرف هو 15 حرف")]
        public string? BusRoute { get; set; }
        [MaxLength(30, ErrorMessage = "الحد الاقصي لاسم خط الاتجاه هو 30 حرف")]
        public string? BusSup { get; set; }
        public int BusState { get; set; }
        public int? BusNumber { get; set; }
        public int? BusStud { get; set; }
    }
}
