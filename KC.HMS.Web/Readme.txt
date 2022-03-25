UPDATE IDENTITY DATABASE 
********************************************************************************************************************************

Add-Migration
********************************
Add-Migration -StartupProject KC.HMS.Web -Context "ApplicationDbContext" Initial

UPDATE/DELETE DATABASE
********************************
UPDATE-DATABASE
DROP-DATABASE

 


public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
    services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();
    services.AddControllersWithViews();
    services.AddRazorPages();
}

 public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  