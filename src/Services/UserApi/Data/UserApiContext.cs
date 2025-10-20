using Microsoft.EntityFrameworkCore;

namespace UserApi.Data
{
    public class UserApiContext(DbContextOptions<UserApiContext> options) : DbContext(options)
    {
        public DbSet<UserApi.Entities.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
