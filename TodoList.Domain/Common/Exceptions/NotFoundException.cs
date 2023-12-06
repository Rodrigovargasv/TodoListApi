using eSistemCurso.Domain.Common.Exceptions;
using System.Net;

namespace eSistemCurso.Domain.Common.Exceptions;


public class NotFoundException : CustomException
{
    public NotFoundException(string message)
        : base(message, null, HttpStatusCode.NotFound)
    {
    }
}
