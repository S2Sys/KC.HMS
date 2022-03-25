namespace KC.HMS.Infrastructure.Contracts
{
    public interface IApplicationStartup
    {
        void ConfigureServices(WebApplicationBuilder builder);
        void Configure(WebApplication app);

    }
    
}
