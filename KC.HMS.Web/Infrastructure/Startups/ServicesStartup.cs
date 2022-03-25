

using KC.HMS.Core.Services;
using KC.HMS.Web.Infrastructure.Contracts;

namespace KC.HMS.Web.Infrastructure.Startups
{
    public class ServicesStartup : IServiceStartup
    {
        public void Configure(IApplicationBuilder app)
        {

        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IExternalServices, ExternalServices>();
            services.AddTransient<IValidateService, ValidateService>();
        }
    }
}
