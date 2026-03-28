using System.Net;

namespace FinManager.WebApi.Exceptions;

public abstract class AppException : Exception
{
    public HttpStatusCode StatusCode { get; set; }

    protected AppException(string message, HttpStatusCode statusCode) 
        : base(message)
    {
        StatusCode = statusCode;
    }
}
