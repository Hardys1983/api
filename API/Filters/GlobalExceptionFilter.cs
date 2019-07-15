﻿using API.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace API
{
    public class GlobalExceptionFilter: IExceptionFilter
    {
        public GlobalExceptionFilter()
        {

        }

        public void OnException(ExceptionContext context)
        {
            var exceptionMessage = new StringBuilder($"Exception Message: {context.Exception.Message}");

            for(var inner = context.Exception.InnerException; inner != null; inner = inner.InnerException)
            {
                exceptionMessage.Append($"\nInner Exception: {context.Exception.InnerException}");
            }

            context.Result = new JsonResult(new Error
            {
                Code = StatusCodes.Status500InternalServerError,
                Message = exceptionMessage.ToString()
            });
        }
    }
}
