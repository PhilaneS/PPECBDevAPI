using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProductCodeSequence> ProductCodeSequences { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure entity relationships and constraints here if needed
            // User entity configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Email).IsRequired();
                entity.Property(u => u.Password).IsRequired();
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(c => c.CategoryCode).IsUnique().HasDatabaseName("IX_Category_Code"); ;
                entity.HasKey(c => c.CategoryId);
                entity.Property(c => c.Name).IsRequired();
                entity.Property(c => c.CategoryCode).IsRequired();
                entity.Property(c => c.IsActive).IsRequired();

                // Category -> User (many categories belong to one user)
                entity.HasOne(c => c.User)
                      .WithMany()
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.ProductCode).IsRequired();
                entity.Property(p => p.Name).IsRequired();
                //entity.Property(p => p.CategoryName).IsRequired();
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");

                // Product -> User (many products belong to one user)
                entity.HasOne(p => p.User)
                      .WithMany()
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Product -> Category (many products belong to one category)
                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductCodeSequence>(entity =>
            {
                entity.HasKey(pcs => pcs.Id);
                entity.Property(pcs => pcs.LastNumber).IsRequired();
            });
        }
    }
}
