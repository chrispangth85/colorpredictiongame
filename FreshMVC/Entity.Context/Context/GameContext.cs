using System;
using Entity.Context;
using Entity.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdGame> CvdGame { get; set; }

        protected void OnModelCreatingCvdGameContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdGame>(entity =>
            {
                entity.HasKey(e => e.CgameId);

                entity.ToTable("CVD_GAME");

                entity.Property(e => e.CgameId).HasColumnName("CGAME_ID");

                entity.Property(e => e.CgameDeletionstate).HasColumnName("CGAME_DELETIONSTATE");

                entity.Property(e => e.CgameDurationSeconds).HasColumnName("CGAME_DURATION_SECONDS");

                entity.Property(e => e.CgameGreen5Return)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CGAME_GREEN5_RETURN");

                entity.Property(e => e.CgameGreenReturn)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CGAME_GREEN_RETURN");

                entity.Property(e => e.CgameImagepath)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("CGAME_IMAGEPATH");

                entity.Property(e => e.CgameName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CGAME_NAME");

                entity.Property(e => e.CgameNo0Return)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CGAME_NO0_RETURN");

                entity.Property(e => e.CgameNo1Return)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CGAME_NO1_RETURN");

                entity.Property(e => e.CgameNo2Return)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CGAME_NO2_RETURN");

                entity.Property(e => e.CgameNo3Return)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CGAME_NO3_RETURN");

                entity.Property(e => e.CgameNo4Return)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CGAME_NO4_RETURN");

                entity.Property(e => e.CgameNo5Return)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CGAME_NO5_RETURN");

                entity.Property(e => e.CgameNo6Return)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CGAME_NO6_RETURN");

                entity.Property(e => e.CgameNo7Return)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CGAME_NO7_RETURN");

                entity.Property(e => e.CgameNo8Return)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CGAME_NO8_RETURN");

                entity.Property(e => e.CgameNo9Return)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CGAME_NO9_RETURN");

                entity.Property(e => e.CgameRed0Return)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CGAME_RED0_RETURN");

                entity.Property(e => e.CgameRedReturn)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CGAME_RED_RETURN");

                entity.Property(e => e.CgameStatus).HasColumnName("CGAME_STATUS");

                entity.Property(e => e.CgameType).HasColumnName("CGAME_TYPE");

                entity.Property(e => e.CgameVioletReturn)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CGAME_VIOLET_RETURN");
            });

            OnModelCreatingPartial(modelBuilder);
        }
    
    }
}
