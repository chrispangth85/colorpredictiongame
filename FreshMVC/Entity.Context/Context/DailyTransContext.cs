using System;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdDailytrans> CvdDailytrans { get; set; }

        protected void OnModelCreatingCvdDailytransContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdDailytrans>(entity =>
            {
                entity.HasKey(e => e.CdailId);

                entity.ToTable("CVD_DAILYTRANS");

                entity.Property(e => e.CdailId).HasColumnName("CDAIL_ID");

                entity.Property(e => e.CdailBet)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CDAIL_BET");

                entity.Property(e => e.CdailRecharge)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CDAIL_RECHARGE");

                entity.Property(e => e.CdailTrandate)
                    .HasColumnType("datetime")
                    .HasColumnName("CDAIL_TRANDATE");

                entity.Property(e => e.CdailWin)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CDAIL_WIN");

                entity.Property(e => e.CdailWithdraw)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CDAIL_WITHDRAW");
            });
            
            OnModelCreatingPartial(modelBuilder);
        }
    
    }
}
