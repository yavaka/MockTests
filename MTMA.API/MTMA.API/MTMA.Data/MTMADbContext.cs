namespace MTMA.Data
{
    using System.Reflection;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MTMA.Data.Common.Models;
    using MTMA.Data.Models.Identity;

    /// <summary>
    /// Represents the main application's DbContext, extending the IdentityDbContext with custom entities and configurations.
    /// </summary>
    public class MTMADbContext : IdentityDbContext<MTMAUser, MTMARole, string>
    {
        // A private static field used to store a reference to the SetIsDeletedQueryFilter method via reflection.
        // It is used later to apply a global query filter for not deleted entities only during model creation.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1009:Closing parenthesis should be spaced correctly", Justification = "Reviewed")]
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(MTMADbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static)!;

        public MTMADbContext(DbContextOptions<MTMADbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Overrides the SaveChanges method to automatically apply audit info rules before saving changes.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        public override int SaveChanges() => this.SaveChanges(true);

        /// <summary>
        /// Overrides the SaveChanges method to automatically apply audit info rules before saving changes.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indicates whether all changes should be accepted on success.</param>
        /// <returns>The number of state entries written to the database.</returns>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// Overrides the SaveChangesAsync method to automatically apply audit info rules before saving changes asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>A Task representing the asynchronous save operation.</returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        /// <summary>
        /// Overrides the SaveChangesAsync method to automatically apply audit info rules before saving changes asynchronously.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indicates whether all changes should be accepted on success.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>A Task representing the asynchronous save operation.</returns>
        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Applies model configurations, query filters, and disable cascade delete during model creation.
        /// </summary>
        /// <param name="builder">The ModelBuilder instance to configure the model.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Call the base OnModelCreating to configure Identity models.
            base.OnModelCreating(builder);

            // Configure user identity relations.
            this.ConfigureUserIdentityRelations(builder);

            // Retrieve all entity types and set a global query filter for not deleted entities only.
            var entityTypes = builder.Model.GetEntityTypes().ToList();
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete for all foreign keys to restrict deletion behavior.
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        #region Helpers

        /// <summary>
        /// Sets a query filter to exclude deleted entities from the specified entity type.
        /// </summary>
        /// <typeparam name="T">The entity type implementing the IDeletableEntity interface.</typeparam>
        /// <param name="builder">The ModelBuilder instance to configure the model.</param>
        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        /// <summary>
        /// Applies audit info rules to entities that implement the IAuditInfo interface before saving changes.
        /// </summary>
        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }

        /// <summary>
        /// Applies user identity relations for the model using the provided ModelBuilder.
        /// </summary>
        /// <param name="builder">The ModelBuilder instance to configure the model.</param>
        private void ConfigureUserIdentityRelations(ModelBuilder builder)
            => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        #endregion
    }
}
