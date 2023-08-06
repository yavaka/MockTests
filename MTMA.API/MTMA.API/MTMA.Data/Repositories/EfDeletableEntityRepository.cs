namespace MTMA.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using MTMA.Data.Common.Models;
    using MTMA.Data.Common.Repositories;

    /// <summary>
    /// Represents a generic repository for deletable entities that implements the IDeletableEntityRepository interface.
    /// Provides CRUD operations for deletable entities using Entity Framework with soft deletion support.
    /// </summary>
    /// <typeparam name="TEntity">The type of deletable entity that the repository works with.</typeparam>
    public class EfDeletableEntityRepository<TEntity> : EfRepository<TEntity>, IDeletableEntityRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EfDeletableEntityRepository{TEntity}"/> class with the specified database context.
        /// </summary>
        /// <param name="context">The database context used by the repository.</param>
        public EfDeletableEntityRepository(MTMADbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets all entities in the repository that are not marked as deleted.
        /// </summary>
        /// <returns>An IQueryable representing non-deleted entities in the repository.</returns>
        public override IQueryable<TEntity> All()
            => base.All().Where(x => !x.IsDeleted);

        /// <summary>
        /// Gets all entities in the repository that are not marked as deleted without tracking changes.
        /// </summary>
        /// <returns>An IQueryable representing non-deleted entities in the repository without tracking changes.</returns>
        public override IQueryable<TEntity> AllAsNoTracking()
            => base.AllAsNoTracking().Where(x => !x.IsDeleted);

        /// <summary>
        /// Gets all entities in the repository, including those marked as deleted.
        /// </summary>
        /// <returns>An IQueryable representing all entities in the repository, including deleted ones.</returns>
        public IQueryable<TEntity> AllWithDeleted()
            => base.All().IgnoreQueryFilters();

        /// <summary>
        /// Gets all entities in the repository, including those marked as deleted, without tracking changes.
        /// </summary>
        /// <returns>An IQueryable representing all entities in the repository without tracking changes, including deleted ones.</returns>
        public IQueryable<TEntity> AllAsNoTrackingWithDeleted()
            => base.AllAsNoTracking().IgnoreQueryFilters();

        /// <summary>
        /// Performs a hard delete of an entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to be hard deleted.</param>
        public void HardDelete(TEntity entity)
            => base.Delete(entity);

        /// <summary>
        /// Undeletes a soft-deleted entity in the repository by marking it as not deleted.
        /// </summary>
        /// <param name="entity">The entity to be undeleted.</param>
        public void Undelete(TEntity entity)
        {
            entity.IsDeleted = false;
            entity.DeletedOn = null;
            this.Update(entity);
        }

        /// <summary>
        /// Marks an entity for soft deletion by setting the IsDeleted flag and DeletedOn date.
        /// </summary>
        /// <param name="entity">The entity to be soft deleted.</param>
        public override void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
            this.Update(entity);
        }
    }
}
