namespace MTMA.Data.Seeding.Seeders
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using MTMA.Data.Models.Identity;
    using AdminConstants = MTMA.Common.GlobalConstants.Admin;

    /// <summary>
    /// Implementation of the ISeeder interface to seed roles into the MTMADbContext.
    /// </summary>
    internal class RolesSeeder : ISeeder
    {
        /// <summary>
        /// Seed the AdministratorRole into the MTMADbContext using the provided roleManager and roleName.
        /// </summary>
        /// <param name="dbContext">The MTMADbContext instance.</param>
        /// <param name="serviceProvider">The IServiceProvider instance to provide necessary services for seeding.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SeedAsync(MTMADbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<MTMARole>>();

            var role = await roleManager.FindByNameAsync(AdminConstants.RoleName);
            if (role is null)
            {
                var result = await roleManager.CreateAsync(new MTMARole(AdminConstants.RoleName));
                if (result.Succeeded is false)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
