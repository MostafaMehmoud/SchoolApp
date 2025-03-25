﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.ViewModels
{
    public class VWStudentCompleteFees
    {
        public int Id { get; set; }
        public int StudentNumber {  get; set; }
        public string StudentName { get; set; }
        public string StageName {  get; set; }
        public string ClassTypeName {  get; set; }
        public decimal Fees {  get; set; }
        public decimal Payments {  get; set; }
    }
}
