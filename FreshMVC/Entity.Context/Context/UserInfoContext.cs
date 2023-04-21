using System;
using Entity.Context;
using Entity.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdUserInfo> CvdUserInfo { get; set; }

        protected void OnModelCreatingCvdUserInfoContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdUserInfo>(entity =>
            {
                entity.HasKey(e => e.CusrId);

                entity.ToTable("CVD_USER_INFO");

                entity.Property(e => e.CusrId).HasColumnName("CUSR_ID");

                entity.Property(e => e.CusrUsername)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CUSR_USERNAME");

                entity.Property(e => e.MemberWalletAddress)
                    .HasMaxLength(200)
                    .HasColumnName("MEMBER_WALLET_ADDRESS");

                entity.Property(e => e.MemberWalletNetwork)
                    .HasMaxLength(200)
                    .HasColumnName("MEMBER_WALLET_NETWORK");
            });

            OnModelCreatingPartial(modelBuilder);
        }
    
    }
}
