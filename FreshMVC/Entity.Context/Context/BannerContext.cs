using System;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdBanner> CvdBanner { get; set; }

        protected void OnModelCreatingCvdBannerContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdBanner>(entity =>
            {
                entity.HasKey(e => e.CbannerId);

                entity.ToTable("CVD_BANNER");

                entity.Property(e => e.CbannerId).HasColumnName("CBANNER_ID");

                entity.Property(e => e.CbannerCreatedby)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CBANNER_CREATEDBY")
                    .HasDefaultValueSql("('SYS')");

                entity.Property(e => e.CbannerCreatedon)
                    .HasColumnType("datetime")
                    .HasColumnName("CBANNER_CREATEDON")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CbannerDeletionstate).HasColumnName("CBANNER_DELETIONSTATE");

                entity.Property(e => e.CbannerImages)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .HasColumnName("CBANNER_IMAGES");

                entity.Property(e => e.CbannerTitle)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CBANNER_TITLE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

    }
}
