using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Semana10Sales.DOMAIN.Core.Entities;

namespace Semana10Sales.DOMAIN.Infrastructure.Data
{
    public partial class SalesContext : DbContext
    {
        public SalesContext()
        {
        }

        public SalesContext(DbContextOptions<SalesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; } = null!;
        public virtual DbSet<Order> Order { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItem { get; set; } = null!;
        public virtual DbSet<Product> Product { get; set; } = null!;
        public virtual DbSet<Supplier> Supplier { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=DESKTOP-S1DROK0\\SQLEXPRESS;Database=Sales;Trusted_Connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.City).HasMaxLength(40);

                entity.Property(e => e.Country).HasMaxLength(40);

                entity.Property(e => e.FirstName).HasMaxLength(40);

                entity.Property(e => e.LastName).HasMaxLength(40);

                entity.Property(e => e.Phone).HasMaxLength(20);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderNumber).HasMaxLength(10);

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(12, 2)")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDER_REFERENCE_CUSTOMER");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDERITE_REFERENCE_ORDER");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ORDERITE_REFERENCE_PRODUCT");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Package).HasMaxLength(30);

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(12, 2)")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRODUCT_REFERENCE_SUPPLIER");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.City).HasMaxLength(40);

                entity.Property(e => e.CompanyName).HasMaxLength(40);

                entity.Property(e => e.ContactName).HasMaxLength(50);

                entity.Property(e => e.ContactTitle).HasMaxLength(40);

                entity.Property(e => e.Country).HasMaxLength(40);

                entity.Property(e => e.Fax).HasMaxLength(30);

                entity.Property(e => e.Phone).HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
