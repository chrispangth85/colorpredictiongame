using System;
using Entity.Context;
using Entity.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdUser> CvdUser { get; set; }

        protected void OnModelCreatingCvdUserContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdUser>(entity =>
            {
                entity.HasKey(e => e.CusrId);

                entity.ToTable("CVD_USER");

                entity.Property(e => e.CusrId).HasColumnName("CUSR_ID");

                entity.Property(e => e.CcountryId).HasColumnName("CCOUNTRY_ID");

                entity.Property(e => e.CroleId).HasColumnName("CROLE_ID");

                entity.Property(e => e.CusrCashwlt)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CUSR_CASHWLT")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CusrCreatedby)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CUSR_CREATEDBY")
                    .HasDefaultValueSql("('SYS')");

                entity.Property(e => e.CusrCreatedon)
                    .HasColumnType("datetime")
                    .HasColumnName("CUSR_CREATEDON")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CusrDeletionstate).HasColumnName("CUSR_DELETIONSTATE");

                entity.Property(e => e.CusrEmail)
                    .HasMaxLength(100)
                    .HasColumnName("CUSR_EMAIL")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CusrFirstname)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("CUSR_FIRSTNAME")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CusrImagepath)
                    .HasMaxLength(2000)
                    .HasColumnName("CUSR_IMAGEPATH")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CusrLastname)
                    .HasMaxLength(60)
                    .HasColumnName("CUSR_LASTNAME")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CusrPassword)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CUSR_PASSWORD");

                entity.Property(e => e.CusrPin)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CUSR_PIN");

                entity.Property(e => e.CusrRechargewlt)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CUSR_RECHARGEWLT")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CusrRedpacketwlt)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CUSR_REDPACKETWLT")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CusrReferralCode)
                    .HasMaxLength(10)
                    .HasColumnName("CUSR_REFERRAL_CODE")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CusrReferralid)
                    .HasMaxLength(100)
                    .HasColumnName("CUSR_REFERRALID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CusrUpdatedby)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CUSR_UPDATEDBY")
                    .HasDefaultValueSql("('SYS')");

                entity.Property(e => e.CusrUpdatedon)
                    .HasColumnType("datetime")
                    .HasColumnName("CUSR_UPDATEDON")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CusrUsername)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CUSR_USERNAME");

                entity.Property(e => e.MemberDownlineTotalBet)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("MEMBER_DOWNLINE_TOTAL_BET")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MemberDownlineTotalCommission)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("MEMBER_DOWNLINE_TOTAL_COMMISSION")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MemberDownlineTotalRecharge)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("MEMBER_DOWNLINE_TOTAL_RECHARGE")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MemberDownlineTotalWin)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("MEMBER_DOWNLINE_TOTAL_WIN")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MemberDownlineTotalWithdrawal)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("MEMBER_DOWNLINE_TOTAL_WITHDRAWAL")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MemberLevel2Intro)
                    .HasMaxLength(200)
                    .HasColumnName("MEMBER_LEVEL2_INTRO")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MemberLevel3Intro)
                    .HasMaxLength(200)
                    .HasColumnName("MEMBER_LEVEL3_INTRO")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MemberLevel4Intro)
                    .HasMaxLength(200)
                    .HasColumnName("MEMBER_LEVEL4_INTRO")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MemberLevel5Intro)
                    .HasMaxLength(200)
                    .HasColumnName("MEMBER_LEVEL5_INTRO")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MemberTotalDownline)
                    .HasColumnName("MEMBER_TOTAL_DOWNLINE")
                    .HasDefaultValueSql("((0))");
            });

            OnModelCreatingPartial(modelBuilder);
        }
    
    }
}
