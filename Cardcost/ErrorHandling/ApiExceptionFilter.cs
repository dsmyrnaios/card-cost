using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cardcost.ErrorHandling
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiException)
            {
                // handle explicit 'known' API errors
                var ex = context.Exception as ApiException;
                context.Exception = null;
                apiError = new ApiError(ex.Message);
                //apiError.errors = ex.Errors;

                context.HttpContext.Response.StatusCode = ex.StatusCode;
                //
                _logger.LogError("API error. ExceptionType = {ExceptionType}, StatusCode={StatusCode}, Error={Error}", "ApiException",
                    ex.StatusCode, ex.Message);
            }

            base.OnException(context);
        }

        public class ApiException : Exception
        {
            public int StatusCode { get; set; }

            //public ValidationErrorCollection Errors { get; set; }

            public ApiException(string message, int statusCode = 500 /*ValidationErrorCollection errors = null*/) : base(message)
            {
                StatusCode = statusCode;
                //Errors = errors;
            }

            public ApiException(Exception ex, int statusCode = 500) : base(ex.Message)
            {
                StatusCode = statusCode;
            }
        }
    }
}
