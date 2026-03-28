using System.Net;

namespace FinManager.WebApi.Exceptions;

public sealed class ValidationException : AppException
{
    public IDictionary<string, string[]> Errors { get; set; }

    public ValidationException(IDictionary<string, string[]> errors)
        : base("One or more validation errors occurred.", HttpStatusCode.BadRequest)
    {
        Errors = errors;
    }

    public ValidationException(string field, string error)
        : base("One or more validation errors occurred.", HttpStatusCode.BadRequest)
    {
        Errors = new Dictionary<string, string[]>
        {
            {field, [error]}
        };
    }
}
