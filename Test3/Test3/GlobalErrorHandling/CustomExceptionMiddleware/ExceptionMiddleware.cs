using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Test3.GlobalErrorHandling.Extensions;
using Test3.GlobalErrorHandling.Models;

namespace Test3.GlobalErrorHandling.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;      

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {             
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            int httpStatusCode;
            string message;

            switch (exception)
            {
                case var _ when exception is ValidationException:
                    httpStatusCode = (int)HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;
                default:
                    httpStatusCode = (int)HttpStatusCode.InternalServerError;
                    message = exception.Message;
                    break;
            }
            context.Response.StatusCode = httpStatusCode;


            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = httpStatusCode,
                Message = message
            }.ToString());
        }
    }

}
