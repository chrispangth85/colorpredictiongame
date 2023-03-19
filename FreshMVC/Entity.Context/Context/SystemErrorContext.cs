using System;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdSystemerror> CvdSystemerror { get; set; }

        protected void OnModelCreatingCvdSystemerrorContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdSystemerror>(entity =>
            {
                entity.HasKey(e => e.CerrorId);

                entity.ToTable("CVD_SYSTEMERROR");

                entity.Property(e => e.CerrorId).HasColumnName("CERROR_ID");

                entity.Property(e => e.CerrorAppother)
                    .HasMaxLength(200)
                    .HasColumnName("CERROR_APPOTHER");

                entity.Property(e => e.CerrorAppother2)
                    .HasMaxLength(1000)
                    .HasColumnName("CERROR_APPOTHER2");

                entity.Property(e => e.CerrorAppother3)
                    .HasMaxLength(1000)
                    .HasColumnName("CERROR_APPOTHER3");

                entity.Property(e => e.CerrorCreatedon)
                    .HasColumnType("datetime")
                    .HasColumnName("CERROR_CREATEDON")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CerrorInfo)
                    .IsRequired()
                    .HasColumnName("CERROR_INFO");
            });
            
            OnModelCreatingPartial(modelBuilder);
        }
    
    }
}
