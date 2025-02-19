using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;

namespace SchoolApp.DAL.ViewModels
{
    public class VWClassType
    {
        public VWClassType()
        {
            amounts = new List<VWAmoumt>();
        }
        public int Id { get; set; }
        public int Code { get; set; }

        public int StageId { get; set; }
        public DateTime CurrentDateClassType { get; set; }
        public string StageName { get; set; }
        public decimal CLSAcpt { get; set; }
        public decimal CLSRegs { get; set; }
        public decimal CLSCloth { get; set; }
        public List<VWAmoumt> amounts { get; set; }   
    }
    public class VWAmoumt
    {
        public int Id { get; set; }
        public int ClassTypeNameId { get; set; }
        public int ClassTypeId { get; set; }
        public string ClassTypeName { get; set; }   
        public decimal AmountPrice { get; set; }
        public DateTime AmountDate { get; set; }
    }
}
