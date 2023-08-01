namespace MTMA.Services.Mapping
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using AutoMapper.QueryableExtensions;

    /// <summary>
    /// Provides extension methods to map IQueryable collections to destination types using AutoMapper.
    /// </summary>
    public static class QueryableMappingExtensions
    {
        /// <summary>
        /// Projects the elements of the IQueryable sequence to the specified destination type using AutoMapper.
        /// Optionally, specific members of the destination type can be expanded during the mapping process.
        /// </summary>
        /// <typeparam name="TDestination">The destination type to which elements are projected.</typeparam>
        /// <param name="source">The IQueryable source sequence to be projected.</param>
        /// <param name="membersToExpand">An array of expressions specifying the members to expand during mapping.</param>
        /// <returns>An IQueryable sequence of the specified destination type.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the source IQueryable is null.</exception>
        public static IQueryable<TDestination> To<TDestination>(
            this IQueryable source,
            params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ProjectTo(AutoMapperConfig.MapperInstance.ConfigurationProvider, null, membersToExpand);
        }

        /// <summary>
        /// Projects the elements of the IQueryable sequence to the specified destination type using AutoMapper.
        /// The mapping behavior can be controlled using the provided parameters object.
        /// </summary>
        /// <typeparam name="TDestination">The destination type to which elements are projected.</typeparam>
        /// <param name="source">The IQueryable source sequence to be projected.</param>
        /// <param name="parameters">An object containing parameters to control the mapping behavior.</param>
        /// <returns>An IQueryable sequence of the specified destination type.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the source IQueryable is null.</exception>
        public static IQueryable<TDestination> To<TDestination>(
            this IQueryable source,
            object parameters)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ProjectTo<TDestination>(AutoMapperConfig.MapperInstance.ConfigurationProvider, parameters);
        }
    }
}