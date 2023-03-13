using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdParameter> CvdParameter { get; set; }

        protected void OnModelCreatingCvdParameterContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdParameter>(entity =>
            {
                entity.HasKey(e => e.CparaId);

                entity.ToTable("CVD_PARAMETER");

                entity.Property(e => e.CparaId).HasColumnName("CPARA_ID");

                entity.Property(e => e.CparaCreatedby)
                    .IsRequired()
                    .HasColumnName("CPARA_CREATEDBY")
                    .HasMaxLength(50);

                entity.Property(e => e.CparaCreatedon)
                    .HasColumnName("CPARA_CREATEDON")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CparaDecimalvalue)
                    .HasColumnName("CPARA_DECIMALVALUE")
                    .HasColumnType("decimal(15, 2)");

                entity.Property(e => e.CparaDeletionstate)
                    .IsRequired()
                    .HasColumnName("CPARA_DELETIONSTATE")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.CparaDescription)
                    .IsRequired()
                    .HasColumnName("CPARA_DESCRIPTION")
                    .HasMaxLength(100);

                entity.Property(e => e.CparaName)
                    .IsRequired()
                    .HasColumnName("CPARA_NAME")
                    .HasMaxLength(100);

                entity.Property(e => e.CparaStringvalue)
                    .IsRequired()
                    .HasColumnName("CPARA_STRINGVALUE")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CparaUom)
                    .IsRequired()
                    .HasColumnName("CPARA_UOM")
                    .HasMaxLength(100);

                entity.Property(e => e.CparaUpdatedby)
                    .IsRequired()
                    .HasColumnName("CPARA_UPDATEDBY")
                    .HasMaxLength(50);

                entity.Property(e => e.CparaUpdatedon)
                    .HasColumnName("CPARA_UPDATEDON")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

            });
            
            OnModelCreatingPartial(modelBuilder);
        }
    
    }
}
