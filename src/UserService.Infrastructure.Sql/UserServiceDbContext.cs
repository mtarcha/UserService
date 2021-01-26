using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Infrastructure.Sql.Entities;
using User = UserService.Infrastructure.Sql.Entities.User;

namespace UserService.Infrastructure.Sql
{
    public class UserServiceDbContext : DbContext
    {
        public UserServiceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserEncryptionKeys> UserEncryptionKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users").HasKey(p => p.Id);
            modelBuilder.Entity<User>().HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<User>().Property(p => p.IsEmailVerified).IsRequired();
            modelBuilder.Entity<User>().Property(p => p.UpdatedAt).IsRequired();

            modelBuilder.Entity<User>()
                .HasOne<UserEncryptionKeys>()
                .WithOne(x => x.User)
                .HasForeignKey<UserEncryptionKeys>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserEncryptionKeys>().ToTable("UserEncryptionKeys").HasKey(p => p.Id);
            modelBuilder.Entity<UserEncryptionKeys>().Property(x => x.EncryptionKey).IsRequired();
        }
    }
}