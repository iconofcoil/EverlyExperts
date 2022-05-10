using Duende.IdentityServer.EntityFramework.Options;
using EverlyExperts.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EverlyExperts.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Friend> Friends { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Member>()
                   .HasKey(x => x.Id)
                   .HasName("PK_Members");

            builder.Entity<Friend>()
                   .HasKey(x => new { x.MemberId, x.FriendId })
                   .HasName("PK_Friends");
        }
    }
}