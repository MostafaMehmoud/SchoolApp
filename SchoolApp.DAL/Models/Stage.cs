using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.Models
{
    public class Stage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string StageName { get; set; }
        [Required]
        public int StageCode {  get; set; }
    }
}
