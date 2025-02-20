using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.ViewModels
{
    public class VWDepartment
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="اسم القسم مطلوب")]
        public string Name { get; set; }
        public int Code { get; set; }
    }
}
