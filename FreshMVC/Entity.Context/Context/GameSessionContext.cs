using System;
using Entity.Context;
using Entity.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdGameSession> CvdGameSession { get; set; }

        protected void OnModelCreatingCvdGameSessionContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdGameSession>(entity =>
            {
                entity.HasKey(e => e.CgamesesId);

                entity.ToTable("CVD_GAME_SESSION");

                entity.Property(e => e.CgamesesId).HasColumnName("CGAMESES_ID");

                entity.Property(e => e.CgameEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("CGAME_END");

                entity.Property(e => e.CgameId).HasColumnName("CGAME_ID");

                entity.Property(e => e.CgamePeriod)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CGAME_PERIOD");

                entity.Property(e => e.CgameResult).HasColumnName("CGAME_RESULT");

                entity.Property(e => e.CgameStart)
                    .HasColumnType("datetime")
                    .HasColumnName("CGAME_START");

                entity.Property(e => e.CgameStatus).HasColumnName("CGAME_STATUS");

                entity.Property(e => e.CgamesesDeletionstate).HasColumnName("CGAMESES_DELETIONSTATE");
            });

            OnModelCreatingPartial(modelBuilder);
        }
    
    }
}
