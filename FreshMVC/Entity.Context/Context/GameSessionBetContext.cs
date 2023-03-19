using System;
using Entity.Context;
using Entity.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdGameSessionBet> CvdGameSessionBet { get; set; }

        protected void OnModelCreatingCvdGameSessionBetContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdGameSessionBet>(entity =>
            {
                entity.HasKey(e => e.CbetId);

                entity.ToTable("CVD_GAME_SESSION_BET");

                entity.Property(e => e.CbetId).HasColumnName("CBET_ID");

                entity.Property(e => e.CbetDeletionstate).HasColumnName("CBET_DELETIONSTATE");

                entity.Property(e => e.CgameAmount)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CGAME_AMOUNT");

                entity.Property(e => e.CgameId)
                    .HasColumnName("CGAME_ID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CgameNumber).HasColumnName("CGAME_NUMBER");

                entity.Property(e => e.CgameWinAmount)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CGAME_WIN_AMOUNT");

                entity.Property(e => e.CgamesesId).HasColumnName("CGAMESES_ID");

                entity.Property(e => e.CusrUsername)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CUSR_USERNAME");
            });

            OnModelCreatingPartial(modelBuilder);
        }
    
    }
}
