using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nexora.Application.Exceptions;
using Nexora.Domain.Interfaces.Provider;

namespace Nexora.Api.Filters
{
    public class ApiExceptionFilter(IMessageProvider messages) : IExceptionFilter
    {
        private readonly IMessageProvider _messages = messages;
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiException ex)
            {
                var message = _messages.Get(ex.Key);

                context.Result = new ObjectResult(new
                {
                    code = ex.Key,
                    message
                })
                {
                    StatusCode = ex.StatusCode
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
