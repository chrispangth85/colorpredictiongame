using System;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdBank> CvdBank { get; set; }

        protected void OnModelCreatingCvdBankContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdBank>(entity =>
            {
                entity.HasKey(e => e.CbankId);

                entity.ToTable("CVD_BANK");

                entity.Property(e => e.CbankId).HasColumnName("CBANK_ID");

                entity.Property(e => e.CbankCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CBANK_CODE");

                entity.Property(e => e.CbankDeletionstate)
                    .IsRequired()
                    .HasColumnName("CBANK_DELETIONSTATE")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.CbankName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CBANK_NAME");

                entity.Property(e => e.CcountryCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("CCOUNTRY_CODE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

    }
}
