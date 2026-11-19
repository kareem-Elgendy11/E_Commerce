using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; } = false;
        public T? Value { get; set; }
        public string? Error { get; set; }
        public static ApiResponse<T> Success(T? value) =>new ApiResponse<T> { IsSuccess=true,Value = value };
        public static ApiResponse<T> Faild(string? Error) => new ApiResponse<T> { IsSuccess = false, Error=Error};

    }
}
