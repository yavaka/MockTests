namespace MTMA.API.Configuration
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = null)]
    internal static class APIConfiguration
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            return services;
        }
    }
}
