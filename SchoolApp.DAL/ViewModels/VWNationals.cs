using System.ComponentModel.DataAnnotations;

namespace SchoolApp.DAL.ViewModels
{
    public class VWNationals
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "اسم الجنسية مطلوب")]
        [MaxLength(100, ErrorMessage = "لا يمكن أن يتجاوز الاسم 100 حرف")]
        public string Name { get; set; }

        public int code { get; set; }
    }
}
