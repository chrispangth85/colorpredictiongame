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

                entity.Property(e => e.CcountryBuy)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("CCOUNTRY_BUY")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CcountryCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("CCOUNTRY_CODE");

                entity.Property(e => e.CcountryCreatedby)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CCOUNTRY_CREATEDBY");

                entity.Property(e => e.CcountryCreatedon)
                    .HasColumnType("datetime")
                    .HasColumnName("CCOUNTRY_CREATEDON")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CcountryDeletionstate)
                    .IsRequired()
                    .HasColumnName("CCOUNTRY_DELETIONSTATE")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.CcountryImagepath)
                    .HasMaxLength(500)
                    .HasColumnName("CCOUNTRY_IMAGEPATH");

                entity.Property(e => e.CcountryMobilecode)
                    .HasMaxLength(10)
                    .HasColumnName("CCOUNTRY_MOBILECODE");

                entity.Property(e => e.CcountryName)
                    .HasMaxLength(100)
                    .HasColumnName("CCOUNTRY_NAME")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CcountrySell)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("CCOUNTRY_SELL")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CcountryTaxLabel)
                    .HasMaxLength(100)
                    .HasColumnName("CCOUNTRY_TAX_LABEL");

                entity.Property(e => e.CcountryTaxValue)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CCOUNTRY_TAX_VALUE");

                entity.Property(e => e.CcountryUpdatedby)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CCOUNTRY_UPDATEDBY");

                entity.Property(e => e.CcountryUpdatedon)
                    .HasColumnType("datetime")
                    .HasColumnName("CCOUNTRY_UPDATEDON")
                    .HasDefaultValueSql("(getdate())");
            });
            
            OnModelCreatingPartial(modelBuilder);
        }
    
    }
}
