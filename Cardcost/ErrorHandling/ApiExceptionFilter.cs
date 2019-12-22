using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cardcost.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cardcost.ErrorHandling
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            ApiError apiError;

            if (context.Exception is ApiException)
            {
                // handle explicit 'known' API errors
                var ex = context.Exception as ApiException;
                context.Exception = null;
                apiError = new ApiError(ex.Message);

                context.HttpContext.Response.StatusCode = ex.StatusCode;
                //
                //_logger.LogError("API error. ExceptionType = {ExceptionType}, StatusCode={StatusCode}, Error={Error}", "ApiException",
                //    ex.StatusCode, ex.Message);
            }
            else
            {
                var msg = context.Exception.GetBaseException().Message;
                string stack = context.Exception.StackTrace;
                apiError = new ApiError(msg);
                apiError.Detail = stack;
            }

            // always return a JSON result
            context.Result = new JsonResult(apiError);

            base.OnException(context);
        }
    }
}
