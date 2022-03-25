using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace KC.HMS.Core.Middlewares
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
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = new ErrorResponse
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = "Internal Server Error " + ex.Message,
                    Exception = ex
                };

                httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }

       
    }
}
