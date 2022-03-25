
using Microsoft.Data.SqlClient;
using System.Data;

namespace KC.HMS.Web.Infrastructure.Startups
{
    public class IdentityStartup : IServiceStartup
    {

        public IConfiguration Configuration { get; set; }

        public IdentityStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Configure(IApplicationBuilder app)
        {
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.MapRazorPages();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
 

            services.AddTransient<IDbConnection>(db => new SqlConnection(
                Configuration.GetConnectionString(connectionString)));

           // options.UseSqlServer("server=.\\sqlexpress;database=dsafdsaf;uid=sa;pwd=123456", b => b.MigrationsAssembly("WebApplication3"));


            //User Manager Service
            services.AddIdentity<ApplicationUser, IdentityRole>(
                options => options.SignIn.RequireConfirmedAccount = false
                                       )
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddRazorPages();


            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });


            services.AddScoped<IUserService, UserService>();

          //  string name = b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
            //Adding DB Context with MSSQL
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                   //b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                   b => b.MigrationsAssembly("KC.HMS.Web")
                ));
             

        }
    }
}
