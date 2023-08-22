namespace MTMA.Services.Configuration
{
    using Microsoft.Extensions.DependencyInjection;
    using MTMA.Services.Identity;
    using MTMA.Services.JwtGenerator;

    public static class ServicesConfiguration
    {
        /// <summary>
        /// Add services from MTMA.Services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMTMAServices(this IServiceCollection services)
        {
            services.AddTransient<IJwtGeneratorService, JwtGeneratorService>();
            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }
    }
}
