﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.ViewModels
{
    public class ForgotPasswordByUsernameViewModel
    {
        [Required(ErrorMessage = "يرجى إدخال اسم المستخدم")]
        public string Username { get; set; }
    }
}
