﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.DAL.ViewModels
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public ApiResponse(bool success, T data, string message = null, List<string> errors = null)
        {
            Success = success;
            Data = data;
            Message = message;
            Errors = errors ?? new List<string>();
        }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Operation successful")
        {
            return new ApiResponse<T>(true, data, message);
        }

        public static ApiResponse<T> ErrorResponse(List<string> errors, string message = "Operation failed")
        {
            return new ApiResponse<T>(false, default, message, errors);
        }
    }

}
