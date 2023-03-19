using System;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdCashwalletlogtemp> CvdCashwalletlogtemp { get; set; }

        protected void OnModelCreatingCvdCashwalletlogtempContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdCashwalletlogtemp>(entity =>
            {
                entity.HasKey(e => e.CcashId);

                entity.ToTable("CVD_CASHWALLETLOGTEMP");

                entity.Property(e => e.CcashId).HasColumnName("CCASH_ID");

                entity.Property(e => e.CcashAppnumber)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CCASH_APPNUMBER");

                entity.Property(e => e.CcashAppother)
                    .HasMaxLength(200)
                    .HasColumnName("CCASH_APPOTHER");

                entity.Property(e => e.CcashApprate)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CCASH_APPRATE");

                entity.Property(e => e.CcashAppuser)
                    .HasMaxLength(200)
                    .HasColumnName("CCASH_APPUSER");

                entity.Property(e => e.CcashCashin)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CCASH_CASHIN");

                entity.Property(e => e.CcashCashname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("CCASH_CASHNAME");

                entity.Property(e => e.CcashCashout)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CCASH_CASHOUT");

                entity.Property(e => e.CcashCreatedby)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CCASH_CREATEDBY")
                    .HasDefaultValueSql("('SYS')");

                entity.Property(e => e.CcashCreatedon)
                    .HasColumnType("datetime")
                    .HasColumnName("CCASH_CREATEDON")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CcashDeletionstate).HasColumnName("CCASH_DELETIONSTATE");

                entity.Property(e => e.CcashStatus)
                    .HasColumnName("CCASH_STATUS")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CcashWallet)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CCASH_WALLET");

                entity.Property(e => e.CusrUsername)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CUSR_USERNAME");
            });
            
            OnModelCreatingPartial(modelBuilder);
        }
    
    }
}
