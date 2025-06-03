using AnalyticsService.DataAccess.Entities;
using AnalyticsService.DataAccess.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.DataAccess.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<UserStat> UserStats { get; set; }
    public DbSet<InstrumentStat> InstrumentStats { get; set; }
    public DbSet<InstrumentDailyStat> InstrumentDailyStats { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserStatEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InstrumentStatEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InstrumentsDailyStatEntityTypeConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}