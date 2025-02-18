using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.ViewModels
{
    public class VWStage
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "اسم المرحلة الدراسية مطلوب")]
        [MaxLength(100, ErrorMessage = "لا يمكن أن يتجاوز الاسم 100 حرف")]
        public string Name { get; set; }

        public int code { get; set; }
    }
}
