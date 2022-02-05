using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bill.Models
{
    public partial class billContext : DbContext
    {
        public billContext()
        {
        }

        public billContext(DbContextOptions<billContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Gst> Gst { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source= ARATHIR\\SQLEXPRESS; Initial Catalog=bill; Integrated security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .HasColumnName("category_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Gst>(entity =>
            {
                entity.ToTable("gst");

                entity.Property(e => e.GstId).HasColumnName("gst_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.GstValue).HasColumnName("gst_value");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Gst)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__gst__category_id__3A81B327");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductCode)
                    .HasName("PK__product__AE1A8CC5FF080D23");

                entity.ToTable("product");

                entity.Property(e => e.ProductCode)
                    .HasColumnName("product_code")
                    .ValueGeneratedNever();

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.ProductDescription)
                    .HasColumnName("product_description")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.RatePerUnit).HasColumnName("rate_per_unit");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__product__categor__38996AB5");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
