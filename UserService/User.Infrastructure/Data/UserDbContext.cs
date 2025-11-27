using Microsoft.EntityFrameworkCore;
using UserEntity = User.Core.Entities.User;
using RoleEntity = User.Core.Entities.Role;
using ProfileEntity = User.Core.Entities.Profile;

namespace User.Infrastructure.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> opts) : base(opts) { }

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<RoleEntity> Roles => Set<RoleEntity>();
        public DbSet<ProfileEntity> Profiles => Set<ProfileEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>().HasIndex(u => u.Email).IsUnique();
        }
    }
}
