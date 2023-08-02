namespace AspNetCoreTemplate.Data.Common.Models
{
    using System;

    /// <summary>
    /// Entity that supports soft deletion.
    /// Soft deletion means the entity is not physically removed from the database but marked as deleted.
    /// </summary>
    public interface IDeletableEntity
    {
        /// <summary>
        /// Gets or sets a value indicating whether the entity is marked as deleted.
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets date and time when the entity was marked as deleted.
        /// </summary>
        DateTime? DeletedOn { get; set; }
    }
}
