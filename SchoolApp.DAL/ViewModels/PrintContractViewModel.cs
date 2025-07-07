using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.DAL.Models;

namespace SchoolApp.DAL.ViewModels
{
    public class PrintViewModel<T> where T : class
    {
        public T model { get; set; }
        public Company Company { get; set; }
    }

}
