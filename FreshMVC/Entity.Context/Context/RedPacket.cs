using System;
using Entity.Context;
using Entity.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdRedPacket> CvdRedPacket { get; set; }

        protected void OnModelCreatingCvdRedPacketContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdRedPacket>(entity =>
            {
                entity.HasKey(e => e.CredpId);

                entity.ToTable("CVD_RED_PACKET");

                entity.Property(e => e.CredpId).HasColumnName("CREDP_ID");

                entity.Property(e => e.CredpAgent)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CREDP_AGENT");

                entity.Property(e => e.CredpAmount)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("CREDP_AMOUNT");

                entity.Property(e => e.CredpClaimedon)
                    .HasColumnType("datetime")
                    .HasColumnName("CREDP_CLAIMEDON");

                entity.Property(e => e.CredpCreatedon)
                    .HasColumnType("datetime")
                    .HasColumnName("CREDP_CREATEDON");

                entity.Property(e => e.CredpDeletionstate).HasColumnName("CREDP_DELETIONSTATE");

                entity.Property(e => e.CredpReferenceId)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CREDP_REFERENCE_ID");

                entity.Property(e => e.CredpStatus).HasColumnName("CREDP_STATUS");

                entity.Property(e => e.CusrUsername)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .HasColumnName("CUSR_USERNAME");
            });

            OnModelCreatingPartial(modelBuilder);
        }
    
    }
}
