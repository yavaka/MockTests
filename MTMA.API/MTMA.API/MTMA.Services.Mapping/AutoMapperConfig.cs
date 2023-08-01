namespace MTMA.Services.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using AutoMapper;

    /// <summary>
    /// Configure Automapper.
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// Track whether or not the mappings have already been registered.
        /// </summary>
        private static bool initialized;

        /// <summary>
        /// Gets or Sets reference to the `Mapper` instance that is used to perform object mapping.
        /// </summary>
        public static IMapper MapperInstance { get; set; } = default!;

        /// <summary>
        /// Registers the mappings with AutoMapper.
        /// </summary>
        /// <param name="assemblies">List of assemblies that contain the types that the mappings will be applied to.</param>
        public static void RegisterMappings(params Assembly[] assemblies)
        {
            if (initialized)
            {
                return;
            }

            initialized = true;

            var types = assemblies.SelectMany(a => a.GetExportedTypes()).ToList();

            var config = new MapperConfigurationExpression();
            config.CreateProfile(
                "ReflectionProfile",
                configuration =>
                {
                    // Register mappings for classes implementing the IMapFrom<> interface.
                    foreach (var map in GetFromMaps(types))
                    {
                        configuration.CreateMap(map.Source, map.Destination);
                    }

                    // Register mappings for classes implementing the IMapTo<> interface.
                    foreach (var map in GetToMaps(types))
                    {
                        configuration.CreateMap(map.Source, map.Destination);
                    }

                    // Call the CreateMappings method on classes implementing the IHaveCustomMappings interface.
                    foreach (var map in GetCustomMappings(types))
                    {
                        map.CreateMappings(configuration);
                    }
                });
            MapperInstance = new Mapper(new MapperConfiguration(config));
        }

        /// <summary>
        /// Get source and destination types for classes implementing the IMapFrom<> interface.
        /// </summary>
        /// <param name="types">A collection of types from which to retrieve mappings.</param>
        /// <returns>A collection of TypesMap containing source and destination types for mapping.</returns>
        private static IEnumerable<TypesMap> GetFromMaps(IEnumerable<Type> types)
        {
            var fromMaps = from t in types
                           from i in t.GetTypeInfo().GetInterfaces()
                           where i.GetTypeInfo().IsGenericType &&
                                 i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                                 !t.GetTypeInfo().IsAbstract &&
                                 !t.GetTypeInfo().IsInterface
                           select new TypesMap
                           {
                               Source = i.GetTypeInfo().GetGenericArguments()[0],
                               Destination = t,
                           };

            return fromMaps;
        }

        /// <summary>
        /// Get source and destination types for classes implementing the IMapTo<> interface.
        /// </summary>
        /// <param name="types">A collection of types from which to retrieve mappings.</param>
        /// <returns>A collection of TypesMap containing source and destination types for mapping.</returns>
        private static IEnumerable<TypesMap> GetToMaps(IEnumerable<Type> types)
        {
            var toMaps = from t in types
                         from i in t.GetTypeInfo().GetInterfaces()
                         where i.GetTypeInfo().IsGenericType &&
                               i.GetTypeInfo().GetGenericTypeDefinition() == typeof(IMapTo<>) &&
                               !t.GetTypeInfo().IsAbstract &&
                               !t.GetTypeInfo().IsInterface
                         select new TypesMap
                         {
                             Source = t,
                             Destination = i.GetTypeInfo().GetGenericArguments()[0],
                         };

            return toMaps;
        }

        /// <summary>
        /// Get instances of classes implementing the IHaveCustomMappings interface.
        /// </summary>
        /// <param name="types">A collection of types from which to retrieve mappings.</param>
        /// <returns>A collection of classes implementing the IHaveCustomMappings interface.</returns>
        private static IEnumerable<IHaveCustomMappings> GetCustomMappings(IEnumerable<Type> types)
        {
            var customMaps = from t in types
                             from i in t.GetTypeInfo().GetInterfaces()
                             where typeof(IHaveCustomMappings).GetTypeInfo().IsAssignableFrom(t) &&
                                   !t.GetTypeInfo().IsAbstract &&
                                   !t.GetTypeInfo().IsInterface
                             select Activator.CreateInstance(t) as IHaveCustomMappings;

            return customMaps;
        }

        private class TypesMap
        {
            public Type Source { get; set; } = default!;

            public Type Destination { get; set; } = default!;
        }
    }
}
