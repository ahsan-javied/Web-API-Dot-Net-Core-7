using Microsoft.EntityFrameworkCore;
using Models.Domain.Entites;
using System.Reflection.Metadata;

namespace DAL.DBCore
{
    public class CoreDBContext : DbContext
    {
        public CoreDBContext(DbContextOptions<CoreDBContext> options) : base(options)
        {
            base.ChangeTracker.AutoDetectChangesEnabled = true;
        }

        public DbSet<RegisterUser> RegisterUsers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<RegisterUser>().HasIndex(u => u.Username).IsUnique();

            //modelBuilder.Entity<User>()
            //    .HasOne(u => u.RegisterUser)
            //    .WithOne(l => l.User)
            //    .HasForeignKey<RegisterUser>(l => l.RegisterUserId).IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
