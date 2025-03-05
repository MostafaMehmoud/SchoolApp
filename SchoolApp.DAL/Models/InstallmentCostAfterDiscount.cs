using System.ComponentModel.DataAnnotations;

namespace SchoolApp.DAL.Models
{
    public class InstallmentCostAfterDiscount
    {
        [Key]
        public int Id { get; set; }
        public int StageId { get; set; }
        public int ClassTypeId { get; set; }
        public int InstallmentId { get; set; }
        public decimal CostInstallment { get; set; }
        public int StudentId {  get; set; }
        public string InstallmentName { get; set; }

    }
}
