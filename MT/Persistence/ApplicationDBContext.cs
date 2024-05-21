using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> opt) : base(opt) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Group> Groups => Set<Group>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(_ => !_.IsDeleted && _.IsActive);
            modelBuilder.Entity<Group>().HasQueryFilter(_ => !_.IsDeleted && _.IsActive);
        }
    }
}
