using Microsoft.EntityFrameworkCore;
using TestTaskRT.Shared.Dbo.Entities;

namespace TestTaskRT.Shared
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasOne(p => p.Department)
                .WithMany(t => t.Users)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }
    }
}