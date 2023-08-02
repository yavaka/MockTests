namespace AspNetCoreTemplate.Data.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Abstract base class for entities with a primary key of type TKey.
    /// It includes common audit information by implementing the IAuditInfo interface.
    /// </summary>
    /// <typeparam name="TKey">The data type of the entity's primary key.</typeparam>
    public abstract class BaseModel<TKey> : IAuditInfo
    {
        /// <summary>
        /// Gets or sets the primary key of the entity.
        /// </summary>
        [Key]
        required public TKey Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last modified.
        /// If the entity has not been modified, the value will be null.
        /// </summary>
        public DateTime? ModifiedOn { get; set; }
    }
}
