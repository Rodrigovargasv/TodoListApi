
using System.Net;

namespace eSistemCurso.Domain.Common.Exceptions;

public class BadRequestException : CustomException
{
    public BadRequestException(string message, List<string> errors = default)
        : base(message, errors, HttpStatusCode.BadRequest)
    {
    }
    public BadRequestException(string message, params string[] errors)
        : base(message, errors.ToList(), HttpStatusCode.BadRequest)
    {
    }
}
