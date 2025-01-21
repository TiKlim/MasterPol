using System;
using System.Collections.Generic;
using Avtoservice.Models;
using Microsoft.EntityFrameworkCore;

namespace Avtoservice.Context;

public partial class KlimBaseContext : DbContext
{
    public KlimBaseContext()
    {
    }

    public KlimBaseContext(DbContextOptions<KlimBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=89.110.53.87:5522; Database=klim_base; Username=klim; Password=nissan");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.ManId).HasName("manufacturers_pk");

            entity.ToTable("manufacturers", "avtoservice");

            entity.Property(e => e.ManId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("man_id");
            entity.Property(e => e.ManName)
                .HasColumnType("character varying")
                .HasColumnName("man_name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("products_pk");

            entity.ToTable("products", "avtoservice");

            entity.Property(e => e.ProductId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("product_id");
            entity.Property(e => e.ProductArticule)
                .HasColumnType("character varying")
                .HasColumnName("product_articule");
            entity.Property(e => e.ProductCategory).HasColumnName("product_category");
            entity.Property(e => e.ProductCost).HasColumnName("product_cost");
            entity.Property(e => e.ProductCount)
                .HasColumnType("character varying")
                .HasColumnName("product_count");
            entity.Property(e => e.ProductDescription)
                .HasColumnType("character varying")
                .HasColumnName("product_description");
            entity.Property(e => e.ProductDiscountNow).HasColumnName("product_discount_now");
            entity.Property(e => e.ProductImage)
                .HasColumnType("character varying")
                .HasColumnName("product_image");
            entity.Property(e => e.ProductManufacturer).HasColumnName("product_manufacturer");
            entity.Property(e => e.ProductMaxDiscount).HasColumnName("product_max_discount");
            entity.Property(e => e.ProductName)
                .HasColumnType("character varying")
                .HasColumnName("product_name");
            entity.Property(e => e.ProductProvider).HasColumnName("product_provider");
            entity.Property(e => e.ProductUnit).HasColumnName("product_unit");

            entity.HasOne(d => d.ProductCategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_product_category_fk");

            entity.HasOne(d => d.ProductManufacturerNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductManufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_manufacturers_fk");

            entity.HasOne(d => d.ProductProviderNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductProvider)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_providers_fk");

            entity.HasOne(d => d.ProductUnitNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductUnit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_units_fk");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("product_category_pk");

            entity.ToTable("product_category", "avtoservice");

            entity.Property(e => e.CategoryId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasColumnType("character varying")
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.ProviderId).HasName("providers_pk");

            entity.ToTable("providers", "avtoservice");

            entity.Property(e => e.ProviderId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("provider_id");
            entity.Property(e => e.ProviderName)
                .HasColumnType("character varying")
                .HasColumnName("provider_name");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("units_pk");

            entity.ToTable("units", "avtoservice");

            entity.Property(e => e.UnitId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("unit_id");
            entity.Property(e => e.UnitName)
                .HasColumnType("character varying")
                .HasColumnName("unit_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
