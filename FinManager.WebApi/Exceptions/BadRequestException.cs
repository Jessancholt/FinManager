using System.Net;

namespace FinManager.WebApi.Exceptions;

public sealed class BadRequestException : AppException
{
    public BadRequestException(string message)
        : base(message, HttpStatusCode.BadRequest)
    { }
}
