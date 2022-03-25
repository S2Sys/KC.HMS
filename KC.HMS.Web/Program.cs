var builder = WebApplication.CreateBuilder(args);

var startup = new ApplicationStartup(builder.Environment, builder.Configuration);

startup.ConfigureServices(builder);

var app = builder.Build();


var seed = builder.Configuration.GetValue<bool>("AllowSeed");
if (seed)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        try
        {
            //Seed Default Users
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await ApplicationDbContextSeed.SeedEssentialsAsync(userManager, roleManager);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occurred seeding the DB.");
        }
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


startup.Configure(app); 
app.UseAuthentication();
app.Run();
