using System;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdPaymentinfo> CvdPaymentinfo { get; set; }

        protected void OnModelCreatingCvdPaymentinfoContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdPaymentinfo>(entity =>
            {
                entity.HasKey(e => e.CpaymentId);

                entity.ToTable("CVD_PAYMENTINFO");

                entity.Property(e => e.CpaymentId).HasColumnName("CPAYMENT_ID");

                entity.Property(e => e.CpaymentAmount)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("CPAYMENT_AMOUNT");

                entity.Property(e => e.CpaymentCreatedon)
                    .HasColumnType("datetime")
                    .HasColumnName("CPAYMENT_CREATEDON")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CpaymentIssuebank)
                    .HasMaxLength(500)
                    .HasColumnName("CPAYMENT_ISSUEBANK");

                entity.Property(e => e.CpaymentReturncode)
                    .HasMaxLength(500)
                    .HasColumnName("CPAYMENT_RETURNCODE");

                entity.Property(e => e.CpaymentReturnsign)
                    .HasMaxLength(500)
                    .HasColumnName("CPAYMENT_RETURNSIGN");

                entity.Property(e => e.CpaymentTransid)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("CPAYMENT_TRANSID");

                entity.Property(e => e.CpaymentTransmsg)
                    .HasMaxLength(500)
                    .HasColumnName("CPAYMENT_TRANSMSG");

                entity.Property(e => e.CpaymentTranspaymentid)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("CPAYMENT_TRANSPAYMENTID");

                entity.Property(e => e.CusrUsername)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("CUSR_USERNAME");
            });

            OnModelCreatingPartial(modelBuilder);
        }

    }
}
