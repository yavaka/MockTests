namespace MTMA.Data.Common.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Generic interface representing a repository for a data entity.
    /// Inherits from the IDisposable interface to allow resource cleanup.
    /// </summary>
    /// <typeparam name="TEntity">The type of the data entity.</typeparam>
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        /// <summary>
        /// Gets an IQueryable representing all entities of type TEntity.
        /// </summary>
        /// <returns>An IQueryable representing all entities.</returns>
        IQueryable<TEntity> All();

        /// <summary>
        /// Gets an IQueryable representing all entities of type TEntity without change tracking.
        /// </summary>
        /// <returns>An IQueryable representing all entities without change tracking.</returns>
        IQueryable<TEntity> AllAsNoTracking();

        /// <summary>
        /// Asynchronously adds a new entity of type TEntity to the repository.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Updates an existing entity of type TEntity in the repository.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes an existing entity of type TEntity from the repository.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Asynchronously saves changes made to the repository.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation and the number of affected entities.</returns>
        Task<int> SaveChangesAsync();
    }
}
