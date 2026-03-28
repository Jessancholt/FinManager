using System.Net;

namespace FinManager.WebApi.Exceptions;

public sealed class NotFoundException : AppException
{
    public NotFoundException(string resourceName, object key) 
        : base($"{resourceName} with identifier '{key}' was not found.", HttpStatusCode.NotFound)
    { }

    public NotFoundException(string resourceName)
        : base($"{resourceName} not found.", HttpStatusCode.NotFound)
    { }
}