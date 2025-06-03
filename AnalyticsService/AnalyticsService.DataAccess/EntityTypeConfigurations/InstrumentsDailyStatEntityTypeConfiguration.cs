using AnalyticsService.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnalyticsService.DataAccess.EntityTypeConfigurations;

public class InstrumentsDailyStatEntityTypeConfiguration : IEntityTypeConfiguration<InstrumentDailyStat>
{
    public void Configure(EntityTypeBuilder<InstrumentDailyStat> builder)
    {
        builder.HasKey(instrumentDailyStat => 
            new { instrumentDailyStat.InstrumentId, instrumentDailyStat.Date });
    }
}