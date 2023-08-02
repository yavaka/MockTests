namespace AspNetCoreTemplate.Data.Common.Repositories
{
    using System.Linq;

    using AspNetCoreTemplate.Data.Common.Models;

    /// <summary>
    /// Generic interface for a repository that deals with entities supporting soft deletion.
    /// Inherits from the IRepository interface.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IDeletableEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        /// <summary>
        /// Retrieves all entities, including those marked as deleted.
        /// </summary>
        /// <returns>An IQueryable representing all entities, including deleted ones.</returns>
        IQueryable<TEntity> AllWithDeleted();

        /// <summary>
        /// Retrieves all entities, including those marked as deleted, without tracking changes.
        /// </summary>
        /// <returns>An IQueryable representing all entities without change tracking, including deleted ones.</returns>
        IQueryable<TEntity> AllAsNoTrackingWithDeleted();

        /// <summary>
        /// Performs a hard delete of the specified entity.
        /// A hard delete permanently removes the entity from the database.
        /// </summary>
        /// <param name="entity">The entity to be hard deleted.</param>
        void HardDelete(TEntity entity);

        /// <summary>
        /// Restores a previously deleted entity by unmarking it as deleted.
        /// </summary>
        /// <param name="entity">The entity to be undeleted.</param>
        void Undelete(TEntity entity);
    }
}
