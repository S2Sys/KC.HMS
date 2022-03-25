using KC.HMS.Core.Extentions;
namespace KC.HMS.Web.Infrastructure.Startups
{
    public class MiddlewaresStartup : IServiceStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
              // options.Filters.Add<BusinessExceptionFilter>();
            });

          
            #region Genric ValidationFilter 
            //services.AddScoped<ValidationFilterAttribute<SeedUser>>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            }); 
            #endregion
        }

        public void Configure(IApplicationBuilder app)
        {
            //Middleware registration 
            Middleware.Use<ResponseTimeMiddleware>(app); // app.UseMiddleware<ResponseTimeMiddleware>();
            Middleware.Use<ExceptionMiddleware>(app);  //app.UseMiddleware<ExceptionMiddleware>();


        }

        
    }
}
