using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.ViewModels
{
    public class VWClassTypeName
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="الاسم مطلوب")]
        public string Name { get; set; }
        [Required(ErrorMessage = "الكود مطلوب")]
        public int Code { get; set; }
        [Required(ErrorMessage = " رجا اختر المرحلة")]
        public int StageId { get; set; }
    }
}
