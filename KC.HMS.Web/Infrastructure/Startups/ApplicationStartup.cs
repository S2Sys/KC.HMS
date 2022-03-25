
using KC.HMS.Core.Domain;
using KC.HMS.Core.Extensions;
using KC.HMS.Core.Settings;
using KC.HMS.Web.Infrastructure.Contracts;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Data.SqlClient;
using Serilog;
using System.Data;

namespace KC.HMS.Web.Infrastructure
{
    public class ApplicationStartup : IApplicationStartup
    {
        private const string DbConnectionString = "DBConnectionString";
        private const string ExternalServiceEndPoint = "OtherServiceEndpoints:MasterDataService";

        #region Startup

        private readonly IServiceStartup identityStartup;
        private readonly IServiceStartup swaggerStartup;
        private readonly IServiceStartup repositoriesStatup;
        private readonly IServiceStartup servicesStartup;
        private readonly IServiceStartup autoMapperStartup;
        private readonly IServiceStartup filterStartup;

        #endregion

        public IConfiguration Configuration { get; }

        public ApplicationStartup(
            IHostEnvironment hostEnvironment,
            IConfiguration configuration)
        {
            Configuration = configuration;
            identityStartup = new IdentityStartup(Configuration);
            swaggerStartup = new SwaggerStartup(Configuration);
            repositoriesStatup = new RepositoriesStartup(Configuration);
            servicesStartup = new ServicesStartup();
            autoMapperStartup = new AutoMapperStartup();
            filterStartup = new MiddlewaresStartup();
        }

        public void ConfigureServices(WebApplicationBuilder builder)
        {
            string outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}";
            Log.Logger = new
                LoggerConfiguration().WriteTo.File("C:\\Logs\\Demo-{Date}.txt",
                rollingInterval: RollingInterval.Hour,
                outputTemplate: outputTemplate)
                .CreateLogger();

            builder.Services.AddSingleton(Configuration);

           // builder.Services.AddTransient(s => s.GetService<HttpContext>()); 


            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            // Inject IDbConnection, with implementation from SqlConnection class.
            builder.Services.AddTransient<IDbConnection>((sp) => new SqlConnection(connectionString));
            DapperDb.Initialize(new SqlConnection(connectionString), builder.Configuration);

            var h = DapperDb.Query<HotelDto>("GetHotel", CommandType.StoredProcedure, null);

            builder.Services.AddTransient<IEmailSender, SendGridEmailSender>();
            builder.Services.Configure<SendGridEmailSenderOptions>(options =>
            {
                options.ApiKey = Configuration["ExternalProviders:SendGrid:ApiKey"];
                options.SenderEmail = Configuration["ExternalProviders:SendGrid:SenderEmail"];
                options.SenderName = Configuration["ExternalProviders:SendGrid:SenderName"];

            });

            // Add services to the container.

            builder.Services.AddAuthentication(ApplicationConstants.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    options.SlidingExpiration = true;
                    options.AccessDeniedPath = "/Account/Manage/AccessDenied/";
                })
                .AddGoogle("google", opt =>
                {
                    var googleAuth = Configuration.GetSection("Authentication:Google");
                    opt.ClientId = googleAuth["ClientId"];
                    opt.ClientSecret = googleAuth["ClientSecret"];
                    opt.SignInScheme = IdentityConstants.ExternalScheme;
                });

            identityStartup.ConfigureServices(builder.Services);
            swaggerStartup.ConfigureServices(builder.Services);
            repositoriesStatup.ConfigureServices(builder.Services);
            servicesStartup.ConfigureServices(builder.Services);
            autoMapperStartup.ConfigureServices(builder.Services);
            filterStartup.ConfigureServices(builder.Services);

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddRazorPages();
            builder.Services.AddControllers();


            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

        }

        public void Configure(WebApplication app)
        {

            swaggerStartup.Configure(app);

            app.UseCors(x => x.AllowAnyMethod()
                .AllowAnyHeader().
                SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseSerilogRequestLogging();
            app.MapRazorPages();
        }
    }

}

