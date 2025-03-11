using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.ViewModels
{
    public class ReportClassType
    {
        public int Id {  get; set; }
        public int stageId {  get; set; }
        public int ClassTypeId {  get; set; }
        public int ClassTypeNameId {  get; set; }
        public string ClassTypeName { get; set; }
        public string StageName {  get; set; }
        public decimal PriceClassType {  get; set; }
        public int NumberOfInstallments { get; set; } = 1;
        public decimal PriceInstallment { get; set; } = 0;
        public decimal CLSAcpt { get; set; } = 0;
        public decimal CLSRegs { get; set; } = 0;
        public decimal CLSCloth { get; set; } = 0;
        public decimal CLSBakelite { get; set; } = 0;
        public decimal TotalAmountClassType { get; set; } = 0;
    }
}
