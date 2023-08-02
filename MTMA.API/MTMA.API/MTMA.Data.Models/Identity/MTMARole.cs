namespace MTMA.Data.Models.Identity
{
    using Microsoft.AspNetCore.Identity;
    using MTMA.Data.Common.Models;

    /// <summary>
    /// Represents an application role in the system, extending the IdentityRole class.
    /// Implements the IAuditInfo and IDeletableEntity interfaces to track audit information and soft deletion status.
    /// </summary>
    public class MTMARole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public MTMARole()
            : this(default!)
        {
        }

        public MTMARole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
