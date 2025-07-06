using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SchoolApp.DAL.ViewModels
{
    public class VWCompany
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string TaxNumber { get; set; }
        public decimal TaxRate { get; set; }
        public IFormFile Logo { get; set; } // Assuming Logo is stored as a byte array
        public string Description { get; set; }
        public string BulidingAddressNumber { get; set; }
        public string BulidingAddressAddtionalNumber { get; set; }
        public string NeighborhoodAddress { get; set; }
        public string StreetAddress { get; set; }
        public string CityAddress { get; set; }
        public string AreaAddress { get; set; }
        public string CommercialRegister { get; set; }

        public string CompanyPhoneNumber { get; set; }
        public string CompanyEmail { get; set; }
        public DateTime EstablishedDate { get; set; }
    }
}
