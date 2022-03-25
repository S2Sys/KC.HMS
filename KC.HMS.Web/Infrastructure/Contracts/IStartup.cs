namespace KC.HMS.Web.Infrastructure.Contracts
{
    public interface IServiceStartup
    {
    
        public void ConfigureServices(IServiceCollection services);
        public void Configure(IApplicationBuilder app);

    }

}
