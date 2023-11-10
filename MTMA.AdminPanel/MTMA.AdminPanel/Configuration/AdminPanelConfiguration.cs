using MTMA.AdminPanel.Services.Common.SessionStorage;

namespace MTMA.AdminPanel.Configuration
{
    public static class AdminPanelConfiguration
    {
        public static IServiceCollection AddAdminPanelServices(this IServiceCollection services)
        {
            services.AddScoped<ISessionStorageAccessor, SessionStorageAccessor>();

            return services;
        }
    }
}
