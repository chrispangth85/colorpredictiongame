using Microsoft.EntityFrameworkCore;

namespace Entity.Context
{
    public partial class SpeedyDbContext : DbContext
    {
        public SpeedyDbContext(DbContextOptions<DbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.OnModelCreatingCvdAdminOperationLogContext(modelBuilder);
            this.OnModelCreatingCvdCountryContext(modelBuilder);
            this.OnModelCreatingCvdParameterContext(modelBuilder);            
            this.OnModelCreatingCvdUserContext(modelBuilder);
            this.OnModelCreatingCvdOtpContext(modelBuilder);
            this.OnModelCreatingCvdReceiptContext(modelBuilder);
            this.OnModelCreatingCvdRedPacketContext(modelBuilder);
            this.OnModelCreatingCvdGameContext(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}