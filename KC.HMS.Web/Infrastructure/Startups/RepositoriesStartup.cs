namespace KC.HMS.Web.Infrastructure.Startups
{
    public class RepositoriesStartup : IServiceStartup
    {
        public IConfiguration Configuration { get; set; }

        public RepositoriesStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Genric Repository 
            services.AddScoped(typeof(IEntityGenricRepository<>), typeof(EntityGenricRepository<>));
           // services.AddScoped(IValidateRepository, ValidateRepository)();



        }

        public void Configure(IApplicationBuilder app)
        {

        }
    }
}
