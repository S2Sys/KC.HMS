using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KC.HMS.Infrastructure.Middlewares
{
    /// <summary>
    /// ResponseTime  Middleware sent response time in each request in the header X-Response-Time-ms header
    /// </summary>
    public class ResponseTimeMiddleware
    {
        // Name of the Response Header, Custom Headers starts with "X-"  
        private const string RESPONSE_HEADER_RESPONSE_TIME = "X-Response-Time-ms";

        private readonly RequestDelegate _next;
        public ResponseTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public Task InvokeAsync(HttpContext context)
        {
            var watch = new Stopwatch();
            watch.Start();
            context.Response.OnStarting(() => {
                 watch.Stop();
                var responseTime = watch.ElapsedMilliseconds;
                // Add the Response time information in the Response headers.   
                context.Response.Headers[RESPONSE_HEADER_RESPONSE_TIME] = responseTime.ToString();
                return Task.CompletedTask;
            });
  
            return this._next(context);
        }
    }
}
