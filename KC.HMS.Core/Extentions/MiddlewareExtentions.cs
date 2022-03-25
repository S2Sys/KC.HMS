using Microsoft.AspNetCore.Builder;

namespace KC.HMS.Core.Extentions
{
    public static class Middleware
    {
        public static void UseGlobalException(this IApplicationBuilder app)
        {
            UseMiddlewareExtensions.UseMiddleware<ExceptionMiddleware>(app);
        }

        public static void UseResponseTime(this IApplicationBuilder app)
        {
            UseMiddlewareExtensions.UseMiddleware<ResponseTimeMiddleware>(app);
        }

         
        public static void Use<T>(this IApplicationBuilder app)
        {
            UseMiddlewareExtensions.UseMiddleware<T>(app);
        }
    }
}
