using KC.HMS.Core.Contracts;
using KC.HMS.Core.Services;
using KC.HMS.Services.Contracts;
using KC.HMS.Services.Repositories;
using KC.HMS.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KC.HMS.Services
{
    public static class ServiceRegistration
    {
        public static void UseServices(this IServiceCollection services)
        {
            services.AddScoped<IHotelRepository, HotelRepository>();

            services.AddScoped<IRoomRepository, RoomRepository>();

            services.AddTransient<IUserService, UserService>();

            services.AddScoped<IValidateService, ValidateService>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddHttpContextAccessor();
        }
         
    }
}
