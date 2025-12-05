using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionVb02.Application.CqrsAndMediatr.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public bool IsFailure => !IsSuccess;
        public T Data { get; private set; }
        public string Message { get; private set; }
        public string ErrorDetail { get; private set; }
        public List<string> Errors { get; private set; }

        private Result() 
        {
            Errors = new List<string>();
        }

        public static Result<T> Success(T data, string message = "İşlem başarılı")
        {
            return new Result<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message,
                Errors = new List<string>()
            };
        }

        public static Result<T> Failure(string message, string errorDetail = null)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Message = message,
                ErrorDetail = errorDetail,
                Errors = new List<string> { message }
            };
        }

        public static Result<T> Failure(string message, List<string> errors)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }

        public static Result<T> ValidationFailure(List<string> validationErrors)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Message = "Doğrulama hataları oluştu",
                Errors = validationErrors ?? new List<string>()
            };
        }
    }

    public class Result
    {
        public bool IsSuccess { get; private set; }
        public bool IsFailure => !IsSuccess;
        public string Message { get; private set; }
        public string ErrorDetail { get; private set; }
        public List<string> Errors { get; private set; }

        private Result()
        {
            Errors = new List<string>();
        }

        public static Result Success(string message = "İşlem başarılı")
        {
            return new Result
            {
                IsSuccess = true,
                Message = message,
                Errors = new List<string>()
            };
        }

        public static Result Failure(string message, string errorDetail = null)
        {
            return new Result
            {
                IsSuccess = false,
                Message = message,
                ErrorDetail = errorDetail,
                Errors = new List<string> { message }
            };
        }

        public static Result Failure(string message, List<string> errors)
        {
            return new Result
            {
                IsSuccess = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }

        public static Result ValidationFailure(List<string> validationErrors)
        {
            return new Result
            {
                IsSuccess = false,
                Message = "Doğrulama hataları oluştu",
                Errors = validationErrors ?? new List<string>()
            };
        }
    }
}
