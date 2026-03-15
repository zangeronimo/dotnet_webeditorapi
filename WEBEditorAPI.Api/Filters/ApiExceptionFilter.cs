using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WEBEditorAPI.Application.Exceptions;
using WEBEditorAPI.Application.DTOs.System;

namespace WEBEditorAPI.Api.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiException ex)
            {
                int statusCode = context.Exception switch
                {
                    ApiInvalidCredentialsException => 401,
                    ApiForbiddenException => 403,
                    ApiNotFoundException => 404,
                    _ => 400
                };

                context.Result = new ObjectResult(new
                {
                    message = ex.Message
                })
                {
                    StatusCode = statusCode
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
