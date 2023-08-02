namespace AspNetCoreTemplate.Data.Common.Models
{
    using System;

    /// <summary>
    /// Interface representing common audit information for entities.
    /// It tracks the creation and modification date and time of an entity.
    /// </summary>
    public interface IAuditInfo
    {
        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the entity was last modified.
        /// If the entity has not been modified, the value will be null.
        /// </summary>
        DateTime? ModifiedOn { get; set; }
    }
}
