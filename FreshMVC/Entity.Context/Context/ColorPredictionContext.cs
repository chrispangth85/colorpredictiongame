using System;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdProduct> CvdProduct { get; set; }

        protected void OnModelCreatingCvdProductContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdProduct>(entity =>
            {
                entity.HasKey(e => e.CproId);

                entity.ToTable("CVD_PRODUCT");

                entity.Property(e => e.CproId).HasColumnName("CPRO_ID");

                entity.Property(e => e.CproCreatedby)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CPRO_CREATEDBY")
                    .HasDefaultValueSql("('SYS')");

                entity.Property(e => e.CproCreatedon)
                    .HasColumnType("datetime")
                    .HasColumnName("CPRO_CREATEDON")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CproDeletionstate).HasColumnName("CPRO_DELETIONSTATE");

                entity.Property(e => e.CproDesc).HasColumnName("CPRO_DESC");

                entity.Property(e => e.CproDescAdd)
                    .IsRequired()
                    .HasColumnName("CPRO_DESC_ADD");

                entity.Property(e => e.CproImages)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .HasColumnName("CPRO_IMAGES");

                entity.Property(e => e.CproPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("CPRO_PRICE");

                entity.Property(e => e.CproTitle)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CPRO_TITLE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

    }
}
