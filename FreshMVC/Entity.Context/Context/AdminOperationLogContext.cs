using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdAdminOperationlog> CvdAdminOperationLog { get; set; }

        protected void OnModelCreatingCvdAdminOperationLogContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdAdminOperationlog>(entity =>
            {
                entity.HasKey(e => e.CoplogId);

                entity.ToTable("CVD_ADMIN_OPERATIONLOG");

                entity.Property(e => e.CoplogId).HasColumnName("COPLOG_ID");

                entity.Property(e => e.CoplogAppnumber)
                    .HasColumnName("COPLOG_APPNUMBER")
                    .HasColumnType("decimal(15, 2)");

                entity.Property(e => e.CoplogAppother)
                    .HasColumnName("COPLOG_APPOTHER")
                    .HasMaxLength(100);

                entity.Property(e => e.CoplogAppother1)
                    .HasColumnName("COPLOG_APPOTHER1")
                    .HasMaxLength(100);

                entity.Property(e => e.CoplogAppother2)
                    .HasColumnName("COPLOG_APPOTHER2")
                    .HasMaxLength(100);

                entity.Property(e => e.CoplogAppother3)
                    .HasColumnName("COPLOG_APPOTHER3")
                    .HasMaxLength(100);

                entity.Property(e => e.CoplogAppother4)
                    .HasColumnName("COPLOG_APPOTHER4")
                    .HasMaxLength(100);

                entity.Property(e => e.CoplogCreatedby)
                    .IsRequired()
                    .HasColumnName("COPLOG_CREATEDBY")
                    .HasMaxLength(100);

                entity.Property(e => e.CoplogCreatedon)
                    .HasColumnName("COPLOG_CREATEDON")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CoplogDeletionstate)
                    .IsRequired()
                    .HasColumnName("COPLOG_DELETIONSTATE")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.CoplogOperation)
                    .HasColumnName("COPLOG_OPERATION")
                    .HasMaxLength(100);

                entity.Property(e => e.CoplogUpdatedby)
                    .IsRequired()
                    .HasColumnName("COPLOG_UPDATEDBY")
                    .HasMaxLength(50);

                entity.Property(e => e.CoplogUpdatedon)
                    .HasColumnName("COPLOG_UPDATEDON")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CusrUsername)
                    .IsRequired()
                    .HasColumnName("CUSR_USERNAME")
                    .HasMaxLength(50);
            });
            
            OnModelCreatingPartial(modelBuilder);
        }
    
    }
}
