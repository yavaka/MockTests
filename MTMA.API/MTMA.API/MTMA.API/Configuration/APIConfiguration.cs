namespace MTMA.API.Configuration
{
    using System.Reflection;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using MTMA.Data;
    using MTMA.Data.Common.Repositories;
    using MTMA.Data.Models.Identity;
    using MTMA.Data.Repositories;
    using MTMA.Data.Seeding;
    using MTMA.Services.Mapping;
    using MTMA.Services.ServiceModels;

    internal static class APIConfiguration
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddDbContext<MTMADbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton(configuration);

            // Identity
            services.AddIdentity<MTMAUser, MTMARole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<MTMADbContext>();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            // Options
            services.Configure<AdminOptions>(configuration.GetSection(AdminOptions.Admin));

            return services;
        }

        public static WebApplication ConfigureAPI(this WebApplication app)
        {
            // Seed data on application startup
            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<MTMADbContext>();
                dbContext.Database.Migrate();
                new MTMADbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorServiceModel).GetTypeInfo().Assembly);

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
