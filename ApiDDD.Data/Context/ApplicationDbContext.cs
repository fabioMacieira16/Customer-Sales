using Microsoft.EntityFrameworkCore;
using SalesAPI.Domain.Entities;
using SalesAPI.Domain.ValueObjects;

namespace ApiDDD.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<ProductInfo> ProductInfos { get; set; }
        public DbSet<CustomerInfo> CustomerInfos { get; set; }
        public DbSet<BranchInfo> BranchInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.SaleNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.SaleDate).IsRequired();
                entity.Property(e => e.IsCancelled).IsRequired();
                entity.Property(e => e.TotalAmount).HasPrecision(10, 2);

                entity.OwnsOne(e => e.Customer, customer =>
                {
                    customer.Property(c => c.Name).IsRequired().HasMaxLength(100);
                    customer.Property(c => c.Email).HasMaxLength(100);
                    customer.Property(c => c.Phone).HasMaxLength(20);
                });

                entity.OwnsOne(e => e.Branch, branch =>
                {
                    branch.Property(b => b.Name).IsRequired().HasMaxLength(100);
                    branch.Property(b => b.Address).HasMaxLength(200);
                    branch.Property(b => b.City).HasMaxLength(50);
                    branch.Property(b => b.State).HasMaxLength(50);
                });

                entity.HasMany(e => e.Items)
                      .WithOne(e => e.Sales)
                      .HasForeignKey(e => e.SaleId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SaleItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UnitPrice).HasPrecision(10, 2);
                entity.Property(e => e.TotalAmount).HasPrecision(10, 2);
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.Discount).HasPrecision(4, 2);

                entity.OwnsOne(e => e.Product, product =>
                {
                    product.Property(p => p.Id).IsRequired();
                    product.Property(p => p.Name).IsRequired().HasMaxLength(100);
                });
            });
        }
    }
}