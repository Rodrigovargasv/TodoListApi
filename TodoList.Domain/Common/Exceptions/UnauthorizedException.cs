using eSistemCurso.Domain.Common.Exceptions;
using System.Net;

namespace eSistemCurso.Domain.Common.Exceptions;


public class UnauthorizedException : CustomException
{
    public UnauthorizedException(string message)
       : base(message, null, HttpStatusCode.Unauthorized)
    {
    }
}
