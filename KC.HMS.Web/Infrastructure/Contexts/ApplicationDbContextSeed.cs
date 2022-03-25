namespace KC.HMS.Web.Infrastructure.Contexts
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedEssentialsAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles & Seed Default User
            //await roleManager.CreateAsync(new IdentityRole(Roles.Administrator.ToString()));
            //await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            var seedUsers = new List<SeedUser>();
            foreach (var role in Enum.GetValues(typeof(Roles))
                    .Cast<Enum>())
            {


                var roleEntity = await roleManager.FindByNameAsync(role.ToString());
                if (roleEntity == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role.ToString()));
                }
                //var userEntity = await userManager.FindByNameAsync(role.ToString());
                //if (userEntity == null)
                //{
                seedUsers.Add(new SeedUser()
                {
                    Username = role.ToString(),
                    Email = role.ToString() + "@app.com",
                    Password = SeedUser.DefaultPassword,
                    Role = (Roles)role
                });
                //}
            }


            foreach (var seedUser in seedUsers)
            {

                // if (userManager.Users.All(u => u.UserName != seedUser.Username))
                {
                    await userManager.CreateAsync(seedUser.ToDTO(), seedUser.Password);
                    await userManager.AddToRoleAsync(seedUser.ToDTO(), seedUser.Role.ToString());
                }
            }
        }
    }
}
