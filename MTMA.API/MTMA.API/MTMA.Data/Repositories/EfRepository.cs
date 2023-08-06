namespace MTMA.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using MTMA.Data.Common.Repositories;

    /// <summary>
    /// Represents a generic repository for entities that implements the IRepository interface.
    /// Provides basic CRUD operations for the entity using Entity Framework.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity that the repository works with.</typeparam>
    public class EfRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EfRepository{TEntity}"/> class with the specified database context.
        /// </summary>
        /// <param name="context">The database context used by the repository.</param>
        public EfRepository(MTMADbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.DbSet = this.Context.Set<TEntity>();
        }

        /// <summary>
        /// Gets or sets the DbSet representing the entity in the database.
        /// </summary>
        protected DbSet<TEntity> DbSet { get; set; }

        /// <summary>
        /// Gets or sets the database context used by the repository.
        /// </summary>
        protected MTMADbContext Context { get; set; }

        /// <summary>
        /// Gets all entities in the repository.
        /// </summary>
        /// <returns>An IQueryable representing all entities in the repository.</returns>
        public virtual IQueryable<TEntity> All()
            => this.DbSet;

        /// <summary>
        /// Gets all entities in the repository without tracking changes.
        /// </summary>
        /// <returns>An IQueryable representing all entities in the repository without tracking changes.</returns>
        public virtual IQueryable<TEntity> AllAsNoTracking()
            => this.DbSet.AsNoTracking();

        /// <summary>
        /// Adds a new entity to the repository asynchronously.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public virtual Task AddAsync(TEntity entity)
            => this.DbSet.AddAsync(entity).AsTask();

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        public virtual void Update(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        /// <summary>
        /// Marks an entity for deletion in the repository.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public virtual void Delete(TEntity entity)
            => this.DbSet.Remove(entity);

        /// <summary>
        /// Saves changes made to the repository asynchronously.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation and the number of objects written to the database.</returns>
        public Task<int> SaveChangesAsync()
            => this.Context.SaveChangesAsync();

        /// <summary>
        /// Disposes the repository and the underlying context.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the repository and the underlying context.
        /// </summary>
        /// <param name="disposing">A flag indicating if the repository is being disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Context?.Dispose();
            }
        }
    }
}
