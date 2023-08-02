namespace MTMA.Data.Models.Identity
{
    using Microsoft.AspNetCore.Identity;
    using MTMA.Data.Common.Models;

    /// <summary>
    /// Represents an application user in the system, extending the IdentityUser class.
    /// Implements the IAuditInfo and IDeletableEntity interfaces to track audit information and soft deletion status.
    /// </summary>
    public class MTMAUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public MTMAUser() => this.Id = Guid.NewGuid().ToString();

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        /// <summary>
        /// Gets or sets the collection of user roles associated with the user.
        /// </summary>
        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; } = new HashSet<IdentityUserRole<string>>();

        /// <summary>
        /// Gets or sets the collection of claims associated with the user.
        /// </summary>
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; } = new HashSet<IdentityUserClaim<string>>();

        /// <summary>
        /// Gets or sets the collection of external logins associated with the user.
        /// </summary>
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; } = new HashSet<IdentityUserLogin<string>>();
    }
}
