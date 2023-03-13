using System;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdCountry> CvdCountry { get; set; }

        protected void OnModelCreatingCvdCountryContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdCountry>(entity =>
            {
                entity.HasKey(e => e.CcountryId);

                entity.ToTable("CVD_COUNTRY");

                entity.Property(e => e.CcountryId).HasColumnName("CCOUNTRY_ID");

                entity.Property(e => e.CcountryCode)
                    .IsRequired()
                    .HasColumnName("CCOUNTRY_CODE")
                    .HasMaxLength(10);

                entity.Property(e => e.CcountryCreatedby)
                    .IsRequired()
                    .HasColumnName("CCOUNTRY_CREATEDBY")
                    .HasMaxLength(50);

                entity.Property(e => e.CcountryCreatedon)
                    .HasColumnName("CCOUNTRY_CREATEDON")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CcountryDeletionstate)
                    .IsRequired()
                    .HasColumnName("CCOUNTRY_DELETIONSTATE")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.CcountryImagepath)
                    .HasColumnName("CCOUNTRY_IMAGEPATH")
                    .HasMaxLength(500);

                entity.Property(e => e.CcountryMobilecode)
                    .HasColumnName("CCOUNTRY_MOBILECODE")
                    .HasMaxLength(10);

                entity.Property(e => e.CcountryTaxLabel)
                    .HasColumnName("CCOUNTRY_TAX_LABEL")
                    .HasMaxLength(100);

                entity.Property(e => e.CcountryTaxValue)
                    .HasColumnName("CCOUNTRY_TAX_VALUE")
                    .HasColumnType("decimal(15, 2)");

                entity.Property(e => e.CcountryUpdatedby)
                    .IsRequired()
                    .HasColumnName("CCOUNTRY_UPDATEDBY")
                    .HasMaxLength(50);

                entity.Property(e => e.CcountryUpdatedon)
                    .HasColumnName("CCOUNTRY_UPDATEDON")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });
            
            OnModelCreatingPartial(modelBuilder);
        }
    
    }
}
