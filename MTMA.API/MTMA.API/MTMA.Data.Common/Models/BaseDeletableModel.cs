namespace MTMA.Data.Common.Models
{
    using System;

    /// <summary>
    /// Abstract base class for entities that support soft deletion.
    /// It inherits from the BaseModel class and implements the IDeletableEntity interface.
    /// </summary>
    /// <typeparam name="TKey">The data type of the entity's primary key.</typeparam>
    public abstract class BaseDeletableModel<TKey> : BaseModel<TKey>, IDeletableEntity
    {
        /// <summary>
        /// Gets or sets a value indicating whether the entity is marked as deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was marked as deleted.
        /// If the entity is not deleted, the value will be null.
        /// </summary>
        public DateTime? DeletedOn { get; set; }
    }
}
