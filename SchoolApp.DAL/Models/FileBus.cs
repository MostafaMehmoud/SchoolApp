using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.Models
{
    public class FileBus
    {
        [Key]
        public int BusId { get; set; }
        public int BusCode { get; set; }
        [Required]
        [MaxLength(15)]
        public string BusPlate {  get; set; }
        [Required]
        public decimal BusGo {  get; set; }
        [Required]
        public decimal BusReturn {  get; set; }
        [Required]
        public decimal BusAll {  get; set; }
        [MaxLength(15)]
        public string? MobilPhone1 {  get; set; }
        [MaxLength(15)]
        public string? MobilPhone2 { get; set; }
        [MaxLength(30)]
        public string? BusDrive { get; set; }
        [MaxLength(15)]
        public string? BusRoute { get; set; }
        [MaxLength(30)]
        public string? BusSup { get; set; }
        public int BusState {  get; set; }
        public int? BusNumber { get; set; }
        public int? BusStud { get; set; }

    }
}
