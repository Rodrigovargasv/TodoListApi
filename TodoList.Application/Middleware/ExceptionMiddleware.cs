using eSistemCurso.Domain.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using TodoList.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using Serilog;
using System.Text.Json;

namespace TodoList.Application.ExceptionMiddleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {

                var errorId = Guid.NewGuid().ToString();

                ErrorResult errorResult = new()
                {
                    Source = exception.TargetSite?.DeclaringType?.FullName,
                    Exception = exception.Message.Trim(),
                    ErrorId = errorId,
                    SupportMessage = $"Forneça o ErrorId {errorId} à equipe de suporte para uma análise mais aprofundada."
                };

                if (exception is not CustomException && exception.InnerException != null)
                {
                    while (exception.InnerException != null)
                    {
                        exception = exception.InnerException;
                    }
                }
                if (exception is FluentValidation.ValidationException fluentException)
                {
                    errorResult.Exception = "Erro na requisição. Uma ou mais validações falharam.";
                    foreach (var error in fluentException.Errors)
                    {
                        errorResult.Messages.Add($"[{error.PropertyName}] {error.ErrorMessage}");
                    }
                }

                switch (exception)
                {
                    case CustomException e:
                        errorResult.StatusCode = (int)e.StatusCode;
                        if (e.ErrorMessages is not null)
                        {
                            errorResult.Messages = e.ErrorMessages;
                        }

                        break;
                    case KeyNotFoundException:
                        errorResult.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case ValidationException:
                        errorResult.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case FluentValidation.ValidationException:
                        errorResult.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        break;
                    default:
                        errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                Log.Error($"{errorResult.Exception} A solicitação falhou com o código de status {errorResult.StatusCode} e o  Error Id {errorId}.");
                HttpResponse response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = errorResult.StatusCode;
                await response.WriteAsync(JsonSerializer.Serialize(errorResult));
            }

        }
    }
}

