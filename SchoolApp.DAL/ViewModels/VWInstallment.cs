﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.ViewModels
{
    public class VWInstallment
    {
       
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string InstallName { get; set; }
        public int StageId { get; set; }
        public int Code { get; set; }
        public int ClassTypeId {  get; set; }
    }
}
