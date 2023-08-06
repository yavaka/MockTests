namespace MTMA.Data.Seeding
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using MTMA.Data.Seeding.Seeders;

    /// <summary>
    /// Implementation of the ISeeder interface to seed initial data into the MTMADbContext.
    /// </summary>
    public class MTMADbContextSeeder : ISeeder
    {
        /// <summary>
        /// Seed initial data into the MTMADbContext using the provided dbContext and serviceProvider.
        /// </summary>
        /// <param name="dbContext">The MTMADbContext instance.</param>
        /// <param name="serviceProvider">The IServiceProvider instance to provide necessary services for seeding.</param>
        /// <exception cref="ArgumentNullException">Thrown when dbContext or serviceProvider is null.</exception>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1009:Closing parenthesis should be spaced correctly", Justification = "Reviewed")]
        public async Task SeedAsync(MTMADbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext is null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider is null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var logger = serviceProvider.GetService<ILoggerFactory>()!.CreateLogger(typeof(MTMADbContextSeeder));

            var seeders = new List<ISeeder>
            {
                new RolesSeeder(), // roles seeder must be always at the top
                new UsersSeeder(),
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
            }
        }
    }
}
