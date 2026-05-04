using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Domain.Interfaces.Provider;

namespace WEBEditorAPI.Api.Filters
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
