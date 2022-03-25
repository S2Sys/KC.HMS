using AutoMapper;

namespace KC.HMS.Web.Infrastructure.Startups
{
    public class AutoMapperStartup : IServiceStartup
    {
        public void Configure(IApplicationBuilder app)
        {
             
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var config = new MapperConfiguration(mapper =>
            {

                // DownloadDto.AutoMapperConfig(mapper);
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
