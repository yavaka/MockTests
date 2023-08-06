namespace MTMA.Data.Seeding.Seeders
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using MTMA.Data;
    using MTMA.Data.Models.Identity;
    using AdminConstants = MTMA.Common.GlobalConstants.Admin;

    /// <summary>
    /// Seeder class responsible for creating the administrator user if it doesn't exist in the database.
    /// </summary>
    internal class UsersSeeder : ISeeder
    {
        /// <summary>
        /// Seed method that creates the administrator user if it doesn't exist in the database.
        /// </summary>
        /// <param name="dbContext">The application's database context.</param>
        /// <param name="serviceProvider">The service provider used to retrieve required services.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SeedAsync(MTMADbContext dbContext, IServiceProvider serviceProvider)
        {
            // Check if the administrator role exists
            if (await serviceProvider.GetRequiredService<RoleManager<MTMARole>>().FindByNameAsync(AdminConstants.RoleName) is null)
            {
                throw new Exception($"{AdminConstants.RoleName} role not found!");
            }

            // Check if the administrator user exists
            var userManager = serviceProvider.GetRequiredService<UserManager<MTMAUser>>();
            var admin = await userManager.FindByEmailAsync(AdminConstants.Email);
            if (admin is null)
            {
                // Retrieve administrator options from configuration
                var adminOpt = GetAdminOptions(serviceProvider);

                // Create and initialize the administrator user
                var adminUser = new MTMAUser
                {
                    Email = adminOpt.Email,
                    UserName = adminOpt.UserName,
                };

                // Create the administrator user and add it to the administrator role
                ValidateIdentityResult(await userManager.CreateAsync(adminUser, adminOpt.Password));
                ValidateIdentityResult(await userManager.AddToRoleAsync(adminUser, AdminConstants.RoleName));
            }
        }

        private static void ValidateIdentityResult(IdentityResult result)
        {
            if (result.Succeeded is false)
            {
                throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
            }
        }

        private static AdminOptions GetAdminOptions(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var scopedProvider = scope.ServiceProvider;
            return scopedProvider.GetRequiredService<IOptionsSnapshot<AdminOptions>>().Value;
        }
    }
}
