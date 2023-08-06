namespace MTMA.Data.Seeding
{
    /// <summary>
    /// Interface for seeders, responsible for seeding initial data into the MTMADbContext.
    /// </summary>
    public interface ISeeder
    {
        /// <summary>
        /// Seed initial data into the MTMADbContext.
        /// </summary>
        /// <param name="dbContext">The MTMADbContext instance.</param>
        /// <param name="serviceProvider">The IServiceProvider instance to provide necessary services for seeding.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task SeedAsync(MTMADbContext dbContext, IServiceProvider serviceProvider);
    }
}
