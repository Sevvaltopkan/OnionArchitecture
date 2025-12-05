using System;
using System.Collections.Generic;

namespace OnionVb02.Application.ErrorHandling
{
    public class ErrorResult
    {
        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDetail { get; set; }
        public List<string> Errors { get; set; }
        public Exception Exception { get; set; }

        public ErrorResult()
        {
            Errors = new List<string>();
        }

        public static ErrorResult Success(string message = "İşlem başarılı")
        {
            return new ErrorResult
            {
                IsSuccess = true,
                Message = message,
                Errors = new List<string>()
            };
        }

        public static ErrorResult Failure(string message, string errorCode = null, string errorDetail = null)
        {
            return new ErrorResult
            {
                IsSuccess = false,
                Message = message,
                ErrorCode = errorCode,
                ErrorDetail = errorDetail,
                Errors = new List<string> { message }
            };
        }

        public static ErrorResult FromException(Exception ex, string message = null)
        {
            return new ErrorResult
            {
                IsSuccess = false,
                Message = message ?? "Beklenmeyen bir hata oluştu",
                ErrorDetail = ex.Message,
                Exception = ex,
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public class ErrorResult<T>
    {
        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorDetail { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }

        public ErrorResult()
        {
            Errors = new List<string>();
        }

        public static ErrorResult<T> Success(T data, string message = "İşlem başarılı")
        {
            return new ErrorResult<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data,
                Errors = new List<string>()
            };
        }

        public static ErrorResult<T> Failure(string message, string errorCode = null, string errorDetail = null)
        {
            return new ErrorResult<T>
            {
                IsSuccess = false,
                Message = message,
                ErrorCode = errorCode,
                ErrorDetail = errorDetail,
                Data = default,
                Errors = new List<string> { message }
            };
        }
    }
}
