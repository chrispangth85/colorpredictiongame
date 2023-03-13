using System;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdOtp> CvdOtp { get; set; }

        protected void OnModelCreatingCvdOtpContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdOtp>(entity =>
            {
                entity.HasKey(e => e.CotpId);

                entity.ToTable("CVD_OTP");

                entity.Property(e => e.CotpId).HasColumnName("COTP_ID");

                entity.Property(e => e.CotpCode)
                    .IsRequired()
                    .HasColumnName("COTP_CODE")
                    .HasMaxLength(10);

                entity.Property(e => e.CotpCreatedby)
                    .IsRequired()
                    .HasColumnName("COTP_CREATEDBY")
                    .HasMaxLength(50);

                entity.Property(e => e.CotpCreatedon)
                    .HasColumnName("COTP_CREATEDON")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CotpDeletionstate)
                    .IsRequired()
                    .HasColumnName("COTP_DELETIONSTATE")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.CotpUpdatedby)
                    .IsRequired()
                    .HasColumnName("COTP_UPDATEDBY")
                    .HasMaxLength(50);

                entity.Property(e => e.CotpUpdatedon)
                    .HasColumnName("COTP_UPDATEDON")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CusrUsername)
                    .IsRequired()
                    .HasColumnName("CUSR_USERNAME")
                    .HasMaxLength(200);
            });
            
            OnModelCreatingPartial(modelBuilder);
        }
    
    }
}
