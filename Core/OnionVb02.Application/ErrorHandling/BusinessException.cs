using System;
using System.Collections.Generic;

namespace OnionVb02.Application.ErrorHandling
{
    public class BusinessException : Exception
    {
        public string ErrorCode { get; }
        public List<string> Errors { get; }

        public BusinessException(string message) : base(message)
        {
            Errors = new List<string> { message };
        }

        public BusinessException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
            Errors = new List<string> { message };
        }

        public BusinessException(string message, List<string> errors) : base(message)
        {
            Errors = errors ?? new List<string>();
        }

        public BusinessException(string message, Exception innerException) : base(message, innerException)
        {
            Errors = new List<string> { message };
        }
    }

    public class NotFoundException : BusinessException
    {
        public NotFoundException(string entityName, int id)
            : base($"{entityName} bulunamadı. ID: {id}", "NOT_FOUND")
        {
        }

        public NotFoundException(string message) : base(message, "NOT_FOUND")
        {
        }
    }

    public class ValidationException : BusinessException
    {
        public ValidationException(string message) : base(message, "VALIDATION_ERROR")
        {
        }

        public ValidationException(List<string> errors)
            : base("Doğrulama hataları oluştu", errors)
        {
        }
    }

    public class UnauthorizedException : BusinessException
    {
        public UnauthorizedException(string message = "Bu işlem için yetkiniz bulunmamaktadır")
            : base(message, "UNAUTHORIZED")
        {
        }
    }
}
