using System;
using System.Collections.Generic;
using MasterPol.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterPol.Context;

public partial class KlimBaseContext : DbContext
{
    public KlimBaseContext()
    {
    }

    public KlimBaseContext(DbContextOptions<KlimBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<PartnerProductsImport> PartnerProductsImports { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<MasterPol.Models.Type> Types { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=89.110.53.87:5522; Database=klim_base; Username=klim; Password=nissan");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("partners_pk");

            entity.ToTable("partners", "MasterPol");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
            entity.Property(e => e.Director)
                .HasColumnType("character varying")
                .HasColumnName("director");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Inn)
                .HasColumnType("character varying")
                .HasColumnName("inn");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasColumnType("character varying")
                .HasColumnName("phone");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.RatingNavigation).WithMany(p => p.Partners)
                .HasForeignKey(d => d.Rating)
                .HasConstraintName("partners_rating_fk");

            entity.HasOne(d => d.Type).WithMany(p => p.Partners)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("partners_type_fk");
        });

        modelBuilder.Entity<PartnerProductsImport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("partner_products_import_pk");

            entity.ToTable("partner_products_import", "MasterPol");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DateOfRealization).HasColumnName("date_of_realization");
            entity.Property(e => e.PartnerName).HasColumnName("partner_name");
            entity.Property(e => e.Products).HasColumnName("products");
            entity.Property(e => e.ProductsCount)
                .HasColumnType("character varying")
                .HasColumnName("products_count");

            entity.HasOne(d => d.PartnerNameNavigation).WithMany(p => p.PartnerProductsImports)
                .HasForeignKey(d => d.PartnerName)
                .HasConstraintName("partner_products_import_partners_fk");

            entity.HasOne(d => d.ProductsNavigation).WithMany(p => p.PartnerProductsImports)
                .HasForeignKey(d => d.Products)
                .HasConstraintName("partner_products_import_products_fk");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("newtable_pk");

            entity.ToTable("products", "MasterPol");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ArticleNumber)
                .HasColumnType("character varying")
                .HasColumnName("article_number");
            entity.Property(e => e.MinimumCost).HasColumnName("minimum_cost");
            entity.Property(e => e.ProductName)
                .HasColumnType("character varying")
                .HasColumnName("product_name");
            entity.Property(e => e.ProductType).HasColumnName("product_type");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_type_pk");

            entity.ToTable("product_type", "MasterPol");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ProductTypeName)
                .HasColumnType("character varying")
                .HasColumnName("product_type_name");
            entity.Property(e => e.Ratio).HasColumnName("ratio");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rating_pk");

            entity.ToTable("rating", "MasterPol");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.RatingType)
                .HasColumnType("character varying")
                .HasColumnName("rating_type");
        });

        modelBuilder.Entity<MasterPol.Models.Type>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("type_pk");

            entity.ToTable("type", "MasterPol");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.TypeName)
                .HasColumnType("character varying")
                .HasColumnName("type_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
