using System;
using Entity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public virtual DbSet<CvdCompanywallet> CvdCompanywallet { get; set; }

        protected void OnModelCreatingCvdCompanywalletContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CvdCompanywallet>(entity =>
            {
                entity.HasKey(e => e.CcomId);

                entity.ToTable("CVD_COMPANYWALLET");

                entity.Property(e => e.CcomId).HasColumnName("CCOM_ID");

                entity.Property(e => e.CcomCreatedby)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CCOM_CREATEDBY")
                    .HasDefaultValueSql("('SYS')");

                entity.Property(e => e.CcomCreatedon)
                    .HasColumnType("datetime")
                    .HasColumnName("CCOM_CREATEDON")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CcomDeletionstate).HasColumnName("CCOM_DELETIONSTATE");

                entity.Property(e => e.CcomIsactive).HasColumnName("CCOM_ISACTIVE");

                entity.Property(e => e.CcomName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CCOM_NAME");

                entity.Property(e => e.CcomNetworktype)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CCOM_NETWORKTYPE");

                entity.Property(e => e.CcomWalletaddress)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("CCOM_WALLETADDRESS");
            });
            
            OnModelCreatingPartial(modelBuilder);
        }
    
    }
}
