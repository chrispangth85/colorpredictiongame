using System;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdReceipt> CvdReceipt { get; set; }

        protected void OnModelCreatingCvdReceiptContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdReceipt>(entity =>
            {
                entity.HasKey(e => e.CrecId);

                entity.ToTable("CVD_RECEIPT");

                entity.Property(e => e.CrecId).HasColumnName("CREC_ID");

                entity.Property(e => e.CrecCategory)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CREC_CATEGORY");

                entity.Property(e => e.CrecCreatedon)
                    .HasColumnType("datetime")
                    .HasColumnName("CREC_CREATEDON");

                entity.Property(e => e.CrecDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CREC_DATE");

                entity.Property(e => e.CrecDeletionstate).HasColumnName("CREC_DELETIONSTATE");

                entity.Property(e => e.CrecFolder)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CREC_FOLDER");

                entity.Property(e => e.CrecImagepath)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("CREC_IMAGEPATH");

                entity.Property(e => e.CusrUsername)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CUSR_USERNAME");
            });

            OnModelCreatingPartial(modelBuilder);
        }
    
    }
}
