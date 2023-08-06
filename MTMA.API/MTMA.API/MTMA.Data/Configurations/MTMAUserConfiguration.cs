namespace MTMA.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MTMA.Data.Models.Identity;

    /// <summary>
    /// Configures the entity mapping for the MTMAUser class using the Entity Framework IEntityTypeConfiguration interface.
    /// This class is responsible for defining the relationships between MTMAUser and other related entities.
    /// </summary>
    public class MTMAUserConfiguration : IEntityTypeConfiguration<MTMAUser>
    {
        /// <summary>
        /// Configures the entity mapping for the MTMAUser class.
        /// </summary>
        /// <param name="appUser">The EntityTypeBuilder for the MTMAUser entity.</param>
        public void Configure(EntityTypeBuilder<MTMAUser> appUser)
        {
            // Configure the relationship between MTMAUser and IdentityUserClaim.
            appUser
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between MTMAUser and IdentityUserLogin.
            appUser
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between MTMAUser and IdentityUserRole.
            appUser
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
