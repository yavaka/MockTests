namespace MTMA.Services.ServiceModels.Configuration
{
    using System.Reflection;
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceModelsValidatorsConfiguration
    {
        /// <summary>
        /// Extension method to register validators for service models in the application.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to which the validators will be added.</param>
        /// <returns>The modified <see cref="IServiceCollection"/> containing the registered validators.</returns>
        public static IServiceCollection AddMTMAServiceModelsValidators(this IServiceCollection services)
        {
            // Get the IValidator<> interface type
            var validatorInterfaceType = typeof(IValidator<>);

            // Get the assembly where the extension method is defined
            var assembly = Assembly.GetExecutingAssembly();

            // Get the namespace of service models
            var serviceModelsNamespace = typeof(ErrorServiceModel).Namespace;

            // Find types that implement the IValidator<> interface within the specified namespace
            var validatorImplementations = assembly.GetTypes()
                .Where(type => type.Namespace == serviceModelsNamespace
                    && !type.IsAbstract
                    && !type.IsInterface
                    && ImplementsOpenGenericInterface(type, validatorInterfaceType));

            foreach (var implementationType in validatorImplementations)
            {
                // Get the T parameter type for IValidator<T>
                var serviceModelType = implementationType.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == validatorInterfaceType)
                    .Select(i => i.GetGenericArguments()[0])
                    .FirstOrDefault();

                if (serviceModelType != null)
                {
                    // Register each implementation as a scoped service
                    services.AddScoped(typeof(IValidator<>).MakeGenericType(serviceModelType), implementationType);
                }
            }

            // Return the modified service collection
            return services;
        }

        /// <summary>
        /// Checks if a type implements a specific open generic interface.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <param name="openGenericType">The open generic interface type.</param>
        /// <returns><c>true</c> if the type implements the open generic interface; otherwise, <c>false</c>.</returns>
        private static bool ImplementsOpenGenericInterface(Type type, Type openGenericType)
            => type.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == openGenericType);

    }
}
