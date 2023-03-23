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
            this.OnModelCreatingCvdGameSessionBetContext(modelBuilder);
            this.OnModelCreatingCvdProductContext(modelBuilder);
            this.OnModelCreatingCvdGameSessionContext(modelBuilder);
            this.OnModelCreatingCvdBannerContext(modelBuilder);
            this.OnModelCreatingCvdPaymentinfoContext(modelBuilder);
            this.OnModelCreatingCvdSystemerrorContext(modelBuilder);
            this.OnModelCreatingCvdCashwalletlogtempContext(modelBuilder);
            this.OnModelCreatingCvdCashwalletlogContext(modelBuilder);
            this.OnModelCreatingCvdBankContext(modelBuilder);
            this.OnModelCreatingCvdMemberBankContext(modelBuilder);
            this.OnModelCreatingCvdDailytransContext(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}