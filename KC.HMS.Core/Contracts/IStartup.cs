using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace KC.HMS.Core.Contracts
{
    public interface IServiceStartup
    {
    
        void ConfigureServices(IServiceCollection services);
        void Configure(IApplicationBuilder app);

    }

}
