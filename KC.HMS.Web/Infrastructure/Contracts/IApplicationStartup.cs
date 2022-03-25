namespace KC.HMS.Web.Infrastructure.Contracts
{
    public interface IApplicationStartup
    {
        public void ConfigureServices(WebApplicationBuilder builder);
        public void Configure(WebApplication app); 

    }
    
}
