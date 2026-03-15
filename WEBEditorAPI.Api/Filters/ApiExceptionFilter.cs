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
                context.Result = new ObjectResult(new
                {
                    message = ex.Message
                })
                {
                    StatusCode = ex.StatusCode
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
