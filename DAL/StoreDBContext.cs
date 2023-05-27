using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;

namespace DAL
{
    public class StoreDBContext : DbContext
    {
        public StoreDBContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Subcategory> Subcategories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<ProductAuditLog> ProductAuditLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add the shadow property to the model
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Product>
            //    (entry =>
            //{
            //    entry.ToTable("Products", tb => tb.HasTrigger("ProductAuditLogTrigger"));
            //});

           // modelBuilder.Entity<Product>().HasMany<ProductImage>(pi => pi.ProductImages);
        }
    }
}
