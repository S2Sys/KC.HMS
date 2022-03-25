namespace KC.HMS.Web.Infrastructure.Extentions
{
    public static class Util
    {
        public static void UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            UseMiddlewareExtensions.UseMiddleware<ExceptionMiddleware>(app);
        }

        public static void UseResponseTimeMiddleware(this IApplicationBuilder app)
        {
            UseMiddlewareExtensions.UseMiddleware<ResponseTimeMiddleware>(app);
        }

         
        public static void UseMiddleware<T>(this IApplicationBuilder app)
        {
            UseMiddlewareExtensions.UseMiddleware<T>(app);
        }
    }
}
