using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MTMA.AdminPanel.Components.Account;
using MTMA.Data.Models.Identity;
using MTMA.Data;
using Microsoft.EntityFrameworkCore;
using MTMA.AdminPanel.Components;
using MTMA.Data.Seeding;
using MTMA.Services.Configuration;
using MTMA.Data.Repositories;
using MTMA.Services.ServiceModels;
using MTMA.Data.Common.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MTMA.Services.ServiceModels.Configuration;
using SlackLogger;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MTMA.AdminPanel.Configuration
{
    internal static class AdminPanelConfiguration
    {
        public static IServiceCollection AddAdminPanelServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRazorComponents().AddInteractiveServerComponents();

            services.AddDatabase(configuration);
            services.AddIdentity();

            services.AddAdminPanelOptions(configuration);
            services.AddMTMAServices();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            // Fluent validation
            services.AddValidatorsFromAssemblyContaining<ErrorServiceModel>();
            services.AddFluentValidationAutoValidation(); // auto validate service models
            services.AddMTMAServiceModelsValidators();

            // Slack logger
            services.AddLogging(builder =>
            {
                builder.AddConfiguration(configuration.GetSection("Logging"));
                builder.AddSlack();
            });

            return services;
        }

        public static WebApplication ConfigureAdminPanel(this WebApplication app)
        {
            // Seed data on application startup
            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<MTMADbContext>();
                dbContext.Database.Migrate();
                new MTMADbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            return app;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<MTMADbContext>(options => options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")))
                .AddDatabaseDeveloperPageExceptionFilter();

        private static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddCascadingAuthenticationState();
            services.AddScoped<IdentityUserAccessor>();
            services.AddScoped<IdentityRedirectManager>();
            services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            });

            services.AddIdentity<MTMAUser, MTMARole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                }).AddEntityFrameworkStores<MTMADbContext>();

            services.AddSingleton<IEmailSender<MTMAUser>, IdentityNoOpEmailSender>();

            return services;
        }

        private static IServiceCollection AddAdminPanelOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AdminOptions>(configuration.GetSection(AdminOptions.Admin));

            return services;
        }
    }
}
