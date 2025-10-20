using IdentityApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Data
{
    public class IdentityApiContext(DbContextOptions<IdentityApiContext> options) : DbContext(options)
    {
        public DbSet<Identity> Identities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
