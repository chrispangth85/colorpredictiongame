using System;
using Entity.Context;
using Entity.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdDepositUsdt> CvdDepositUsdt { get; set; }

        protected void OnModelCreatingCvdDepositUSDTContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdDepositUsdt>(entity =>
            {
                entity.HasKey(e => e.CdepoId);

                entity.ToTable("CVD_DEPOSIT_USDT");

                entity.Property(e => e.CdepoId).HasColumnName("CDEPO_ID");

                entity.Property(e => e.CdepoAmount)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CDEPO_AMOUNT");

                entity.Property(e => e.CdepoCreatedon)
                    .HasColumnType("datetime")
                    .HasColumnName("CDEPO_CREATEDON");

                entity.Property(e => e.CdepoImagepath)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("CDEPO_IMAGEPATH");

                entity.Property(e => e.CdepoStatus).HasColumnName("CDEPO_STATUS");

                entity.Property(e => e.CdepoTransactionId)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CDEPO_TRANSACTION_ID");

                entity.Property(e => e.CdepoUpdatedby)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CDEPO_UPDATEDBY")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CdepoWalletAddress)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CDEPO_WALLET_ADDRESS");

                entity.Property(e => e.CdepoWalletCompanyAddress)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CDEPO_WALLET_COMPANY_ADDRESS");

                entity.Property(e => e.CdepoWalletCompanyNetwork)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CDEPO_WALLET_COMPANY_NETWORK");

                entity.Property(e => e.CdepoWalletNetwork)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CDEPO_WALLET_NETWORK");

                entity.Property(e => e.CusrUsername)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("CUSR_USERNAME");
            });

            OnModelCreatingPartial(modelBuilder);
        }

    }
}
