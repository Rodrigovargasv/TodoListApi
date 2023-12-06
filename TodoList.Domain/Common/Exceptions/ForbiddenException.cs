using eSistemCurso.Domain.Common.Exceptions;
using System.Net;

namespace eSistemCurso.Domain.Common.Exceptions;


public class ForbiddenException : CustomException
{
    public ForbiddenException(string message)
        : base(message, null, HttpStatusCode.Forbidden)
    {
    }
}
