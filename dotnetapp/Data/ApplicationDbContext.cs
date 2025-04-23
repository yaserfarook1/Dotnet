using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;

namespace dotnetapp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Represents the Users table
        public DbSet<User> Users { get; set; }

        // Represents the Transactions table
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the one-to-many relationship between User and Transaction
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User) // A Transaction belongs to one User
                .WithMany(u => u.Transactions) // A User can have many Transactions
                .HasForeignKey(t => t.UserId) // Foreign key is UserId
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete if User is deleted

            base.OnModelCreating(modelBuilder);
        }
    }
}