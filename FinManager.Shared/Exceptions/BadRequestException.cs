using System.Net;

namespace FinManager.Shared.Exceptions;

public sealed class BadRequestException : AppException
{
    public BadRequestException(string message)
        : base(message, HttpStatusCode.BadRequest)
    { }
}
