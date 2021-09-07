using System;

namespace BlackListService.Exceptions
{
    public class BaseException : Exception
    {
        public int StatusCode { get; set; }

        public BaseException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
