
using Microsoft.OpenApi.Models;

namespace KC.HMS.Web.Infrastructure.Startups
{

    public class SwaggerStartup : IServiceStartup
    {
        public IConfiguration Configuration { get; set; }

        public SwaggerStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = Configuration["Swagger:Title"], Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", Configuration["Swagger:Description"]));
        }
    }
}
