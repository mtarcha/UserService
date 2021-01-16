using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Sql
{
    public class UserServiceDbContext : DbContext
    {
        public UserServiceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users").HasKey(p => p.Id);
            modelBuilder.Entity<User>().HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<User>().Property(p => p.IsEmailVerified).IsRequired();
        }
    }
}