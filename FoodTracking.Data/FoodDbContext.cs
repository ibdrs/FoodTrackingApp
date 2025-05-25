using FoodTracking.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodTracking.Data
{
    public class FoodDbContext(DbContextOptions<FoodDbContext> options) : DbContext(options)
    {
        public DbSet<Food> Foods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Food>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Description);
                entity.Property(e => e.Calories).HasPrecision(10, 2);
                entity.Property(e => e.DateAdded).IsRequired();
            });
        }
    }
} 