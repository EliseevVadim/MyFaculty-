using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Identity.Data.Configurations;
using MyFaculty.Identity.Models;

namespace MyFaculty.Identity.Data
{
    public class AuthDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>(entity => entity.ToTable("Users"));
            builder.Entity<IdentityRole<int>>(entity => entity.ToTable("Roles"));
            builder.Entity<IdentityUserRole<int>>(entity => entity.ToTable("UserRoles"));
            builder.Entity<IdentityUserClaim<int>>(entity => entity.ToTable("UserClaims"));
            builder.Entity<IdentityUserLogin<int>>(entity => entity.ToTable("UserLogins"));
            builder.Entity<IdentityUserToken<int>>(entity => entity.ToTable("UserTokens"));
            builder.Entity<IdentityRoleClaim<int>>(entity => entity.ToTable("RoleClaims"));
            builder.ApplyConfiguration(new AppUserConfiguration());
        }
    }
}
