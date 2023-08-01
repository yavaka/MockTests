namespace MTMA.API.Configuration
{
    using System.Reflection;
    using MTMA.Services.Mapping;
    using MTMA.Services.ServiceModels;

    internal static class APIConfiguration
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            return services;
        }
    }
}
