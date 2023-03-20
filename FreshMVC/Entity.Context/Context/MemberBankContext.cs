using System;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdMemberBank> CvdMemberBank { get; set; }

        protected void OnModelCreatingCvdMemberBankContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdMemberBank>(entity =>
            {
                entity.HasKey(e => e.CbankId)
                  .HasName("PK_CVD_MEMBER_BANKS");

                entity.ToTable("CVD_MEMBER_BANK");

                entity.Property(e => e.CbankId).HasColumnName("CBANK_ID");

                entity.Property(e => e.CbankAddress)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .HasColumnName("CBANK_ADDRESS");

                entity.Property(e => e.CbankBankaccount)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("CBANK_BANKACCOUNT");

                entity.Property(e => e.CbankBankaccountname)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("CBANK_BANKACCOUNTNAME");

                entity.Property(e => e.CbankCity)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("CBANK_CITY");

                entity.Property(e => e.CbankCreatedby)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("CBANK_CREATEDBY");

                entity.Property(e => e.CbankCreatedon)
                    .HasColumnType("datetime")
                    .HasColumnName("CBANK_CREATEDON");

                entity.Property(e => e.CbankDeletionstate).HasColumnName("CBANK_DELETIONSTATE");

                entity.Property(e => e.CbankEmail)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("CBANK_EMAIL")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CbankIfsccode)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("CBANK_IFSCCODE");

                entity.Property(e => e.CbankMobile)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("CBANK_MOBILE");

                entity.Property(e => e.CbankName)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("CBANK_NAME");

                entity.Property(e => e.CbankState)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("CBANK_STATE")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CusrUsername)
                    .HasMaxLength(200)
                    .HasColumnName("CUSR_USERNAME");
            });
            
            OnModelCreatingPartial(modelBuilder);
        }
    
    }
}
