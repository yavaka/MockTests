namespace MTMA.API.Configuration
{
    using System.Reflection;
    using System.Text;
    using FluentValidation;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using MTMA.Data;
    using MTMA.Data.Common.Repositories;
    using MTMA.Data.Models.Identity;
    using MTMA.Data.Repositories;
    using MTMA.Data.Seeding;
    using MTMA.Services.Configuration;
    using MTMA.Services.Mapping;
    using MTMA.Services.ServiceModels;
    using MTMA.Services.ServiceModels.Configuration;
    using SlackLogger;

    internal static class APIConfiguration
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddSingleton(configuration);

            services.AddDatabase(configuration);
            services.AddIdentity();
            services.AddAuthenticationToken(configuration);

            services.AddMTMAOptions(configuration);
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

            // Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        public static WebApplication ConfigureMTMAAPI(this WebApplication app)
        {
            // Seed data on application startup
            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<MTMADbContext>();
                dbContext.Database.Migrate();
                new MTMADbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorServiceModel).GetTypeInfo().Assembly);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            return app;
        }

        private static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            // Identity
            services.AddIdentity<MTMAUser, MTMARole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<MTMADbContext>();

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MTMADbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        private static IServiceCollection AddMTMAOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AdminOptions>(configuration.GetSection(AdminOptions.Admin));
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Jwt));

            return services;
        }

        private static IServiceCollection AddAuthenticationToken(this IServiceCollection services, IConfiguration configuration)
        {
            var secret = configuration.GetSection(JwtOptions.Jwt).GetValue<string>(nameof(JwtOptions.Secret));

            var key = Encoding.ASCII.GetBytes(secret!);

            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            services.AddHttpContextAccessor();

            return services;
        }
    }
}
