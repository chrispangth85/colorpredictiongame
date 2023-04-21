using System;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdExchangerate> CvdExchangerate { get; set; }

        protected void OnModelCreatingCvdExchangerateContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdExchangerate>(entity =>
            {
                entity.HasKey(e => e.CexchangeId);

                entity.ToTable("CVD_EXCHANGERATE");

                entity.Property(e => e.CexchangeId).HasColumnName("CEXCHANGE_ID");

                entity.Property(e => e.CexchangeBuy)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("CEXCHANGE_BUY");

                entity.Property(e => e.CexchangeCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CEXCHANGE_CODE");

                entity.Property(e => e.CexchangeDeletionstate)
                    .IsRequired()
                    .HasColumnName("CEXCHANGE_DELETIONSTATE")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.CexchangeSell)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CEXCHANGE_SELL");
            });
            
            OnModelCreatingPartial(modelBuilder);
        }
    
    }
}
